using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionType",
                keyColumn: "Id",
                keyValue: new Guid("d1f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4a"));

            migrationBuilder.DeleteData(
                table: "SubscriptionType",
                keyColumn: "Id",
                keyValue: new Guid("e2f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4b"));

            migrationBuilder.DeleteData(
                table: "SubscriptionType",
                keyColumn: "Id",
                keyValue: new Guid("f3f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4c"));

            migrationBuilder.AddColumn<string>(
                name: "FeaturesJson",
                table: "SubscriptionType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturesJson",
                table: "SubscriptionType");

            migrationBuilder.InsertData(
                table: "SubscriptionType",
                columns: new[] { "Id", "Description", "IsActive", "MaxEmployees", "MaxStudents", "Name", "PricePerMonth" },
                values: new object[,]
                {
                    { new Guid("d1f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4a"), "Basic subscription with limited features.", true, 10, 50, "Basic", 999.99m },
                    { new Guid("e2f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4b"), "Standard subscription with additional features.", true, 50, 200, "Standard", 4999.99m },
                    { new Guid("f3f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4c"), "Premium subscription with all features included.", true, 100, 500, "Premium", 7999.99m }
                });
        }
    }
}
