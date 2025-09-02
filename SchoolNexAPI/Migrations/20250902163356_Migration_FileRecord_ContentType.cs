using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration_FileRecord_ContentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "FileRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "FileRecords");
        }
    }
}
