using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddPyCodeForJudgment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Py",
                table: "Dict_JudgmentMaster",
                nullable: true,
                comment: "拼音码");

            migrationBuilder.AddColumn<string>(
                name: "Py",
                table: "Dict_JudgmentItem",
                nullable: true,
                comment: "拼音码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Py",
                table: "Dict_JudgmentMaster");

            migrationBuilder.DropColumn(
                name: "Py",
                table: "Dict_JudgmentItem");
        }
    }
}
