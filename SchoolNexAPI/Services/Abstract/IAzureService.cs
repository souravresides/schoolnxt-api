using SchoolNexAPI.Enums;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IAzureService
    {
        Task<string> UploadFileAsync(IFormFile file, BlobEntity entity, string entityId, BlobCategory category, string? schoolId = null);
        Task<string> GetSasUrlAsync(string blobName, TimeSpan? validFor = null);
        Task<bool> DeleteFileAsync(string blobName);
        Task<string> UpdateFileAsync(string blobName, IFormFile file);
    }
}
