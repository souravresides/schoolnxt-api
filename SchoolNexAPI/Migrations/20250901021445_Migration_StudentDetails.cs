using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration_StudentDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AcademicYears_AcademicYearId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AcademicYearId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "MotherName",
                table: "Students",
                newName: "PhotoPath");

            migrationBuilder.AddColumn<Guid>(
                name: "AdditionalDetailsId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BankDetailsId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MedicalRecordId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PreviousSchoolId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TransportationId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StudentAdditionalDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightToEducation = table.Column<bool>(type: "bit", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BPLStudent = table.Column<bool>(type: "bit", nullable: false),
                    BPLCardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PwD = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfDisability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherTongue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SingleParentChild = table.Column<bool>(type: "bit", nullable: false),
                    SingleParent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SponsoredStudent = table.Column<bool>(type: "bit", nullable: false),
                    SponsorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAdditionalDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPermanentSameAsCurrent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentBanks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IFSC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountHolderName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentMedicalRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BMI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PulseRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Haemoglobin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COVIDVaccination = table.Column<bool>(type: "bit", nullable: false),
                    ChildImmunisation = table.Column<bool>(type: "bit", nullable: false),
                    ImmunisationRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMedicalRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentParents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentParents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentParents_Students_StudentModelId",
                        column: x => x.StudentModelId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentPreviousSchools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediumOfInstruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastClassPassed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentageOrGrade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPreviousSchools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentTransportations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NeedsTransportation = table.Column<bool>(type: "bit", nullable: false),
                    PickupPoint = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTransportations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_AdditionalDetailsId",
                table: "Students",
                column: "AdditionalDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AddressId",
                table: "Students",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BankDetailsId",
                table: "Students",
                column: "BankDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MedicalRecordId",
                table: "Students",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PreviousSchoolId",
                table: "Students",
                column: "PreviousSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_TransportationId",
                table: "Students",
                column: "TransportationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentParents_StudentModelId",
                table: "StudentParents",
                column: "StudentModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentAdditionalDetails_AdditionalDetailsId",
                table: "Students",
                column: "AdditionalDetailsId",
                principalTable: "StudentAdditionalDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentAddresses_AddressId",
                table: "Students",
                column: "AddressId",
                principalTable: "StudentAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentBanks_BankDetailsId",
                table: "Students",
                column: "BankDetailsId",
                principalTable: "StudentBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentMedicalRecords_MedicalRecordId",
                table: "Students",
                column: "MedicalRecordId",
                principalTable: "StudentMedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentPreviousSchools_PreviousSchoolId",
                table: "Students",
                column: "PreviousSchoolId",
                principalTable: "StudentPreviousSchools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentTransportations_TransportationId",
                table: "Students",
                column: "TransportationId",
                principalTable: "StudentTransportations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentAdditionalDetails_AdditionalDetailsId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentAddresses_AddressId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentBanks_BankDetailsId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentMedicalRecords_MedicalRecordId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentPreviousSchools_PreviousSchoolId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentTransportations_TransportationId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "StudentAdditionalDetails");

            migrationBuilder.DropTable(
                name: "StudentAddresses");

            migrationBuilder.DropTable(
                name: "StudentBanks");

            migrationBuilder.DropTable(
                name: "StudentMedicalRecords");

            migrationBuilder.DropTable(
                name: "StudentParents");

            migrationBuilder.DropTable(
                name: "StudentPreviousSchools");

            migrationBuilder.DropTable(
                name: "StudentTransportations");

            migrationBuilder.DropIndex(
                name: "IX_Students_AdditionalDetailsId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AddressId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_BankDetailsId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_MedicalRecordId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_PreviousSchoolId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_TransportationId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AdditionalDetailsId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BankDetailsId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MedicalRecordId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PreviousSchoolId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TransportationId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Students",
                newName: "MotherName");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AcademicYearId",
                table: "Students",
                column: "AcademicYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AcademicYears_AcademicYearId",
                table: "Students",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
