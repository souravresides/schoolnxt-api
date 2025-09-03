using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.Enums;
using SchoolNexAPI.Services.Abstract;
using System.Xml.Linq;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            this._fileService = fileService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] string entity,
            [FromForm] string entityId, [FromForm] string category, [FromForm] string documentType)
        {
            var record = await _fileService.UploadEntityFileAsync(
                file,
                Enum.Parse<BlobEntity>(entity),
                entityId,
                Enum.Parse<BlobCategory>(category),
                Enum.Parse<DocumentType>(documentType),
                GetSchoolId(),
                uploadedByUserId: GetUserId()
            );

            return Ok(record);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(
                IFormFile file,
                [FromForm] string entity,
                [FromForm] string entityId,
                [FromForm] string category,
                [FromForm] string documentType,
                [FromForm] Guid fileId // ID of the file record to update
            )
        {
            if (file == null) return BadRequest("File is required.");

            var record = await _fileService.UpdateEntityFileAsync(
                file,
                fileId,
                Enum.Parse<BlobEntity>(entity),
                entityId,
                Enum.Parse<BlobCategory>(category),
                Enum.Parse<DocumentType>(documentType),
                GetSchoolId(),
                uploadedByUserId: GetUserId()
            );

            return Ok(record);
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetFileRecord(Guid fileId)
        {
            var record = await _fileService.GetFileRecordAsync(fileId);
            if (record == null) return NotFound("File record not found.");

            return Ok(record);
        }

        [HttpDelete("{fileId}")]
        public async Task<IActionResult> DeleteFileRecord(Guid fileId)
        {
            var success = await _fileService.DeleteFileRecordAsync(fileId);
            if (!success) return NotFound("File record not found.");

            return NoContent();
        }




        [HttpGet("entity-files")]
        public async Task<IActionResult> GetEntityFiles(
            [FromQuery] string entity,
            [FromQuery] string entityId,
            [FromQuery] string? category = null)
        {
            var files = await _fileService.GetEntityFilesAsync(
                Enum.Parse<BlobEntity>(entity),
                entityId,
                category != null ? Enum.Parse<BlobCategory>(category) : null,
                GetSchoolId()
            );

            return Ok(files);
        }

        // 🔹 Delete all files for an entity
        [HttpDelete("entity-files")]
        public async Task<IActionResult> DeleteEntityFiles(
            [FromQuery] string entity,
            [FromQuery] string entityId,
            [FromQuery] string? category = null)
        {
            var deleted = await _fileService.DeleteEntityFilesAsync(
                Enum.Parse<BlobEntity>(entity),
                entityId,
                category != null ? Enum.Parse<BlobCategory>(category) : null,
                GetSchoolId()
            );

            return deleted ? Ok(new { message = "Files deleted successfully" })
                           : BadRequest(new { message = "Failed to delete files" });
        }

    }
}
