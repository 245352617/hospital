using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Update_ConsequenceInfo_20220513 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChangeLevelName",
                table: "Triage_ConsequenceInfo",
                maxLength: 500,
                nullable: true,
                comment: "分诊级别变更名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "分诊级别变更名称");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeLevel",
                table: "Triage_ConsequenceInfo",
                maxLength: 500,
                nullable: true,
                comment: "分诊级别变更Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "分诊级别变更Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChangeLevelName",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "分诊级别变更名称",
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "分诊级别变更名称");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "分诊级别变更Code",
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "分诊级别变更Code");
        }
    }
}
