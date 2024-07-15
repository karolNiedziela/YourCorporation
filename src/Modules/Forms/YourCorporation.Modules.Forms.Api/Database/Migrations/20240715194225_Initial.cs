using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Forms.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "forms");

            migrationBuilder.CreateTable(
                name: "EventForms",
                schema: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(108)", maxLength: 108, nullable: true),
                    IsUniqueSubmission = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inbox",
                schema: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOfferForms",
                schema: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(108)", maxLength: 108, nullable: true),
                    IsUniqueSubmission = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Outbox",
                schema: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkLocations",
                schema: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventSubmissions",
                schema: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSubmissions_EventForms_EventFormId",
                        column: x => x.EventFormId,
                        principalSchema: "forms",
                        principalTable: "EventForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobOfferSubmissions",
                schema: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobOfferFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CVUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOfferSubmissions_JobOfferForms_JobOfferFormId",
                        column: x => x.JobOfferFormId,
                        principalSchema: "forms",
                        principalTable: "JobOfferForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobOfferFormWorkLocation",
                schema: "forms",
                columns: table => new
                {
                    JobOfferFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferFormWorkLocation", x => new { x.JobOfferFormId, x.WorkLocationId });
                    table.ForeignKey(
                        name: "FK_JobOfferFormWorkLocation_JobOfferForms_JobOfferFormId",
                        column: x => x.JobOfferFormId,
                        principalSchema: "forms",
                        principalTable: "JobOfferForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOfferFormWorkLocation_WorkLocations_WorkLocationId",
                        column: x => x.WorkLocationId,
                        principalSchema: "forms",
                        principalTable: "WorkLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_EventForms_EventId",
                schema: "forms",
                table: "EventForms",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventSubmissions_Email",
                schema: "forms",
                table: "EventSubmissions",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubmissions_EventFormId",
                schema: "forms",
                table: "EventSubmissions",
                column: "EventFormId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferForms_JobOfferId",
                schema: "forms",
                table: "JobOfferForms",
                column: "JobOfferId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferFormWorkLocation_WorkLocationId",
                schema: "forms",
                table: "JobOfferFormWorkLocation",
                column: "WorkLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferSubmissionChosenWorkLocation_WorkLocationId",
                schema: "forms",
                table: "JobOfferSubmissionChosenWorkLocation",
                column: "WorkLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferSubmissions_Email",
                schema: "forms",
                table: "JobOfferSubmissions",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferSubmissions_JobOfferFormId",
                schema: "forms",
                table: "JobOfferSubmissions",
                column: "JobOfferFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventSubmissions",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "Inbox",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "JobOfferFormWorkLocation",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "JobOfferSubmissionChosenWorkLocation",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "Outbox",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "EventForms",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "JobOfferSubmissions",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "WorkLocations",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "JobOfferForms",
                schema: "forms");
        }
    }
}
