using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.Models;
using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.Services.Background
{
    public class LateFeeHostedService : BackgroundService
    {
        private readonly IServiceProvider _sp;
        private readonly ILogger<LateFeeHostedService> _log;
        public LateFeeHostedService(IServiceProvider sp, ILogger<LateFeeHostedService> log) { _sp = sp; _log = log; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _sp.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    // For each school & active AY, find overdue invoices and create adjustments if not yet created for that date.
                    var activeAys = await db.AcademicYears.Where(a => a.Status == AcademicYearStatus.Active).ToListAsync(stoppingToken);
                    foreach (var ay in activeAys)
                    {
                        var overdue = await db.Invoices.Where(i => i.SchoolId == ay.SchoolId && i.AcademicYearId == ay.Id && i.AmountDue > 0 && i.DueDate < DateTime.UtcNow.Date).ToListAsync(stoppingToken);
                        foreach (var inv in overdue)
                        {
                            // idempotency: ensure not applied already today (use adjustments with same InvoiceId + Type + CreatedAt date)
                            var appliedToday = await db.Adjustments.AnyAsync(a => a.InvoiceId == inv.Id && a.Type == AdjustmentType.LateFee && a.CreatedAt.Date == DateTime.UtcNow.Date);
                            if (appliedToday) continue;
                            var daysLate = (DateTime.UtcNow.Date - inv.DueDate.Date).Days;
                            if (daysLate <= 0) continue;
                            // simple policy: 0.5% per day
                            var late = Math.Round(inv.TotalAmount * 0.005m * daysLate, 2);
                            if (late <= 0) continue;
                            db.Adjustments.Add(new Adjustment { Id = Guid.NewGuid(), SchoolId = inv.SchoolId, AcademicYearId = inv.AcademicYearId, StudentId = inv.StudentId, InvoiceId = inv.Id, Type = AdjustmentType.LateFee, Amount = late, Reason = $"Late fee {daysLate}d" });
                            inv.AmountDue += late;
                            inv.TotalLateFee += late;
                            db.Invoices.Update(inv);
                            await db.SaveChangesAsync(stoppingToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, "LateFee job failed");
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // daily
            }
        }
    }
}
