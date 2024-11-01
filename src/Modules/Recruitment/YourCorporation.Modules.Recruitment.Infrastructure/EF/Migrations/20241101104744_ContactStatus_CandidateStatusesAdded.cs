using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class ContactStatus_CandidateStatusesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "recruitment",
                table: "ContactStatus",
                columns: new[] { "Id", "Status", "Substatus" },
                values: new object[,]
                {
                    { new Guid("92845a34-6931-4316-aec9-a9dd2799f3ad"), "Candidate", "To contact" },
                    { new Guid("ccc70ee3-3f85-4df1-9761-9d5d1eff54e5"), "Candidate", "Rejected" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "recruitment",
                table: "ContactStatus",
                keyColumn: "Id",
                keyValue: new Guid("92845a34-6931-4316-aec9-a9dd2799f3ad"));

            migrationBuilder.DeleteData(
                schema: "recruitment",
                table: "ContactStatus",
                keyColumn: "Id",
                keyValue: new Guid("ccc70ee3-3f85-4df1-9761-9d5d1eff54e5"));
        }
    }
}
