using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Modify_TriageDeptCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TriageDept",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "患者分诊科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldComment: "患者分诊科室编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TriageDept",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                comment: "患者分诊科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "患者分诊科室编码");
        }
    }
}
