using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.JobSystem.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class JobSystem_Add_JobOffer_And_WorkLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "jobsystem");

            migrationBuilder.CreateTable(
                name: "JobOffers",
                schema: "jobsystem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkLocations",
                schema: "jobsystem",
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
                name: "JobOfferWorkLocation",
                schema: "jobsystem",
                columns: table => new
                {
                    JobOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkLocationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferWorkLocation", x => new { x.JobOfferId, x.WorkLocationsId });
                    table.ForeignKey(
                        name: "FK_JobOfferWorkLocation_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalSchema: "jobsystem",
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOfferWorkLocation_WorkLocations_WorkLocationsId",
                        column: x => x.WorkLocationsId,
                        principalSchema: "jobsystem",
                        principalTable: "WorkLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferWorkLocation_WorkLocationsId",
                schema: "jobsystem",
                table: "JobOfferWorkLocation",
                column: "WorkLocationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobOfferWorkLocation",
                schema: "jobsystem");

            migrationBuilder.DropTable(
                name: "JobOffers",
                schema: "jobsystem");

            migrationBuilder.DropTable(
                name: "WorkLocations",
                schema: "jobsystem");
        }
    }
}
