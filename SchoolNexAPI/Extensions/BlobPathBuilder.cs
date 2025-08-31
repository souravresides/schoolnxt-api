using SchoolNexAPI.Enums;

namespace SchoolNexAPI.Extensions
{
    public static class BlobPathBuilder
    {
        public static string Build(
            BlobEntity entity,
            string entityId,
            BlobCategory category,
            string fileName,
            string? schoolId = null)
        {
            // Ensure safe unique file name
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(fileName)}";

            return entity switch
            {
                // 🔹 School-level entities (schoolId is REQUIRED)
                BlobEntity.Schools or
                BlobEntity.Students or
                BlobEntity.Teachers or
                BlobEntity.Classes or
                BlobEntity.Finance
                    => schoolId is null
                        ? throw new ArgumentException($"schoolId is required for entity {entity}")
                        : $"schools/{schoolId}/{entity}/{entityId}/{category}/{uniqueFileName}",

                // 🔹 Platform-level entity (schoolId is NOT allowed)
                BlobEntity.Platform
                    => schoolId is not null
                        ? throw new ArgumentException("schoolId must be null for Platform files")
                        : $"platform/{category}/{uniqueFileName}",

                BlobEntity.Users
        => $"users/{entityId}/{category}/{uniqueFileName}",

                _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, "Unsupported entity type")
            };
        }
    }

}
