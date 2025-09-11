using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs.Fee;
using SchoolNexAPI.Models.Fees;
using SchoolNexAPI.Services.Abstract.Fee;

namespace SchoolNexAPI.Services.Concrete.Fee
{
    public class FeeStructureService : IFeeStructureService
    {
        private readonly AppDbContext _db;
        public FeeStructureService(AppDbContext db) { _db = db; }

        public async Task<FeeStructure> CreateAsync(FeeStructureCreateDto dto, string createdBy, Guid effectiveSchoolId)
        {
            var fs = new FeeStructure
            {
                Id = Guid.NewGuid(),
                SchoolId = effectiveSchoolId,
                AcademicYearId = dto.AcademicYearId,
                ClassName = dto.ClassName,
                Section = dto.Section,
                BillingCycle = dto.BillingCycle,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };
            fs.Items = dto.Items.Select(i => new FeeStructureItem
            {
                Id = Guid.NewGuid(),
                FeeHeadId = i.FeeHeadId,
                Amount = Math.Round(i.Amount, 2)
            }).ToList();

            _db.FeeStructures.Add(fs);
            await _db.SaveChangesAsync();
            return fs;
        }

        public Task<List<FeeStructure>> ListByClassAsync(Guid schoolId, Guid ayId, string className) =>
            _db.FeeStructures
               .Include(f => f.Items)
               .Where(f => f.SchoolId == schoolId && f.AcademicYearId == ayId && f.ClassName == className)
               .ToListAsync();
    }
}
