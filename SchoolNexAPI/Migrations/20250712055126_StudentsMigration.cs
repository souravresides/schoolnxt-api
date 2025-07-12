using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class StudentsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AcademicYearId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "StudentCustomFieldValues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CustomFieldDefinitions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CustomFieldDefinitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AcademicYearId",
                table: "Students",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCustomFieldValues_SchoolId",
                table: "StudentCustomFieldValues",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCustomFieldValues_Schools_SchoolId",
                table: "StudentCustomFieldValues",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AcademicYears_AcademicYearId",
                table: "Students",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCustomFieldValues_Schools_SchoolId",
                table: "StudentCustomFieldValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AcademicYears_AcademicYearId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AcademicYearId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_StudentCustomFieldValues_SchoolId",
                table: "StudentCustomFieldValues");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "StudentCustomFieldValues");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CustomFieldDefinitions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CustomFieldDefinitions");
        }
    }
}
