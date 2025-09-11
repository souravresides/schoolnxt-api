using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolNexAPI.Services.Concrete;

namespace SchoolNexAPI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("SchoolNexDB"));

            // use a dummy tenant context for design-time
            var tenant = new TenantContext { CurrentSchoolId = null };
            return new AppDbContext(optionsBuilder.Options, tenant);
        }
    }
}
