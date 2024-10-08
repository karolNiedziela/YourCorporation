﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Clustered_Index_From_Guid_Column_And_Add_Clustered_Index_On_Integer_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "recruitment");

            migrationBuilder.CreateTable(
                name: "ContactStatuses",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Substatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStatuses", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
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
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLocations", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
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
                    ContactStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactStatuses_ContactStatusId",
                        column: x => x.ContactStatusId,
                        principalSchema: "recruitment",
                        principalTable: "ContactStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
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
                    WorkLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationChosenWorkLocation", x => new { x.JobApplicationId, x.WorkLocationId })
                        .Annotation("SqlServer:Clustered", false);
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

            migrationBuilder.InsertData(
                schema: "recruitment",
                table: "ContactStatuses",
                columns: new[] { "Id", "Status", "Substatus" },
                values: new object[] { new Guid("0380bc27-18de-4683-bd10-37c267f4f979"), "Not Verified", null });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ClusterId",
                schema: "recruitment",
                table: "Contacts",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactStatusId",
                schema: "recruitment",
                table: "Contacts",
                column: "ContactStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStatuses_ClusterId",
                schema: "recruitment",
                table: "ContactStatuses",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_ClusterId",
                schema: "recruitment",
                table: "Inbox",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationChosenWorkLocation_ClusterId",
                schema: "recruitment",
                table: "JobApplicationChosenWorkLocation",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationChosenWorkLocation_WorkLocationId",
                schema: "recruitment",
                table: "JobApplicationChosenWorkLocation",
                column: "WorkLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ClusterId",
                schema: "recruitment",
                table: "JobApplications",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ContactId",
                schema: "recruitment",
                table: "JobApplications",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Outbox_ClusterId",
                schema: "recruitment",
                table: "Outbox",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkLocations_ClusterId",
                schema: "recruitment",
                table: "WorkLocations",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);
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
                name: "ContactStatuses",
                schema: "recruitment");
        }
    }
}
