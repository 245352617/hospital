using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class Update_PrintSetting_20220311 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParamUrl",
                table: "RpPrintSetting",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "传参Url",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "传参Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParamUrl",
                table: "RpPrintSetting",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "传参Url",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "传参Url");
        }
    }
}
