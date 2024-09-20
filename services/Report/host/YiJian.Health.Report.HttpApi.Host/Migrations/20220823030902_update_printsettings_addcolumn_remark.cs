using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class update_printsettings_addcolumn_remark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "RpPrintSetting",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "备注");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "RpPrintSetting");
        }
    }
}
