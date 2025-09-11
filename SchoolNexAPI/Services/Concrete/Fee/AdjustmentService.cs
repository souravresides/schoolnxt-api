using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs.Fee;
using SchoolNexAPI.Models.Fees;
using SchoolNexAPI.Services.Abstract.Fee;

namespace SchoolNexAPI.Services.Concrete.Fee
{
    public class AdjustmentService : IAdjustmentService
    {
        private readonly AppDbContext _db;
        public AdjustmentService(AppDbContext db) { _db = db; }

        public async Task<Adjustment> ApplyAsync(CreateAdjustmentDto dto, string createdBy)
        {
            var schoolId = dto.SchoolId ?? throw new ArgumentException("SchoolId required");
            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var adj = new Adjustment
                {
                    Id = Guid.NewGuid(),
                    SchoolId = schoolId,
                    AcademicYearId = dto.AcademicYearId,
                    StudentId = dto.StudentId,
                    InvoiceId = dto.InvoiceId,
                    Type = dto.Type,
                    Amount = Math.Round(dto.Amount, 2),
                    Reason = dto.Reason,
                    CreatedAt = DateTime.UtcNow
                };

                _db.Adjustments.Add(adj);

                if (dto.InvoiceId.HasValue)
                {
                    var inv = await _db.Invoices.FirstOrDefaultAsync(i => i.Id == dto.InvoiceId.Value && i.SchoolId == schoolId);
                    if (inv == null) throw new KeyNotFoundException("Invoice not found");

                    // adjustment effect
                    if (dto.Type == AdjustmentType.Waiver || dto.Type == AdjustmentType.Scholarship || dto.Type == AdjustmentType.Credit)
                        inv.AmountDue -= dto.Amount;
                    else
                        inv.AmountDue += dto.Amount;

                    // recalc status
                    var allocated = await _db.PaymentAllocations.Where(pa => pa.InvoiceId == inv.Id).SumAsync(pa => (decimal?)pa.Amount) ?? 0m;
                    if (inv.AmountDue <= 0) inv.Status = InvoiceStatus.Paid;
                    else if (allocated > 0) inv.Status = InvoiceStatus.PartiallyPaid;
                    else inv.Status = InvoiceStatus.Issued;

                    _db.Invoices.Update(inv);
                }

                await _db.SaveChangesAsync();
                await tx.CommitAsync();
                return adj;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public Task<List<Adjustment>> GetByStudentAsync(Guid schoolId, Guid studentId) =>
    _db.Adjustments.Where(a => a.SchoolId == schoolId && a.StudentId == studentId).ToListAsync();
    }
}
