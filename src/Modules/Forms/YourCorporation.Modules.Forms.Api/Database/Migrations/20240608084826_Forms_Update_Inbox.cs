using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Forms.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class Forms_Update_Inbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVUrl",
                schema: "forms",
                table: "JobOfferSubmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                schema: "forms",
                table: "Inbox",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CorrelationId",
                schema: "forms",
                table: "Inbox",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "forms",
                table: "Inbox",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVUrl",
                schema: "forms",
                table: "JobOfferSubmissions");

            migrationBuilder.DropColumn(
                name: "Content",
                schema: "forms",
                table: "Inbox");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                schema: "forms",
                table: "Inbox");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "forms",
                table: "Inbox");
        }
    }
}
