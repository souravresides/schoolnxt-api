using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StudentDto> CreateAsync(Guid schoolId, CreateStudentRequest request, string createdBy)
        {
            var student = new StudentModel
            {
                Id = Guid.NewGuid(),
                SchoolId = schoolId,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                AcademicYearId = request.AcademicYearId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
            };

            _context.Students.Add(student);

            if (request.CustomFields != null && request.CustomFields.Count > 0)
            {
                foreach (var field in request.CustomFields)
                {
                    var fieldDefinition = await _context.CustomFieldDefinitions
                        .FirstOrDefaultAsync(x => x.Id == field.CustomFieldDefinitionId && x.IsVisible);

                    if (fieldDefinition != null)
                    {
                        var value = new StudentCustomFieldValueModel
                        {
                            Id = Guid.NewGuid(),
                            SchoolId = schoolId,
                            StudentId = student.Id,
                            CustomFieldDefinitionId = field.CustomFieldDefinitionId,
                            Value = field.Value,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = createdBy
                        };
                        _context.StudentCustomFieldValues.Add(value);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return await GetByIdAsync(student.Id);
        }

        public async Task<StudentDto> GetByIdAsync(Guid studentId)
        {
            var student = await _context.Students
                .Include(s => s.CustomFieldValuesList)
                    .ThenInclude(v => v.CustomFieldDefinition)
                .FirstOrDefaultAsync(x => x.Id == studentId);

            if (student == null) return null;

            return new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                AcademicYearId = student.AcademicYearId,
                CustomFields = student.CustomFieldValuesList.Select(v => new CustomFieldDto
                {
                    Id = v.CustomFieldDefinitionId,
                    FieldName = v.CustomFieldDefinition.FieldName,
                    FieldType = v.CustomFieldDefinition.FieldType,
                    IsRequired = v.CustomFieldDefinition.IsRequired,
                    TargetEntity = v.CustomFieldDefinition.EntityType,
                    DisplayOrder = v.CustomFieldDefinition.DisplayOrder
                }).ToList()
            };
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync(Guid schoolId)
        {
            var students = await _context.Students
                .Where(s => s.SchoolId == schoolId)
                .Include(s => s.CustomFieldValuesList)
                    .ThenInclude(v => v.CustomFieldDefinition)
                .ToListAsync();

            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                AcademicYearId = s.AcademicYearId,
                CustomFields = s.CustomFieldValuesList.Select(v => new CustomFieldDto
                {
                    Id = v.CustomFieldDefinitionId,
                    FieldName = v.CustomFieldDefinition.FieldName,
                    FieldType = v.CustomFieldDefinition.FieldType,
                    IsRequired = v.CustomFieldDefinition.IsRequired,
                    TargetEntity = v.CustomFieldDefinition.EntityType,
                    DisplayOrder = v.CustomFieldDefinition.DisplayOrder
                }).ToList()
            });
        }
        public async Task<bool> UpdateAsync(Guid studentId, UpdateStudentRequest request, string updatedBy)
        {
            var student = await _context.Students
                .Include(s => s.CustomFieldValuesList)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return false;

            student.FullName = request.FullName;
            student.DateOfBirth = request.DateOfBirth;
            student.Gender = request.Gender;
            student.AcademicYearId = request.AcademicYearId;
            student.UpdatedAt = DateTime.UtcNow;
            student.UpdatedBy = updatedBy;

            // Update custom field values
            if (request.CustomFields != null)
            {
                foreach (var field in request.CustomFields)
                {
                    var existingValue = student.CustomFieldValuesList
                        .FirstOrDefault(x => x.CustomFieldDefinitionId == field.CustomFieldDefinitionId);

                    if (existingValue != null)
                    {
                        existingValue.Value = field.Value;
                        existingValue.UpdatedAt = DateTime.UtcNow;
                        existingValue.UpdatedBy = updatedBy;
                    }
                    else
                    {
                        // Add new custom field value
                        _context.StudentCustomFieldValues.Add(new StudentCustomFieldValueModel
                        {
                            Id = Guid.NewGuid(),
                            StudentId = student.Id,
                            SchoolId = student.SchoolId,
                            CustomFieldDefinitionId = field.CustomFieldDefinitionId,
                            Value = field.Value,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = updatedBy
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(Guid studentId)
        {
            var student = await _context.Students
                .Include(s => s.CustomFieldValuesList)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return false;

            // Delete related custom field values
            if (student.CustomFieldValuesList != null && student.CustomFieldValuesList.Any())
            {
                _context.StudentCustomFieldValues.RemoveRange(student.CustomFieldValuesList);
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
