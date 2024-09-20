using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class Update_PrintSetting_20220301 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TempName",
                table: "RpPrintSetting",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "模板名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempName",
                table: "RpPrintSetting");
        }
    }
}
