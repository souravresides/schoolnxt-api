using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data
{
    public static class SeedDataExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                 new IdentityRole { Id = Guid.Parse("446355ae-1e92-4df9-bd6f-db51322f6298").ToString(), Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
                 new IdentityRole { Id = Guid.Parse("3275882d-2998-4952-9520-670c88a00c81").ToString(), Name = "SchoolAdmin", NormalizedName = "SCHOOLADMIN" },
                 new IdentityRole { Id = Guid.Parse("50e8efc3-0966-444c-9da6-596ec27bc7b2").ToString(), Name = "Teacher", NormalizedName = "TEACHER" },
                 new IdentityRole { Id = Guid.Parse("fcc5069f-610a-4165-88b6-adfbcba13121").ToString(), Name = "Student", NormalizedName = "STUDENT" },
                 new IdentityRole { Id = Guid.Parse("35cd16cb-e048-4db1-a6e0-7ab9e0882155").ToString(), Name = "Parents", NormalizedName = "PARENTS" }
                );
        }
    }
}
