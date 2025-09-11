using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration_Payment_Method : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "Id", "MethodName", "Value" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Cash", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Cheque", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "BankTransfer", 2 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Online", 3 }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatus",
                columns: new[] { "Id", "StatusName", "Value" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Pending", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Confirmed", 1 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Failed", 2 },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Refunded", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "PaymentStatus");
        }
    }
}
