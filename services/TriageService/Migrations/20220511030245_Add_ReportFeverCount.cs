using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Add_ReportFeverCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportFeverCount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeptName = table.Column<string>(maxLength: 60, nullable: true),
                    TriageDate = table.Column<DateTime>(nullable: false),
                    Sort = table.Column<int>(nullable: true),
                    DeathCount = table.Column<int>(nullable: false),
                    DeathCountChanged = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFeverCount", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportFeverCount");
        }
    }
}
