using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2271_Update_NursingSettingHeader_InputType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InputType",
                table: "RpNursingSettingHeader",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "表头类型");

            migrationBuilder.AddColumn<bool>(
                name: "IsCarryInputBox",
                table: "RpNursingSettingHeader",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否带输入框");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputType",
                table: "RpNursingSettingHeader");

            migrationBuilder.DropColumn(
                name: "IsCarryInputBox",
                table: "RpNursingSettingHeader");
        }
    }
}
