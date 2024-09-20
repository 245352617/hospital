using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class update_printsetting_202205 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageSizeHeight",
                table: "RpPrintSetting");

            migrationBuilder.DropColumn(
                name: "PageSizeWidth",
                table: "RpPrintSetting");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
