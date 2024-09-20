using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class Update_PrintSetting_20220316 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Layout",
                table: "RpPrintSetting",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "布局",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "布局");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Layout",
                table: "RpPrintSetting",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "布局",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "布局");
        }
    }
}
