using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.Enums;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class FileService : IFileService
    {
        private readonly IAzureService _azureService;
        private readonly AppDbContext _context;

        public FileService(IAzureService azureService, AppDbContext context)
        {
            this._azureService = azureService;
            this._context = context;
        }

        public async Task<FileRecordsModel> UploadEntityFileAsync(IFormFile file, BlobEntity entity, string entityId, 
            BlobCategory category, DocumentType documentType, Guid? schoolId = null,Guid? uploadedByUserId = null)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            var blobName = await _azureService.UploadFileAsync(file, entity, entityId, category, schoolId.ToString());

            var record = new FileRecordsModel
            {
                Id = Guid.NewGuid(),
                Entity = entity.ToString(),
                EntityId = Guid.Parse(entityId),
                FileCategory = category.ToString(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                FilePath = blobName,
                FileSize = file.Length,
                SchoolId = schoolId == Guid.Empty ? null : schoolId,
                DocumentType = documentType.ToString(),
                UploadedByUserId = uploadedByUserId,
                CreatedAt = DateTime.UtcNow
            };

            _context.FileRecords.Add(record);
            await _context.SaveChangesAsync();
            record.FilePath = await _azureService.GetSasUrlAsync(blobName);

            return record;
        }

        public async Task<List<FileRecordsModel>> GetEntityFilesAsync(
          BlobEntity entity,
          string entityId,
          BlobCategory? category = null,
          Guid? schoolId = null)
        {
            var query = _context.FileRecords
                .Where(f => f.Entity == entity.ToString() && f.EntityId == Guid.Parse(entityId));

            if (category.HasValue)
                query = query.Where(f => f.FileCategory == category.Value.ToString());

            if (schoolId.HasValue && schoolId != Guid.Empty)
                query = query.Where(f => f.SchoolId == schoolId);

            var records = await query.ToListAsync();

            foreach (var record in records)
            {
                record.FilePath = await _azureService.GetSasUrlAsync(record.FilePath);
            }

            return records;
        }

        public async Task<bool> DeleteEntityFilesAsync(
            BlobEntity entity,
            string entityId,
            BlobCategory? category = null,
            Guid? schoolId = null)
        {
            var files = await GetEntityFilesAsync(entity, entityId, category, schoolId);

            foreach (var file in files)
            {
                await _azureService.DeleteFileAsync(file.FilePath);

                _context.FileRecords.Remove(file);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }

}
