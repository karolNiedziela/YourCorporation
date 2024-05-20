using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Forms.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class Forms_Add_JobOfferSubmissionWorkLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobOfferSubmissionChosenWorkLocation",
                schema: "forms",
                columns: table => new
                {
                    JobOfferSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferSubmissionChosenWorkLocation", x => new { x.JobOfferSubmissionId, x.WorkLocationId });
                    table.ForeignKey(
                        name: "FK_JobOfferSubmissionChosenWorkLocation_JobOfferSubmissions_JobOfferSubmissionId",
                        column: x => x.JobOfferSubmissionId,
                        principalSchema: "forms",
                        principalTable: "JobOfferSubmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOfferSubmissionChosenWorkLocation_WorkLocations_WorkLocationId",
                        column: x => x.WorkLocationId,
                        principalSchema: "forms",
                        principalTable: "WorkLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferSubmissionChosenWorkLocation_WorkLocationId",
                schema: "forms",
                table: "JobOfferSubmissionChosenWorkLocation",
                column: "WorkLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobOfferSubmissionChosenWorkLocation",
                schema: "forms");
        }
    }
}
