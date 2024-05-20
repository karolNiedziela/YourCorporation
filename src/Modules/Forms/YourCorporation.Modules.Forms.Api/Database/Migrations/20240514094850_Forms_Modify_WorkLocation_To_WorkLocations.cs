using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Forms.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class Forms_Modify_WorkLocation_To_WorkLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferFormWorkLocation_WorkLocation_WorkLocationId",
                schema: "forms",
                table: "JobOfferFormWorkLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkLocation",
                schema: "forms",
                table: "WorkLocation");

            migrationBuilder.RenameTable(
                name: "WorkLocation",
                schema: "forms",
                newName: "WorkLocations",
                newSchema: "forms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkLocations",
                schema: "forms",
                table: "WorkLocations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferFormWorkLocation_WorkLocations_WorkLocationId",
                schema: "forms",
                table: "JobOfferFormWorkLocation",
                column: "WorkLocationId",
                principalSchema: "forms",
                principalTable: "WorkLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferFormWorkLocation_WorkLocations_WorkLocationId",
                schema: "forms",
                table: "JobOfferFormWorkLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkLocations",
                schema: "forms",
                table: "WorkLocations");

            migrationBuilder.RenameTable(
                name: "WorkLocations",
                schema: "forms",
                newName: "WorkLocation",
                newSchema: "forms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkLocation",
                schema: "forms",
                table: "WorkLocation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferFormWorkLocation_WorkLocation_WorkLocationId",
                schema: "forms",
                table: "JobOfferFormWorkLocation",
                column: "WorkLocationId",
                principalSchema: "forms",
                principalTable: "WorkLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
