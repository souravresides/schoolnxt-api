using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c49d443-aca7-4851-8f54-b83f7d5e4453");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e627121-ab18-4b0d-bc9f-edf78c9c43b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2efe7769-3f8a-4fa4-9afa-6205751131a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36123c89-64c4-430c-91a4-8ea8c0e75741");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1821b70-c285-419c-915e-2134a9f98ea1");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3275882d-2998-4952-9520-670c88a00c81", null, "SchoolAdmin", "SCHOOLADMIN" },
                    { "35cd16cb-e048-4db1-a6e0-7ab9e0882155", null, "Parents", "PARENTS" },
                    { "446355ae-1e92-4df9-bd6f-db51322f6298", null, "SuperAdmin", "SUPERADMIN" },
                    { "50e8efc3-0966-444c-9da6-596ec27bc7b2", null, "Teacher", "TEACHER" },
                    { "fcc5069f-610a-4165-88b6-adfbcba13121", null, "Student", "STUDENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3275882d-2998-4952-9520-670c88a00c81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35cd16cb-e048-4db1-a6e0-7ab9e0882155");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "446355ae-1e92-4df9-bd6f-db51322f6298");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50e8efc3-0966-444c-9da6-596ec27bc7b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcc5069f-610a-4165-88b6-adfbcba13121");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c49d443-aca7-4851-8f54-b83f7d5e4453", null, "Student", "STUDENT" },
                    { "2e627121-ab18-4b0d-bc9f-edf78c9c43b3", null, "SuperAdmin", "SUPERADMIN" },
                    { "2efe7769-3f8a-4fa4-9afa-6205751131a1", null, "SchoolAdmin", "SCHOOLADMIN" },
                    { "36123c89-64c4-430c-91a4-8ea8c0e75741", null, "Teacher", "TEACHER" },
                    { "a1821b70-c285-419c-915e-2134a9f98ea1", null, "Parents", "PARENTS" }
                });
        }
    }
}
