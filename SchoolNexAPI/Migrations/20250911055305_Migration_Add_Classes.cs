using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration_Add_Classes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClassSection",
                columns: new[] { "Id", "SectionName" },
                values: new object[,]
                {
                    { new Guid("a03f0f2f-0f1f-3f7f-8f2f-0f1c2d3e4f34"), "J" },
                    { new Guid("a47f4f6f-4f5d-7f1f-2e6f-4d5e6f7b9c12"), "D" },
                    { new Guid("b58f5f7f-5f6e-8f2f-3f7f-5e6f7c8d9e34"), "E" },
                    { new Guid("c69f6f8f-6f7f-9f3f-4f8f-6f7d8e9f0a56"), "F" },
                    { new Guid("d14f1f3e-1c2a-4f8e-9b3f-1a2b3c4d5e6f"), "A" },
                    { new Guid("d70f7f9f-7f8f-0f4f-5f9f-7f8e9a0b1c78"), "G" },
                    { new Guid("e25f2f4e-2d3b-4f9f-0c4d-2b3c4d5e6f70"), "B" },
                    { new Guid("e81f8f0f-8f9f-1f5f-6f0f-8f9a0b1c2d90"), "H" },
                    { new Guid("f36f3f5f-3e4c-4f0f-1d5e-3c4d5e6f7a81"), "C" },
                    { new Guid("f92f9f1f-9f0f-2f6f-7f1f-9f0b1c2d3e12"), "I" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("a03f0f2f-0f1f-3f7f-8f2f-0f1c2d3e4f34"));

            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("a47f4f6f-4f5d-7f1f-2e6f-4d5e6f7b9c12"));

            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("b58f5f7f-5f6e-8f2f-3f7f-5e6f7c8d9e34"));

            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("c69f6f8f-6f7f-9f3f-4f8f-6f7d8e9f0a56"));

            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("d14f1f3e-1c2a-4f8e-9b3f-1a2b3c4d5e6f"));

            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("d70f7f9f-7f8f-0f4f-5f9f-7f8e9a0b1c78"));

            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("e25f2f4e-2d3b-4f9f-0c4d-2b3c4d5e6f70"));

            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("e81f8f0f-8f9f-1f5f-6f0f-8f9a0b1c2d90"));

            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("f36f3f5f-3e4c-4f0f-1d5e-3c4d5e6f7a81"));

            migrationBuilder.DeleteData(
                table: "ClassSection",
                keyColumn: "Id",
                keyValue: new Guid("f92f9f1f-9f0f-2f6f-7f1f-9f0b1c2d3e12"));
        }
    }
}
