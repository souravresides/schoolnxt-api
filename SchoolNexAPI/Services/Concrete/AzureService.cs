using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Enums;
using SchoolNexAPI.Extensions;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class AzureService : IAzureService
    {
        private readonly AzureOptions _options;
        private readonly BlobServiceClient _blobServiceClient;
        public AzureService(IOptions<AzureOptions> options)
        {
            _options = options.Value;
            _blobServiceClient = new BlobServiceClient(options.Value.ConnectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, BlobEntity entity, string entityId, BlobCategory category, string? schoolId = null)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_options.ContainerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobName = BlobPathBuilder.Build(entity, entityId, category, file.FileName, schoolId);

            var blobClient = containerClient.GetBlobClient(blobName);

            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobName;
        }

        public async Task<string> GetSasUrlAsync(string blobName, TimeSpan? validFor = null)
        {
            if(blobName == null)
            {
                return null;
            }
            var containerClient = _blobServiceClient.GetBlobContainerClient(_options.ContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var expiry = DateTimeOffset.UtcNow.Add(validFor ?? TimeSpan.FromMinutes(15));

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = containerClient.Name,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = expiry
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);
            var sasUri = blobClient.GenerateSasUri(sasBuilder);

            return sasUri.ToString();
        }

        public async Task<bool> DeleteFileAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_options.ContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var response = await blobClient.DeleteIfExistsAsync();
            return response.Value;
        }

        public async Task<string> UpdateFileAsync(string blobName, IFormFile file)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_options.ContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobName;
        }


    }
}
