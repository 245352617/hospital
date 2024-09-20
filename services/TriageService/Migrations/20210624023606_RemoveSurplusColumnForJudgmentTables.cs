using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class RemoveSurplusColumnForJudgmentTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalScore",
                table: "Dict_JudgmentType");

            migrationBuilder.DropColumn(
                name: "EmergencyLevel",
                table: "Dict_JudgmentMaster");

            migrationBuilder.DropColumn(
                name: "Expression",
                table: "Dict_JudgmentItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdditionalScore",
                table: "Dict_JudgmentType",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "额外提升分诊等级");

            migrationBuilder.AddColumn<int>(
                name: "EmergencyLevel",
                table: "Dict_JudgmentMaster",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "级别");

            migrationBuilder.AddColumn<string>(
                name: "Expression",
                table: "Dict_JudgmentItem",
                type: "nvarchar(max)",
                nullable: true,
                comment: "表达式");
        }
    }
}
