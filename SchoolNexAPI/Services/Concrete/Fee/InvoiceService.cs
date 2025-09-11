using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.Models.Fees;
using SchoolNexAPI.Services.Abstract.Fee;

namespace SchoolNexAPI.Services.Concrete.Fee
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<InvoiceService> _log;
        public InvoiceService(AppDbContext db, ILogger<InvoiceService> log) { _db = db; _log = log; }

        private async Task<string> NextInvoiceNumberAsync(Guid schoolId, Guid ayId)
        {
            // atomic via transaction
            var seq = await _db.InvoiceSequences.FirstOrDefaultAsync(s => s.SchoolId == schoolId && s.AcademicYearId == ayId);
            if (seq == null)
            {
                seq = new InvoiceSequence { Id = Guid.NewGuid(), SchoolId = schoolId, AcademicYearId = ayId, NextNumber = 2 };
                _db.InvoiceSequences.Add(seq);
                await _db.SaveChangesAsync();
                return $"{ayId.ToString().Substring(0, 4)}-000001";
            }
            var num = seq.NextNumber;
            seq.NextNumber++;
            _db.InvoiceSequences.Update(seq);
            await _db.SaveChangesAsync();
            var ay = await _db.AcademicYears.FindAsync(ayId);
            var ayName = ay?.Name?.Replace(" ", "") ?? ayId.ToString().Substring(0, 4);
            return $"{ayName}-{num:D6}";
        }

        public async Task<Invoice> GenerateInvoiceFromPlanAsync(Guid schoolId, Guid studentId, Guid academicYearId, DateTime dueDate, string createdBy)
        {
            var plan = await _db.StudentFeePlans.Include(p => p.Lines)
                .FirstOrDefaultAsync(p => p.SchoolId == schoolId && p.StudentId == studentId && p.AcademicYearId == academicYearId);
            if (plan == null) throw new InvalidOperationException("No fee plan for student");

            // prevent duplicate for same month by checking IssueDate month/year + student
            var exist = await _db.Invoices.FirstOrDefaultAsync(i => i.SchoolId == schoolId && i.AcademicYearId == academicYearId && i.StudentId == studentId && i.IssueDate.Year == DateTime.UtcNow.Year && i.IssueDate.Month == DateTime.UtcNow.Month);
            if (exist != null) return exist; // idempotent behavior on monthly create

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                SchoolId = schoolId,
                AcademicYearId = academicYearId,
                StudentId = studentId,
                IssueDate = DateTime.UtcNow,
                DueDate = dueDate,
                InvoiceNumber = await NextInvoiceNumberAsync(schoolId, academicYearId),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                Status = InvoiceStatus.Issued
            };

            foreach (var line in plan.Lines)
            {
                var amount = Math.Round(line.Amount - line.Discount, 2);
                invoice.Lines.Add(new InvoiceLine
                {
                    Id = Guid.NewGuid(),
                    FeeHeadId = line.FeeHeadId,
                    Description = null,
                    Amount = amount,
                    TaxAmount = 0m
                });
            }

            invoice.TotalAmount = invoice.Lines.Sum(l => l.Amount);
            invoice.TotalTax = invoice.Lines.Sum(l => l.TaxAmount);
            invoice.TotalAdjustments = 0m;
            invoice.TotalLateFee = 0m;
            invoice.AmountDue = invoice.TotalAmount + invoice.TotalTax;

            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
            return invoice;
        }

        public async Task<(int created, int skipped)> GenerateInvoicesForMonthAsync(Guid schoolId, Guid academicYearId, int year, int month, string createdBy)
        {
            var created = 0; var skipped = 0;
            var plans = await _db.StudentFeePlans.Where(p => p.SchoolId == schoolId && p.AcademicYearId == academicYearId).ToListAsync();
            foreach (var p in plans)
            {
                // naive: generate one invoice per plan per call (adjust by user input)
                var exist = await _db.Invoices.FirstOrDefaultAsync(i => i.SchoolId == schoolId && i.AcademicYearId == academicYearId && i.StudentId == p.StudentId && i.IssueDate.Year == year && i.IssueDate.Month == month);
                if (exist != null) { skipped++; continue; }

                // create invoice
                var inv = await GenerateInvoiceFromPlanAsync(schoolId, p.StudentId, academicYearId, new DateTime(year, month, 10), createdBy);
                if (inv != null) created++;
            }
            return (created, skipped);
        }

        public async Task RecomputeInvoiceAsync(Guid invoiceId)
        {
            var inv = await _db.Invoices.Include(i => i.Lines).FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (inv == null) throw new KeyNotFoundException();

            inv.TotalAmount = inv.Lines.Sum(l => l.Amount);
            inv.TotalTax = inv.Lines.Sum(l => l.TaxAmount);
            // adjustments / payments
            var adjustments = await _db.Adjustments.Where(a => a.InvoiceId == inv.Id).ToListAsync();
            var positiveDebits = adjustments.Where(a => a.Type == AdjustmentType.Debit || a.Type == AdjustmentType.LateFee).Sum(a => a.Amount);
            var negativeCredits = adjustments.Where(a => a.Type == AdjustmentType.Waiver || a.Type == AdjustmentType.Scholarship || a.Type == AdjustmentType.Credit).Sum(a => a.Amount);

            inv.TotalAdjustments = positiveDebits - negativeCredits;
            inv.TotalLateFee = adjustments.Where(a => a.Type == AdjustmentType.LateFee).Sum(a => a.Amount);

            // payments allocated
            var allocated = await _db.PaymentAllocations.Where(pa => pa.InvoiceId == inv.Id).SumAsync(pa => (decimal?)pa.Amount) ?? 0m;

            inv.AmountDue = inv.TotalAmount + inv.TotalTax + inv.TotalLateFee + positiveDebits - negativeCredits - allocated;
            inv.Status = inv.AmountDue <= 0 ? InvoiceStatus.Paid : (allocated > 0 ? InvoiceStatus.PartiallyPaid : InvoiceStatus.Issued);

            _db.Invoices.Update(inv);
            await _db.SaveChangesAsync();
        }

        public Task<List<Invoice>> ListByStudentAsync(Guid schoolId, Guid studentId, Guid academicYearId) =>
    _db.Invoices.Include(i => i.Lines)
                .Where(i => i.SchoolId == schoolId && i.StudentId == studentId && i.AcademicYearId == academicYearId)
                .ToListAsync();
    }
}
