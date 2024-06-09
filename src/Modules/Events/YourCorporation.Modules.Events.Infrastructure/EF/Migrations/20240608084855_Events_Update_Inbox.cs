using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Events.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Events_Update_Inbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                schema: "events",
                table: "Inbox",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CorrelationId",
                schema: "events",
                table: "Inbox",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "events",
                table: "Inbox",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                schema: "events",
                table: "Inbox");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                schema: "events",
                table: "Inbox");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "events",
                table: "Inbox");
        }
    }
}
