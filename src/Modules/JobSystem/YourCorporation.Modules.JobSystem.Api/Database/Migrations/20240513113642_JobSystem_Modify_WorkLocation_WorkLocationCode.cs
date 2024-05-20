using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourCorporation.Modules.JobSystem.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class JobSystem_Modify_WorkLocation_WorkLocationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "jobsystem",
                table: "WorkLocations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "jobsystem",
                table: "WorkLocations");
        }
    }
}
