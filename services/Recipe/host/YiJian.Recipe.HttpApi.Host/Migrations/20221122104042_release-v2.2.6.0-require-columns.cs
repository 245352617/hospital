using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class releasev2260requirecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DailyFrequency",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "每日次(HIS的配置频次信息)",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "每日次(HIS的配置频次信息)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DailyFrequency",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "每日次(HIS的配置频次信息)",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "每日次(HIS的配置频次信息)");
        }
    }
}
