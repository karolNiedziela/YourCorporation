using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Recruitment_Add_JobApplication_Candidate_WorkLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrivateEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivatePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LinkedinUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobApplicationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobOfferName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobOfferSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkLocations",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobApplicationChosenWorkLocations",
                schema: "recruitment",
                columns: table => new
                {
                    JobApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationChosenWorkLocations", x => new { x.JobApplicationId, x.WorkLocationId });
                    table.ForeignKey(
                        name: "FK_JobApplicationChosenWorkLocations_JobApplications_JobApplicationId",
                        column: x => x.JobApplicationId,
                        principalSchema: "recruitment",
                        principalTable: "JobApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplicationChosenWorkLocations_WorkLocations_WorkLocationId",
                        column: x => x.WorkLocationId,
                        principalSchema: "recruitment",
                        principalTable: "WorkLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationChosenWorkLocations_JobApplicationId_WorkLocationId",
                schema: "recruitment",
                table: "JobApplicationChosenWorkLocations",
                columns: new[] { "JobApplicationId", "WorkLocationId" });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationChosenWorkLocations_WorkLocationId",
                schema: "recruitment",
                table: "JobApplicationChosenWorkLocations",
                column: "WorkLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "JobApplicationChosenWorkLocations",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "JobApplications",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "WorkLocations",
                schema: "recruitment");
        }
    }
}
