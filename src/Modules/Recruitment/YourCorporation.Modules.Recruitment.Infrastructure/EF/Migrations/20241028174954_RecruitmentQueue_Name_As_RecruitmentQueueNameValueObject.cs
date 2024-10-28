using System;
using Microsoft.EntityFrameworkCore.Migrations;
using YourCorporation.Modules.Recruitment.Core.Queues;

#nullable disable

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class RecruitmentQueue_Name_As_RecruitmentQueueNameValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "recruitment",
                table: "RecruitmentQueue",
                keyColumn: "Id",
                keyValue: new Guid("4a053a6e-6927-41c9-a3d9-3e4b74b5f1ca"));

            migrationBuilder.InsertData(
               table: "RecruitmentQueue",
               columns: new[] { "Id", "Name" },
               values: new object[] { RecruitmentQueue.Any.Id.Value, "Any" }
       );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "recruitment",
                table: "RecruitmentQueue",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("4a053a6e-6927-41c9-a3d9-3e4b74b5f1ca"), "Any" });

            migrationBuilder.DeleteData(
                table: "RecruitmentQueue",
                keyColumn: "Id",
                keyValue: RecruitmentQueue.Any.Id.Value
        );
        }
    }
}
