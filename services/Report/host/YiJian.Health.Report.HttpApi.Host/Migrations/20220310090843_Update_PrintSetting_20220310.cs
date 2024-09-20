using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class Update_PrintSetting_20220310 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPreview",
                table: "RpPrintSetting",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否预览");

            migrationBuilder.AddColumn<string>(
                name: "Layout",
                table: "RpPrintSetting",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "布局");

            migrationBuilder.AddColumn<string>(
                name: "PageSizeCode",
                table: "RpPrintSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "纸张编码");

            migrationBuilder.AddColumn<decimal>(
                name: "PageSizeHeight",
                table: "RpPrintSetting",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "纸张高度");

            migrationBuilder.AddColumn<string>(
                name: "PageSizeWidth",
                table: "RpPrintSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "纸张宽度");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreview",
                table: "RpPrintSetting");

            migrationBuilder.DropColumn(
                name: "Layout",
                table: "RpPrintSetting");

            migrationBuilder.DropColumn(
                name: "PageSizeCode",
                table: "RpPrintSetting");

            migrationBuilder.DropColumn(
                name: "PageSizeHeight",
                table: "RpPrintSetting");

            migrationBuilder.DropColumn(
                name: "PageSizeWidth",
                table: "RpPrintSetting");
        }
    }
}
