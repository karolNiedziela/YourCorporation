using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Forms.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class Forms_Add_JobOffer_JobOfferSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "JobOfferSubmissions",
                schema: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobOfferFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferForms_JobOfferId",
                schema: "forms",
                table: "JobOfferForms",
                column: "JobOfferId",
                unique: true);

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
                name: "JobOfferSubmissions",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "JobOfferForms",
                schema: "forms");
        }
    }
}
