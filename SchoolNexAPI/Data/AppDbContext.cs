using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data.Configuration;
using SchoolNexAPI.Models;
using SchoolNexAPI.Models.Student;

namespace SchoolNexAPI.Data
{
    public class AppDbContext : IdentityDbContext<AppUserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUserModel> AppUser { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        public DbSet<SchoolModel> Schools { get; set; }
        public DbSet<SchoolSettingsModel> SchoolSettings { get; set; }
        public DbSet<SchoolSubscriptionModel> SchoolSubscription { get; set; }
        public DbSet<SubscriptionTypeModel> SubscriptionType { get; set; }
        public DbSet<PaymentTransactionModel> PaymentTransactions { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<CustomFieldDefinitionModel> CustomFieldDefinitions { get; set; }
        public DbSet<StudentCustomFieldValueModel> StudentCustomFieldValues { get; set; }
        public DbSet<AcademicYearModel> AcademicYears { get; set; }
        public DbSet<StudentEnrollmentModel> StudentEnrollments { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }

        public DbSet<StudentAddressModel> StudentAddresses { get; set; }
        public DbSet<StudentParentModel> StudentParents { get; set; }
        public DbSet<StudentBankDetailsModel> StudentBanks { get; set; }
        public DbSet<StudentMedicalRecordModel> StudentMedicalRecords { get; set; }
        public DbSet<StudentPreviousSchoolModel> StudentPreviousSchools { get; set; }
        public DbSet<StudentTransportationModel> StudentTransportations { get; set; }
        public DbSet<StudentAdditionalDetailsModel> StudentAdditionalDetails { get; set; }
        public DbSet<FileRecordsModel> FileRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolSubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTransactionConfiguration());

            modelBuilder.ApplyConfiguration(new AcademicYearConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfigurations());
            modelBuilder.ApplyConfiguration(new StudentEnrollmentConfiguration());
            modelBuilder.ApplyConfiguration(new FileRecordsConfiguration());

            base.OnModelCreating(modelBuilder);


            /* It loops over all the entities and their foreign key relationships, and for each relationship,
            it sets the delete behavior to restrict. This means that if you try to delete a parent entity,
            it will not automatically delete the related child entities, and you will need to handle that manually.*/

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
