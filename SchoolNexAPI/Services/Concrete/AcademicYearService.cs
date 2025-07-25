using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly AppDbContext _context;

        public AcademicYearService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AcademicYearModel>> GetAllAsync()
        {
            return await _context.AcademicYears.Include(a => a.School).ToListAsync();
        }

        public async Task<AcademicYearModel> GetByIdAsync(Guid id)
        {
            return await _context.AcademicYears.Include(a => a.School)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AcademicYearModel> CreateAsync(Guid SchoolId,string createdBy,CreateAcademicYearDto dto)
        {
            var yearName = $"{dto.StartDate.Year}-{dto.EndDate.Year}";

            var model = new AcademicYearModel
            {
                Id = Guid.NewGuid(),
                SchoolId = SchoolId,
                YearName = yearName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsCurrent = dto.IsCurrent,
                IsLocked = dto.IsLocked,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.AcademicYears.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<AcademicYearModel> UpdateAsync(Guid id, Guid SchoolId,string updatedBy, CreateAcademicYearDto dto)
        {
            var existing = await _context.AcademicYears.FindAsync(id);
            if (existing == null) return null;

            existing.YearName = $"{dto.StartDate.Year}-{dto.EndDate.Year}";
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;
            existing.IsCurrent = dto.IsCurrent;
            existing.IsLocked = dto.IsLocked;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = updatedBy;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.AcademicYears.FindAsync(id);
            if (model == null) return false;

            _context.AcademicYears.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
