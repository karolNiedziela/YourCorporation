using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.JobSystem.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class JobSystem_Update_Inbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                schema: "jobsystem",
                table: "Inbox",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CorrelationId",
                schema: "jobsystem",
                table: "Inbox",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "jobsystem",
                table: "Inbox",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                schema: "jobsystem",
                table: "Inbox");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                schema: "jobsystem",
                table: "Inbox");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "jobsystem",
                table: "Inbox");
        }
    }
}
