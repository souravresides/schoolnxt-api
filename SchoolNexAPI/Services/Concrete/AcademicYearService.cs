using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
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

        public async Task<AcademicYearModel> CreateAsync(Guid schoolId, string name, DateTime start, DateTime end, string userId)
        {
            if (start >= end)
                throw new InvalidOperationException("Start date must be before end date.");

            // Prevent duplicate names or overlapping date ranges
            var exists = await _context.AcademicYears
                .AnyAsync(x => x.SchoolId == schoolId &&
                    (x.Name == name || (start <= x.EndDate && end >= x.StartDate)));

            if (exists)
                throw new InvalidOperationException("Academic Year already exists or overlaps with an existing one.");

            var ay = new AcademicYearModel
            {
                Id = Guid.NewGuid(),
                SchoolId = schoolId,
                Name = name,
                StartDate = start,
                EndDate = end,
                Status = AcademicYearStatus.Draft,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.AcademicYears.Add(ay);
            await _context.SaveChangesAsync();
            return ay;
        }

        public async Task<AcademicYearModel> ActivateAsync(Guid schoolId, Guid ayId, string userId)
        {
            var ay = await _context.AcademicYears.FirstOrDefaultAsync(x => x.Id == ayId && x.SchoolId == schoolId);
            if (ay == null) throw new InvalidOperationException("Academic Year not found.");

            if (ay.Status == AcademicYearStatus.Closed || ay.Status == AcademicYearStatus.Archived)
                throw new InvalidOperationException("Cannot activate a closed or archived academic year.");

            if (DateTime.UtcNow < ay.StartDate || DateTime.UtcNow > ay.EndDate)
                throw new InvalidOperationException("Cannot activate academic year outside its valid date range.");

            // deactivate others
            var others = _context.AcademicYears.Where(x => x.SchoolId == schoolId && x.Id != ayId && x.IsCurrent);
            foreach (var o in others)
            {
                o.IsCurrent = false;
                o.Status = AcademicYearStatus.Closed; // optional: auto-close old active years
                o.UpdatedAt = DateTime.UtcNow;
                o.UpdatedBy = userId;
            }

            ay.Status = AcademicYearStatus.Active;
            ay.IsCurrent = true;
            ay.UpdatedBy = userId;
            ay.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ay;
        }

        public async Task<AcademicYearModel> CloseAsync(Guid schoolId, Guid ayId, string userId)
        {
            var ay = await _context.AcademicYears.FirstOrDefaultAsync(x => x.Id == ayId && x.SchoolId == schoolId);
            if (ay == null) throw new InvalidOperationException("Academic Year not found.");

            if (ay.Status != AcademicYearStatus.Active)
                throw new InvalidOperationException("Only active academic years can be closed.");

            if (DateTime.UtcNow < ay.EndDate)
                throw new InvalidOperationException("Cannot close an academic year before its end date.");

            ay.Status = AcademicYearStatus.Closed;
            ay.IsCurrent = false;
            ay.FinanceLockedAt = DateTime.UtcNow;
            ay.UpdatedBy = userId;
            ay.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ay;
        }

        public Task<List<AcademicYearModel>> GetAllAsync(Guid schoolId) =>
            _context.AcademicYears
                .Where(x => x.SchoolId == schoolId)
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();

        public async Task<AcademicYearModel?> GetCurrentAsync(Guid schoolId)
        {
            return await _context.AcademicYears
                .Where(x => x.SchoolId == schoolId && x.IsCurrent && x.Status == AcademicYearStatus.Active)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();
        }

    }
}
