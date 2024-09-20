using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddDataSourceFlagForReportSettingQueryOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSourceSql",
                table: "Dict_ReportSettingQueryOption");

            migrationBuilder.AddColumn<string>(
                name: "DataSource",
                table: "Dict_ReportSettingQueryOption",
                nullable: true,
                comment: "数据源");

            migrationBuilder.AddColumn<int>(
                name: "DataSourceFlag",
                table: "Dict_ReportSettingQueryOption",
                nullable: false,
                defaultValue: 0,
                comment: "数据源标识：0.sql  1.json字符串");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSource",
                table: "Dict_ReportSettingQueryOption");

            migrationBuilder.DropColumn(
                name: "DataSourceFlag",
                table: "Dict_ReportSettingQueryOption");

            migrationBuilder.AddColumn<string>(
                name: "DataSourceSql",
                table: "Dict_ReportSettingQueryOption",
                type: "nvarchar(max)",
                nullable: true,
                comment: "数据源：SQL");
        }
    }
}
