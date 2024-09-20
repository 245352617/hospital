using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddTriageLevelCodeAndNameForJudgmentItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TriageLevelCode",
                table: "Dict_JudgmentItem",
                maxLength: 50,
                nullable: true,
                comment: "分诊级别Code");

            migrationBuilder.AddColumn<string>(
                name: "TriageLevelName",
                table: "Dict_JudgmentItem",
                maxLength: 50,
                nullable: true,
                comment: "分诊级别名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriageLevelCode",
                table: "Dict_JudgmentItem");

            migrationBuilder.DropColumn(
                name: "TriageLevelName",
                table: "Dict_JudgmentItem");
        }
    }
}
