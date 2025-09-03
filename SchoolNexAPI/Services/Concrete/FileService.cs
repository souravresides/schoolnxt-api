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

        public async Task<FileRecordsModel> UpdateEntityFileAsync(IFormFile file, Guid fileId, BlobEntity entity,
            string entityId, BlobCategory category, DocumentType documentType, Guid? schoolId = null, Guid? uploadedByUserId = null)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            // Find existing record
            var existingRecord = await _context.FileRecords.FirstOrDefaultAsync(f => f.Id == fileId);
            if (existingRecord == null)
                throw new KeyNotFoundException("File record not found.");

            // Optionally delete old file from Azure
            if (!string.IsNullOrEmpty(existingRecord.FilePath))
            {
                await _azureService.DeleteFileAsync(existingRecord.FilePath);
            }

            // Upload new file
            var blobName = await _azureService.UploadFileAsync(file, entity, entityId, category, schoolId.ToString());

            // Update record
            existingRecord.FileName = file.FileName;
            existingRecord.ContentType = file.ContentType;
            existingRecord.FilePath = blobName;
            existingRecord.FileSize = file.Length;
            existingRecord.FileCategory = category.ToString();
            existingRecord.DocumentType = documentType.ToString();
            existingRecord.UploadedByUserId = uploadedByUserId;
            existingRecord.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Get SAS URL for return
            existingRecord.FilePath = await _azureService.GetSasUrlAsync(blobName);

            return existingRecord;
        }

        public async Task<FileRecordsModel> GetFileRecordAsync(Guid fileId)
        {
            var record = await _context.FileRecords
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if (record != null)
            {
                // Get SAS URL for secure access
                record.FilePath = await _azureService.GetSasUrlAsync(record.FilePath);
            }

            return record;
        }

        public async Task<bool> DeleteFileRecordAsync(Guid fileId)
        {
            var record = await _context.FileRecords.FirstOrDefaultAsync(f => f.Id == fileId);
            if (record == null) return false;

            // Delete file from Azure
            if (!string.IsNullOrEmpty(record.FilePath))
            {
                await _azureService.DeleteFileAsync(record.FilePath);
            }

            _context.FileRecords.Remove(record);
            await _context.SaveChangesAsync();

            return true;
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
