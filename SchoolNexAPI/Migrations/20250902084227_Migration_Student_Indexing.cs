using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration_Student_Indexing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FullName",
                table: "Students",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PhoneNumber",
                table: "Students",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId_CreatedAt",
                table: "Students",
                columns: new[] { "SchoolId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId_IsActive",
                table: "Students",
                columns: new[] { "SchoolId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId_Year_IsActive",
                table: "Students",
                columns: new[] { "SchoolId", "AcademicYearId", "IsActive" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_Email",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FullName",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_PhoneNumber",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolId_CreatedAt",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolId_IsActive",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolId_Year_IsActive",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId",
                table: "Students",
                column: "SchoolId");
        }
    }
}
