using SchoolNexAPI.Enums;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IFileService
    {
        Task<FileRecordsModel> UploadEntityFileAsync(IFormFile file, BlobEntity entity, string entityId,
            BlobCategory category, DocumentType documentType, Guid? schoolId = null, Guid? uploadedByUserId = null);

        Task<List<FileRecordsModel>> GetEntityFilesAsync(BlobEntity entity, string entityId,
            BlobCategory? category = null, Guid? schoolId = null);

        Task<bool> DeleteEntityFilesAsync(BlobEntity entity, string entityId,
            BlobCategory? category = null, Guid? schoolId = null);

        Task<FileRecordsModel> UpdateEntityFileAsync(IFormFile file, Guid fileId, BlobEntity entity, string entityId,
            BlobCategory category, DocumentType documentType, Guid? schoolId = null, Guid? uploadedByUserId = null);

        Task<FileRecordsModel> GetFileRecordAsync(Guid fileId);

        Task<bool> DeleteFileRecordAsync(Guid fileId);
    }
}
