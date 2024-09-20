using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class update_TriageConfig_20220119 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Dict_ReportSetting");

            migrationBuilder.AddColumn<int>(
                name: "Platform",
                table: "Dict_TriageConfig",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Dict_TriageConfig");

            migrationBuilder.AddColumn<int>(
                name: "Platform",
                table: "Dict_ReportSetting",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "平台标识，1:院前急救，2：预检分诊，3：急诊管理");
        }
    }
}
