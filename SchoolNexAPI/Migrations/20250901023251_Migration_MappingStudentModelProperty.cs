using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration_MappingStudentModelProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentParents_Students_StudentModelId",
                table: "StudentParents");

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

            migrationBuilder.DropIndex(
                name: "IX_StudentParents_StudentModelId",
                table: "StudentParents");

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

            migrationBuilder.DropColumn(
                name: "StudentModelId",
                table: "StudentParents");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTransportations_StudentId",
                table: "StudentTransportations",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentPreviousSchools_StudentId",
                table: "StudentPreviousSchools",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentParents_StudentId",
                table: "StudentParents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMedicalRecords_StudentId",
                table: "StudentMedicalRecords",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentBanks_StudentId",
                table: "StudentBanks",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddresses_StudentId",
                table: "StudentAddresses",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAdditionalDetails_StudentId",
                table: "StudentAdditionalDetails",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAdditionalDetails_Students_StudentId",
                table: "StudentAdditionalDetails",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddresses_Students_StudentId",
                table: "StudentAddresses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentBanks_Students_StudentId",
                table: "StudentBanks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentMedicalRecords_Students_StudentId",
                table: "StudentMedicalRecords",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParents_Students_StudentId",
                table: "StudentParents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPreviousSchools_Students_StudentId",
                table: "StudentPreviousSchools",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTransportations_Students_StudentId",
                table: "StudentTransportations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAdditionalDetails_Students_StudentId",
                table: "StudentAdditionalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddresses_Students_StudentId",
                table: "StudentAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentBanks_Students_StudentId",
                table: "StudentBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentMedicalRecords_Students_StudentId",
                table: "StudentMedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentParents_Students_StudentId",
                table: "StudentParents");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPreviousSchools_Students_StudentId",
                table: "StudentPreviousSchools");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTransportations_Students_StudentId",
                table: "StudentTransportations");

            migrationBuilder.DropIndex(
                name: "IX_StudentTransportations_StudentId",
                table: "StudentTransportations");

            migrationBuilder.DropIndex(
                name: "IX_StudentPreviousSchools_StudentId",
                table: "StudentPreviousSchools");

            migrationBuilder.DropIndex(
                name: "IX_StudentParents_StudentId",
                table: "StudentParents");

            migrationBuilder.DropIndex(
                name: "IX_StudentMedicalRecords_StudentId",
                table: "StudentMedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_StudentBanks_StudentId",
                table: "StudentBanks");

            migrationBuilder.DropIndex(
                name: "IX_StudentAddresses_StudentId",
                table: "StudentAddresses");

            migrationBuilder.DropIndex(
                name: "IX_StudentAdditionalDetails_StudentId",
                table: "StudentAdditionalDetails");

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

            migrationBuilder.AddColumn<Guid>(
                name: "StudentModelId",
                table: "StudentParents",
                type: "uniqueidentifier",
                nullable: true);

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
                name: "FK_StudentParents_Students_StudentModelId",
                table: "StudentParents",
                column: "StudentModelId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
