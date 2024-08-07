﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Recruitment_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "recruitment");

            migrationBuilder.CreateTable(
                name: "ContactStatus",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Substatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inbox",
                schema: "recruitment",
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
                name: "Outbox",
                schema: "recruitment",
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
                name: "Contacts",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrivateEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivatePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LinkedinUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactStatus_ContactStatusId",
                        column: x => x.ContactStatusId,
                        principalSchema: "recruitment",
                        principalTable: "ContactStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CVUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobApplicationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobOfferName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobOfferSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApplicationLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "recruitment",
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobApplicationChosenWorkLocation",
                schema: "recruitment",
                columns: table => new
                {
                    JobApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationChosenWorkLocation", x => new { x.JobApplicationId, x.WorkLocationId });
                    table.ForeignKey(
                        name: "FK_JobApplicationChosenWorkLocation_JobApplications_JobApplicationId",
                        column: x => x.JobApplicationId,
                        principalSchema: "recruitment",
                        principalTable: "JobApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplicationChosenWorkLocation_WorkLocations_WorkLocationId",
                        column: x => x.WorkLocationId,
                        principalSchema: "recruitment",
                        principalTable: "WorkLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactStatusId",
                schema: "recruitment",
                table: "Contacts",
                column: "ContactStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationChosenWorkLocation_WorkLocationId",
                schema: "recruitment",
                table: "JobApplicationChosenWorkLocation",
                column: "WorkLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ContactId",
                schema: "recruitment",
                table: "JobApplications",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inbox",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "JobApplicationChosenWorkLocation",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "Outbox",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "JobApplications",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "WorkLocations",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "ContactStatus",
                schema: "recruitment");
        }
    }
}
