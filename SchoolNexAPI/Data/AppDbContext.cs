using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data.Configuration;
using SchoolNexAPI.Models;
using SchoolNexAPI.Models.Fees;
using SchoolNexAPI.Models.Student;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Data
{
    public class AppDbContext : IdentityDbContext<AppUserModel>
    {
        private readonly ITenantContext _tenant;

        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantContext tenant) : base(options)
        {
            this._tenant = tenant;
        }

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
        public DbSet<StudentEnrollmentModel> StudentEnrollments { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<EmployeePreviousEmployment> EmployeePreviousEmployment { get; set; }

        public DbSet<StudentAddressModel> StudentAddresses { get; set; }
        public DbSet<StudentParentModel> StudentParents { get; set; }
        public DbSet<StudentBankDetailsModel> StudentBanks { get; set; }
        public DbSet<StudentMedicalRecordModel> StudentMedicalRecords { get; set; }
        public DbSet<StudentPreviousSchoolModel> StudentPreviousSchools { get; set; }
        public DbSet<StudentTransportationModel> StudentTransportations { get; set; }
        public DbSet<StudentAdditionalDetailsModel> StudentAdditionalDetails { get; set; }
        public DbSet<FileRecordsModel> FileRecords { get; set; }
        //public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<StudentClassModel> Class { get; set; }
        public DbSet<ClassSectionModel> ClassSection { get; set; }


        //Fees
        public DbSet<PaymentMethodModel> PaymentMethod { get; set; }
        public DbSet<PaymentStatusModel> PaymentStatus { get; set; }
        public DbSet<FeeHead> FeeHeads { get; set; }
        public DbSet<FeeStructure> FeeStructures { get; set; }
        public DbSet<FeeStructureItem> FeeStructureItems { get; set; }
        public DbSet<StudentFeePlan> StudentFeePlans { get; set; }
        public DbSet<StudentFeePlanLine> StudentFeePlanLines { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentAllocation> PaymentAllocations { get; set; }
        public DbSet<Adjustment> Adjustments { get; set; }
        public DbSet<InvoiceSequence> InvoiceSequences { get; set; }
        public DbSet<AcademicYearModel> AcademicYears { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();    

            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolSubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTransactionConfiguration());

            modelBuilder.ApplyConfiguration(new StudentConfigurations());
            modelBuilder.ApplyConfiguration(new StudentEnrollmentConfiguration());
            modelBuilder.ApplyConfiguration(new FileRecordsConfiguration());

            //Fees
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration(_tenant));
            modelBuilder.ApplyConfiguration(new PaymentConfiguration(_tenant));
            modelBuilder.ApplyConfiguration(new StudentFeeConfiguration(_tenant));
            modelBuilder.ApplyConfiguration(new AdjustmentConfiguration(_tenant));
            modelBuilder.ApplyConfiguration(new FeeStructureConfiguration(_tenant));

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            modelBuilder.ApplyConfiguration(new StudentClassConfiguration());
            modelBuilder.ApplyConfiguration(new ClassSectionConfiguration());

            base.OnModelCreating(modelBuilder);


            /* It loops over all the entities and their foreign key relationships, and for each relationship,
            it sets the delete behavior to restrict. This means that if you try to delete a parent entity,
            it will not automatically delete the related child entities, and you will need to handle that manually.*/

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            /* This code iterates through all the entities in the model and checks each property. If a property is of type decimal,
             */
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties().Where(p => p.ClrType == typeof(decimal)))
                {
                    property.SetPrecision(18);
                    property.SetScale(2);
                }
            }
        }
    }
}
