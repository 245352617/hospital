using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class DevExpressReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrintMethod",
                table: "RpPrintSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "打印方式");

            migrationBuilder.AddColumn<string>(
                name: "TempType",
                table: "RpPrintSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "模板类型");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrintMethod",
                table: "RpPrintSetting");

            migrationBuilder.DropColumn(
                name: "TempType",
                table: "RpPrintSetting");
        }
    }
}
