using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Substatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStatus", x => x.Id)
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
                name: "RecruitmentQueue",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentQueue", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "WorkLocation",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLocation", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrivateEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrivatePhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LinkedinUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Contact_ContactStatus_ContactStatusId",
                        column: x => x.ContactStatusId,
                        principalSchema: "recruitment",
                        principalTable: "ContactStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecruitmentQueueWorkLocation",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecruitmentQueueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentQueueWorkLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecruitmentQueueWorkLocation_RecruitmentQueue_RecruitmentQueueId",
                        column: x => x.RecruitmentQueueId,
                        principalSchema: "recruitment",
                        principalTable: "RecruitmentQueue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplication",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CVUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    JobOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobOfferSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplication", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_JobApplication_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "recruitment",
                        principalTable: "Contact",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobApplicationChosenWorkLocation",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationChosenWorkLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplicationChosenWorkLocation_JobApplication_JobApplicationId",
                        column: x => x.JobApplicationId,
                        principalSchema: "recruitment",
                        principalTable: "JobApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplicationRecruitmentQueue",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecruitmentQueueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationRecruitmentQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplicationRecruitmentQueue_JobApplication_JobApplicationId",
                        column: x => x.JobApplicationId,
                        principalSchema: "recruitment",
                        principalTable: "JobApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "recruitment",
                table: "ContactStatus",
                columns: new[] { "Id", "Status", "Substatus" },
                values: new object[] { new Guid("0380bc27-18de-4683-bd10-37c267f4f979"), "Applicant", "Not Verified" });

            migrationBuilder.InsertData(
                schema: "recruitment",
                table: "RecruitmentQueue",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("4a053a6e-6927-41c9-a3d9-3e4b74b5f1ca"), "Any" });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ClusterId",
                schema: "recruitment",
                table: "Contact",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactStatusId",
                schema: "recruitment",
                table: "Contact",
                column: "ContactStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStatus_ClusterId",
                schema: "recruitment",
                table: "ContactStatus",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_ClusterId",
                schema: "recruitment",
                table: "Inbox",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_ClusterId",
                schema: "recruitment",
                table: "JobApplication",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_ContactId",
                schema: "recruitment",
                table: "JobApplication",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationChosenWorkLocation_JobApplicationId",
                schema: "recruitment",
                table: "JobApplicationChosenWorkLocation",
                column: "JobApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationRecruitmentQueue_JobApplicationId",
                schema: "recruitment",
                table: "JobApplicationRecruitmentQueue",
                column: "JobApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Outbox_ClusterId",
                schema: "recruitment",
                table: "Outbox",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentQueue_ClusterId",
                schema: "recruitment",
                table: "RecruitmentQueue",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentQueueWorkLocation_RecruitmentQueueId",
                schema: "recruitment",
                table: "RecruitmentQueueWorkLocation",
                column: "RecruitmentQueueId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkLocation_ClusterId",
                schema: "recruitment",
                table: "WorkLocation",
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
                name: "JobApplicationRecruitmentQueue",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "Outbox",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "RecruitmentQueueWorkLocation",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "WorkLocation",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "JobApplication",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "RecruitmentQueue",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "ContactStatus",
                schema: "recruitment");
        }
    }
}
