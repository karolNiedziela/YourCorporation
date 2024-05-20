using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Forms.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class Forms_Add_WorkLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkLocation",
                schema: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLocation", x => x.Id);
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
                        name: "FK_JobOfferFormWorkLocation_WorkLocation_WorkLocationId",
                        column: x => x.WorkLocationId,
                        principalSchema: "forms",
                        principalTable: "WorkLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferFormWorkLocation_WorkLocationId",
                schema: "forms",
                table: "JobOfferFormWorkLocation",
                column: "WorkLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobOfferFormWorkLocation",
                schema: "forms");

            migrationBuilder.DropTable(
                name: "WorkLocation",
                schema: "forms");
        }
    }
}
