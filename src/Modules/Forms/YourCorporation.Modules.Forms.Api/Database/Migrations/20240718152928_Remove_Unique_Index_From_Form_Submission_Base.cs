using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Forms.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Unique_Index_From_Form_Submission_Base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobOfferSubmissions_Email",
                schema: "forms",
                table: "JobOfferSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_EventSubmissions_Email",
                schema: "forms",
                table: "EventSubmissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JobOfferSubmissions_Email",
                schema: "forms",
                table: "JobOfferSubmissions",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubmissions_Email",
                schema: "forms",
                table: "EventSubmissions",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }
    }
}
