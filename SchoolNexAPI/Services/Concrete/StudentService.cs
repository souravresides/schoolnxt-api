using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Migrations;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;
using System.Net;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Collections.Specialized.BitVector32;

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
                Class = request.Class,
                Section = request.Section,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
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
                Class = student.Class,
                Section = student.Section,
                DateOfBirth = student.DateOfBirth,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                FatherName = student.FatherName,
                MotherName = student.MotherName,
                Address = student.Address,
                AcademicYearId = student.AcademicYearId,
                CustomFields = student.CustomFieldValuesList.Select(v => new CustomFieldDto
                {
                    Id = v.CustomFieldDefinitionId,
                    FieldName = v.CustomFieldDefinition.FieldName,
                    FieldType = v.CustomFieldDefinition.FieldType,
                    IsRequired = v.CustomFieldDefinition.IsRequired,
                    TargetEntity = v.CustomFieldDefinition.EntityType,
                    DisplayOrder = v.CustomFieldDefinition.DisplayOrder,
                    FieldValue = v.Value
                }).ToList()
            };
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync(Guid schoolId)
        {
            IQueryable<StudentModel> query = _context.Students
                .Include(s => s.CustomFieldValuesList)
                    .ThenInclude(v => v.CustomFieldDefinition);

            if (schoolId != Guid.Empty)
            {
                // SchoolAdmin → filter by school
                query = query.Where(s => s.SchoolId == schoolId);
            }

            return query.Select(s => new StudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Section = s.Section,
                Class = s.Class,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                FatherName = s.FatherName,
                MotherName = s.MotherName,
                Address = s.Address,
                AcademicYearId = s.AcademicYearId,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy,
                CustomFields = s.CustomFieldValuesList.Select(v => new CustomFieldDto
                {
                    Id = v.CustomFieldDefinitionId,
                    FieldName = v.CustomFieldDefinition.FieldName,
                    FieldType = v.CustomFieldDefinition.FieldType,
                    IsRequired = v.CustomFieldDefinition.IsRequired,
                    TargetEntity = v.CustomFieldDefinition.EntityType,
                    DisplayOrder = v.CustomFieldDefinition.DisplayOrder,
                    FieldValue = v.Value
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
            student.Section = request.Section;
            student.Class = request.Class;
            student.Email = request.Email;
            student.PhoneNumber = request.PhoneNumber;
            student.DateOfBirth = request.DateOfBirth;
            student.Gender = request.Gender;
            student.FatherName = request.FatherName;
            student.MotherName = request.MotherName;
            student.Address = request.Address;
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
