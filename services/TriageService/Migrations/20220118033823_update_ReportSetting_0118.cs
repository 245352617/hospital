using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class update_ReportSetting_0118 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Platform",
                table: "Dict_ReportSetting",
                nullable: false,
                defaultValue: 0,
                comment: "平台标识，1:院前急救，2：预检分诊，3：急诊管理");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Dict_ReportSetting");
        }
    }
}
