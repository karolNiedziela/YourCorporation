using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class JobApplication_ContactId_As_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactStatuses_ContactStatusId",
                schema: "recruitment",
                table: "Contacts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactStatusId",
                schema: "recruitment",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactStatuses_ContactStatusId",
                schema: "recruitment",
                table: "Contacts",
                column: "ContactStatusId",
                principalSchema: "recruitment",
                principalTable: "ContactStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactStatuses_ContactStatusId",
                schema: "recruitment",
                table: "Contacts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactStatusId",
                schema: "recruitment",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactStatuses_ContactStatusId",
                schema: "recruitment",
                table: "Contacts",
                column: "ContactStatusId",
                principalSchema: "recruitment",
                principalTable: "ContactStatuses",
                principalColumn: "Id");
        }
    }
}
