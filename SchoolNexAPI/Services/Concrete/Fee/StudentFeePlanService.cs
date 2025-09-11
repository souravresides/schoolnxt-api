using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.Models.Fees;
using SchoolNexAPI.Models.Student;
using SchoolNexAPI.Services.Abstract.Fee;

namespace SchoolNexAPI.Services.Concrete.Fee
{
    public class StudentFeePlanService : IStudentFeePlanService
    {
        private readonly AppDbContext _db;
        public StudentFeePlanService(AppDbContext db) { _db = db; }

        public async Task<StudentFeePlan> GenerateOrRefreshAsync(Guid schoolId, Guid studentId, Guid academicYearId, string createdBy)
        {
            // fetch student's class info - assume StudentModel exists in your DB
            var student = await _db.Set<StudentModel>().FirstOrDefaultAsync(s => s.Id == studentId && s.SchoolId == schoolId);
            if (student == null) throw new KeyNotFoundException("Student not found");

            var structureDb = await _db.FeeStructures.Include(fs => fs.Items).ToListAsync();


            var structure = await _db.FeeStructures.Include(fs => fs.Items)
                .FirstOrDefaultAsync(fs => fs.SchoolId == schoolId && fs.AcademicYearId == academicYearId && 
                fs.ClassName == student.StudentClass.ClassName && (fs.Section == null || fs.Section == student.ClassSection.SectionName));
            if (structure == null) throw new InvalidOperationException("Fee structure not defined");

            var existing = await _db.StudentFeePlans.Include(p => p.Lines)
                .FirstOrDefaultAsync(p => p.SchoolId == schoolId && p.StudentId == studentId && p.AcademicYearId == academicYearId);

            if (existing == null)
            {
                var plan = new StudentFeePlan
                {
                    Id = Guid.NewGuid(),
                    SchoolId = schoolId,
                    AcademicYearId = academicYearId,
                    StudentId = studentId,
                    FeeStructureId = structure.Id,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = createdBy,
                    Lines = structure.Items.Select(i => new StudentFeePlanLine
                    {
                        Id = Guid.NewGuid(),
                        FeeHeadId = i.FeeHeadId,
                        Amount = i.Amount,
                        Discount = 0m
                    }).ToList()
                };
                try
                {
                    _db.StudentFeePlans.Add(plan);
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
                return plan;
            }
            else
            {
                // refresh: replace lines (careful about discounts — you might want to preserve manual discounts)
                existing.FeeStructureId = structure.Id;
                existing.Lines.Clear();
                foreach (var i in structure.Items)
                {
                    existing.Lines.Add(new StudentFeePlanLine { Id = Guid.NewGuid(), FeeHeadId = i.FeeHeadId, Amount = i.Amount, Discount = 0m });
                }
                existing.UpdatedAt = DateTime.UtcNow;
                existing.UpdatedBy = createdBy;
                _db.StudentFeePlans.Update(existing);
                await _db.SaveChangesAsync();
                return existing;
            }
        }

        public Task<StudentFeePlan?> GetAsync(Guid schoolId, Guid studentId, Guid academicYearId) =>
    _db.StudentFeePlans.Include(p => p.Lines)
                       .FirstOrDefaultAsync(p => p.SchoolId == schoolId && p.StudentId == studentId && p.AcademicYearId == academicYearId);
    }
}
