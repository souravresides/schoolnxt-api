using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration_StudentClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "Id", "ClassName" },
                values: new object[,]
                {
                    { new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), "UKG" },
                    { new Guid("12345678-1234-1234-1234-123456789abc"), "12" },
                    { new Guid("123e4567-e89b-12d3-a456-426614174000"), "2" },
                    { new Guid("16fd2706-8baf-433b-82eb-8c7fada847da"), "9" },
                    { new Guid("21ec2020-3aea-4069-a2dd-08002b30309d"), "LKG" },
                    { new Guid("3f2504e0-4f89-11d3-9a0c-0305e82c3301"), "Play School" },
                    { new Guid("4f5b5b6c-29c2-4a93-b0ef-0c1c9f1e5d4c"), "11" },
                    { new Guid("5a1a0c0a-44f8-4c8a-9b2d-8a4e6f0d1234"), "6" },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc964ff"), "Pre Nursery" },
                    { new Guid("6fa459ea-ee8a-3ca4-894e-db77e160355e"), "8" },
                    { new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), "Nursery" },
                    { new Guid("886313e1-3b8a-5372-9b90-0c9aee199e5d"), "10" },
                    { new Guid("9a0b1234-5678-4def-9012-abcdef123456"), "1" },
                    { new Guid("a8098c1a-f86e-11da-bd1a-00112444be1e"), "7" },
                    { new Guid("c56a4180-65aa-42ec-a945-5fd21dec0538"), "3" },
                    { new Guid("e02fd0e4-00fd-090a-ca30-0d00a0038ba0"), "5" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Class");
        }
    }
}
