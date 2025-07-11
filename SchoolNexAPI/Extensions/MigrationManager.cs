using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;

namespace SchoolNexAPI.Extensions
{
    public static class MigrationManager
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            try
            {
                using var scope = app.ApplicationServices.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                throw;
            }
        }
    }
}
