using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.JobSystem.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Clustered_Index_From_Guid_Column_And_Add_Clustered_Index_On_Integer_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "jobsystem");

            migrationBuilder.CreateTable(
                name: "Inbox",
                schema: "jobsystem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "JobOffers",
                schema: "jobsystem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffers", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Outbox",
                schema: "jobsystem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outbox", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "WorkLocations",
                schema: "jobsystem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLocations", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "JobOfferWorkLocation",
                schema: "jobsystem",
                columns: table => new
                {
                    JobOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferWorkLocation", x => new { x.JobOfferId, x.WorkLocationId })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_JobOfferWorkLocation_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalSchema: "jobsystem",
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOfferWorkLocation_WorkLocations_WorkLocationId",
                        column: x => x.WorkLocationId,
                        principalSchema: "jobsystem",
                        principalTable: "WorkLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_ClusterId",
                schema: "jobsystem",
                table: "Inbox",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_ClusterId",
                schema: "jobsystem",
                table: "JobOffers",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferWorkLocation_ClusterId",
                schema: "jobsystem",
                table: "JobOfferWorkLocation",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferWorkLocation_WorkLocationId",
                schema: "jobsystem",
                table: "JobOfferWorkLocation",
                column: "WorkLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Outbox_ClusterId",
                schema: "jobsystem",
                table: "Outbox",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkLocations_ClusterId",
                schema: "jobsystem",
                table: "WorkLocations",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inbox",
                schema: "jobsystem");

            migrationBuilder.DropTable(
                name: "JobOfferWorkLocation",
                schema: "jobsystem");

            migrationBuilder.DropTable(
                name: "Outbox",
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
