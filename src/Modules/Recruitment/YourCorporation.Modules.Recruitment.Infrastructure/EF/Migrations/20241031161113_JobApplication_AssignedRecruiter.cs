using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class JobApplication_AssignedRecruiter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedRecruiterFullName",
                schema: "recruitment",
                table: "JobApplication",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedRecruiterId",
                schema: "recruitment",
                table: "JobApplication",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedRecruiterFullName",
                schema: "recruitment",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "AssignedRecruiterId",
                schema: "recruitment",
                table: "JobApplication");
        }
    }
}
