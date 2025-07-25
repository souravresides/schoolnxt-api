using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;
using System.Text.Json;

namespace SchoolNexAPI.Services.Concrete
{
    public class CustomFieldService : ICustomFieldService
    {
        private readonly AppDbContext _context;

        public CustomFieldService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CustomFieldDto>> GetAllAsync(Guid schoolId, string targetEntity)
        {
            var fields = await _context.CustomFieldDefinitions
                .Where(x => x.SchoolId == schoolId && x.IsVisible && x.EntityType == targetEntity)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();

            return fields.Select(x => new CustomFieldDto
            {
                Id = x.Id,
                FieldName = x.FieldName,
                FieldType = x.FieldType,
                IsRequired = x.IsRequired,
                DisplayOrder = x.DisplayOrder,
                TargetEntity = x.EntityType,
                Options = string.IsNullOrEmpty(x.FieldOptionsJson)
                    ? null
                    : JsonSerializer.Deserialize<List<string>>(x.FieldOptionsJson)
            });
        }


        public async Task<CustomFieldDto> GetByIdAsync(Guid id)
        {
            var field = await _context.CustomFieldDefinitions.FindAsync(id);
            if (field == null || !field.IsVisible) return null;

            return new CustomFieldDto
            {
                Id = field.Id,
                FieldName = field.FieldName,
                FieldType = field.FieldType,
                IsRequired = field.IsRequired,
                DisplayOrder = field.DisplayOrder,
                TargetEntity = field.EntityType,
                Options = string.IsNullOrEmpty(field.FieldOptionsJson)
                    ? null
                    : JsonSerializer.Deserialize<List<string>>(field.FieldOptionsJson)
            };
        }

        public async Task<CustomFieldDto> CreateAsync(Guid schoolId, CreateCustomFieldRequest request, string createdBy)
        {
            var model = new CustomFieldDefinitionModel
            {
                SchoolId = schoolId,
                DisplayName = request.FieldName,
                FieldName = request.FieldName,
                FieldType = request.FieldType,
                IsRequired = request.IsRequired,
                DisplayOrder = request.DisplayOrder,
                EntityType = request.TargetEntity,
                FieldOptionsJson = (request.Options != null && request.Options.Count > 0)
            ? JsonSerializer.Serialize(request.Options)
            : null,
                IsVisible = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.CustomFieldDefinitions.Add(model);
            await _context.SaveChangesAsync();

            return new CustomFieldDto
            {
                Id = model.Id,
                FieldName = model.FieldName,
                FieldType = model.FieldType,
                IsRequired = model.IsRequired,
                DisplayOrder = model.DisplayOrder,
                FieldOptionsJson = model.FieldOptionsJson
            };
        }
        public async Task<bool> UpdateAsync(UpdateCustomFieldRequest request, string updatedBy)
        {
            var field = await _context.CustomFieldDefinitions.FindAsync(request.Id);
            if (field == null || !field.IsVisible) return false;

            field.FieldName = request.FieldName;
            field.FieldType = request.FieldType;
            field.IsRequired = request.IsRequired;
            field.DisplayOrder = request.DisplayOrder;
            field.UpdatedAt = DateTime.UtcNow;
            field.FieldOptionsJson = (request.Options != null && request.Options.Count > 0)
            ? JsonSerializer.Serialize(request.Options)
            : null;
            field.UpdatedBy = updatedBy;

            _context.CustomFieldDefinitions.Update(field);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ReorderFieldsAsync(Guid schoolId, List<ReorderCustomFieldRequest> fields, string updatedBy)
        {
            foreach (var field in fields)
            {
                var existingField = await _context.CustomFieldDefinitions
                    .FirstOrDefaultAsync(f => f.Id == field.Id && f.SchoolId == schoolId);

                if (existingField != null)
                {
                    existingField.DisplayOrder = field.DisplayOrder;
                    existingField.UpdatedBy = updatedBy;
                    existingField.UpdatedAt = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var field = await _context.CustomFieldDefinitions.FindAsync(id);
            if (field == null || !field.IsVisible) return false;

            field.IsVisible = false;
            field.UpdatedAt = DateTime.UtcNow;
            _context.CustomFieldDefinitions.Remove(field);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
