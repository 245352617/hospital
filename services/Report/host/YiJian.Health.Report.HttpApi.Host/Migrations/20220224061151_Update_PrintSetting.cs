using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class Update_PrintSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationName",
                table: "RpPrintSetting",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "创建人名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationName",
                table: "RpPrintSetting");
        }
    }
}
