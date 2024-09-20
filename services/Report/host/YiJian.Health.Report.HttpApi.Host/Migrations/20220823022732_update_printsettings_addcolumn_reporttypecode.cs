using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class update_printsettings_addcolumn_reporttypecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportTypeCode",
                table: "RpPrintSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "单据类型编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportTypeCode",
                table: "RpPrintSetting");
        }
    }
}
