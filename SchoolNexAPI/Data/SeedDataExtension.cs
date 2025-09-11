using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Models;
using SchoolNexAPI.Models.Fees;

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

            modelBuilder.Entity<StudentClassModel>().HasData(
                 new StudentClassModel { Id = Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), ClassName = "Play School" },
                 new StudentClassModel { Id = Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc964ff"), ClassName = "Pre Nursery" },
                 new StudentClassModel { Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), ClassName = "Nursery" },
                 new StudentClassModel { Id = Guid.Parse("21EC2020-3AEA-4069-A2DD-08002B30309D"), ClassName = "LKG" },
                 new StudentClassModel { Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), ClassName = "UKG" },
                 new StudentClassModel { Id = Guid.Parse("9a0b1234-5678-4def-9012-abcdef123456"), ClassName = "1" },
                 new StudentClassModel { Id = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"), ClassName = "2" },
                 new StudentClassModel { Id = Guid.Parse("c56a4180-65aa-42ec-a945-5fd21dec0538"), ClassName = "3" },
                 new StudentClassModel { Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"), ClassName = "4" },
                 new StudentClassModel { Id = Guid.Parse("e02fd0e4-00fd-090A-ca30-0d00a0038ba0"), ClassName = "5" },
                 new StudentClassModel { Id = Guid.Parse("5a1a0c0a-44f8-4c8a-9b2d-8a4e6f0d1234"), ClassName = "6" },
                 new StudentClassModel { Id = Guid.Parse("a8098c1a-f86e-11da-bd1a-00112444be1e"), ClassName = "7" },
                 new StudentClassModel { Id = Guid.Parse("6fa459ea-ee8a-3ca4-894e-db77e160355e"), ClassName = "8" },
                 new StudentClassModel { Id = Guid.Parse("16fd2706-8baf-433b-82eb-8c7fada847da"), ClassName = "9" },
                 new StudentClassModel { Id = Guid.Parse("886313e1-3b8a-5372-9b90-0c9aee199e5d"), ClassName = "10" },
                 new StudentClassModel { Id = Guid.Parse("4f5b5b6c-29c2-4a93-b0ef-0c1c9f1e5d4c"), ClassName = "11" },
                 new StudentClassModel { Id = Guid.Parse("12345678-1234-1234-1234-123456789abc"), ClassName = "12" }
             );

            modelBuilder.Entity<PaymentMethodModel>().HasData(
                new PaymentMethodModel { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), MethodName = "Cash", Value = (int)PaymentMethod.Cash },
                new PaymentMethodModel { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), MethodName = "Cheque", Value = (int)PaymentMethod.Cheque },
                new PaymentMethodModel { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), MethodName = "BankTransfer", Value = (int)PaymentMethod.BankTransfer },
                new PaymentMethodModel { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), MethodName = "Online", Value = (int)PaymentMethod.Online }
            );

            modelBuilder.Entity<PaymentStatusModel>().HasData(
                new PaymentStatusModel { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), StatusName = "Pending", Value = (int)PaymentStatus.Pending },
                new PaymentStatusModel { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), StatusName = "Confirmed", Value = (int)PaymentStatus.Confirmed },
                new PaymentStatusModel { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), StatusName = "Failed", Value = (int)PaymentStatus.Failed },
                new PaymentStatusModel { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), StatusName = "Refunded", Value = (int)PaymentStatus.Refunded }
            );

            modelBuilder.Entity<ClassSectionModel>().HasData(
              new ClassSectionModel { Id = Guid.Parse("d14f1f3e-1c2a-4f8e-9b3f-1a2b3c4d5e6f"), SectionName = "A" },
              new ClassSectionModel { Id = Guid.Parse("e25f2f4e-2d3b-4f9f-0c4d-2b3c4d5e6f70"), SectionName = "B" },
              new ClassSectionModel { Id = Guid.Parse("f36f3f5f-3e4c-4f0f-1d5e-3c4d5e6f7a81"), SectionName = "C" },
              new ClassSectionModel { Id = Guid.Parse("a47f4f6f-4f5d-7f1f-2e6f-4d5e6f7b9c12"), SectionName = "D" },
              new ClassSectionModel { Id = Guid.Parse("b58f5f7f-5f6e-8f2f-3f7f-5e6f7c8d9e34"), SectionName = "E" },
              new ClassSectionModel { Id = Guid.Parse("c69f6f8f-6f7f-9f3f-4f8f-6f7d8e9f0a56"), SectionName = "F" },
              new ClassSectionModel { Id = Guid.Parse("d70f7f9f-7f8f-0f4f-5f9f-7f8e9a0b1c78"), SectionName = "G" },
              new ClassSectionModel { Id = Guid.Parse("e81f8f0f-8f9f-1f5f-6f0f-8f9a0b1c2d90"), SectionName = "H" },
              new ClassSectionModel { Id = Guid.Parse("f92f9f1f-9f0f-2f6f-7f1f-9f0b1c2d3e12"), SectionName = "I" },
              new ClassSectionModel { Id = Guid.Parse("a03f0f2f-0f1f-3f7f-8f2f-0f1c2d3e4f34"), SectionName = "J" }
          );






        }
    }
}
