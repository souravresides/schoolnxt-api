using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs.Fee;
using SchoolNexAPI.Models.Fees;
using SchoolNexAPI.Services.Abstract.Fee;

namespace SchoolNexAPI.Services.Concrete.Fee
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<PaymentService> _log;
        private const int MaxRetries = 3;

        public PaymentService(AppDbContext db, ILogger<PaymentService> log) { _db = db; _log = log; }

        public async Task<Payment> RecordPaymentAsync(CreatePaymentDto dto, string createdBy, string idempotencyKey)
        {
            var schoolId = dto.SchoolId ?? throw new ArgumentException("SchoolId required");
            // idempotency pre-check
            var existing = await _db.Payments.FirstOrDefaultAsync(p => p.SchoolId == schoolId && p.IdempotencyKey == idempotencyKey);
            if (existing != null) return existing;

            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var payment = new Payment
                {
                    Id = Guid.NewGuid(),
                    SchoolId = schoolId,
                    AcademicYearId = dto.AcademicYearId,
                    StudentId = dto.StudentId,
                    Amount = Math.Round(dto.Amount, 2),
                    Method = dto.Method,
                    ReferenceNumber = dto.ReferenceNumber,
                    IdempotencyKey = idempotencyKey,
                    ReceivedAt = DateTime.UtcNow
                };

                _db.Payments.Add(payment);
                await _db.SaveChangesAsync();

                // allocations - user provided or auto-FIFO
                decimal remaining = payment.Amount;
                var allocations = new List<PaymentAllocation>();

                if (dto.Allocations != null && dto.Allocations.Any())
                {
                    foreach (var a in dto.Allocations)
                    {
                        if (remaining <= 0) break;
                        var inv = await _db.Invoices.FirstOrDefaultAsync(i => i.Id == a.InvoiceId && i.SchoolId == schoolId);
                        if (inv == null) throw new KeyNotFoundException($"Invoice {a.InvoiceId} not found");

                        var allocAmt = Math.Min(a.Amount, remaining);
                        allocations.Add(new PaymentAllocation { Id = Guid.NewGuid(), PaymentId = payment.Id, InvoiceId = inv.Id, Amount = allocAmt });

                        // update invoice with concurrency retry
                        int tries = 0;
                        bool saved = false;
                        while (!saved)
                        {
                            try
                            {
                                inv.AmountDue -= allocAmt;
                                if (inv.AmountDue <= 0) inv.Status = InvoiceStatus.Paid;
                                else inv.Status = InvoiceStatus.PartiallyPaid;
                                _db.Invoices.Update(inv);
                                await _db.SaveChangesAsync();
                                saved = true;
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                tries++;
                                if (tries >= MaxRetries) throw;
                                // reload inv
                                _db.Entry(inv).Reload();
                            }
                        }
                        remaining -= allocAmt;
                    }
                }
                else
                {
                    // auto-FIFO unpaid invoices
                    var unpaid = await _db.Invoices.Where(i => i.SchoolId == schoolId && i.StudentId == dto.StudentId && i.AmountDue > 0)
                        .OrderBy(i => i.DueDate).ToListAsync();

                    foreach (var inv in unpaid)
                    {
                        if (remaining <= 0) break;
                        var allocAmt = Math.Min(inv.AmountDue, remaining);
                        allocations.Add(new PaymentAllocation { Id = Guid.NewGuid(), PaymentId = payment.Id, InvoiceId = inv.Id, Amount = allocAmt });

                        int tries = 0;
                        bool saved = false;
                        while (!saved)
                        {
                            try
                            {
                                inv.AmountDue -= allocAmt;
                                if (inv.AmountDue <= 0) inv.Status = InvoiceStatus.Paid;
                                else inv.Status = InvoiceStatus.PartiallyPaid;
                                _db.Invoices.Update(inv);
                                await _db.SaveChangesAsync();
                                saved = true;
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                tries++;
                                if (tries >= MaxRetries) throw;
                                _db.Entry(inv).Reload();
                            }
                        }
                        remaining -= allocAmt;
                    }
                }

                if (remaining > 0)
                {
                    // strategy: create open credit (Adjustment of type Credit) or return error.
                    // We'll create an open credit adjustment.
                    var credit = new Adjustment
                    {
                        Id = Guid.NewGuid(),
                        SchoolId = schoolId,
                        AcademicYearId = payment.AcademicYearId,
                        StudentId = payment.StudentId,
                        InvoiceId = null,
                        Type = AdjustmentType.Credit,
                        Amount = remaining,
                        Reason = "Advance payment (unallocated)"
                    };
                    _db.Adjustments.Add(credit);
                    // no need to adjust invoices
                }

                // attach allocations
                foreach (var a in allocations) _db.PaymentAllocations.Add(a);

                await _db.SaveChangesAsync();
                await tx.CommitAsync();
                return payment;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public Task<List<Payment>> ListByStudentAsync(Guid schoolId, Guid studentId) =>
    _db.Payments.Where(p => p.SchoolId == schoolId && p.StudentId == studentId).ToListAsync();
    }
}
