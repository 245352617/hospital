using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class ScoreInfo20210719 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LevelCode",
                table: "Triage_ScoreInfo",
                type: "nvarchar(60)",
                nullable: true,
                comment: "等级编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelCode",
                table: "Triage_ScoreInfo");
        }
    }
}
