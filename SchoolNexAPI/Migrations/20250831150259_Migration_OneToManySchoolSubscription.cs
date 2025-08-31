using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolNexAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration_OneToManySchoolSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SchoolSubscription_SubscriptionTypeId",
                table: "SchoolSubscription");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSubscription_SubscriptionTypeId",
                table: "SchoolSubscription",
                column: "SubscriptionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SchoolSubscription_SubscriptionTypeId",
                table: "SchoolSubscription");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSubscription_SubscriptionTypeId",
                table: "SchoolSubscription",
                column: "SubscriptionTypeId",
                unique: true);
        }
    }
}
