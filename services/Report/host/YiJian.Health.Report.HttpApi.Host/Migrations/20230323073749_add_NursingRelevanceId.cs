using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Health.Report.Migrations
{
    public partial class add_NursingRelevanceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NursingRelevanceId",
                table: "RpNursingRecord",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "护理记录关联Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NursingRelevanceId",
                table: "RpNursingRecord");
        }
    }
}
