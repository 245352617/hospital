using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class GroupInjuryInfo20211431 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NdLevelCount",
                table: "Triage_GroupInjuryInfo");

            migrationBuilder.DropColumn(
                name: "RdLevelCount",
                table: "Triage_GroupInjuryInfo");

            migrationBuilder.DropColumn(
                name: "StLevelCount",
                table: "Triage_GroupInjuryInfo");

            migrationBuilder.DropColumn(
                name: "ThaLevelCount",
                table: "Triage_GroupInjuryInfo");

            migrationBuilder.DropColumn(
                name: "ThbLevelCount",
                table: "Triage_GroupInjuryInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NdLevelCount",
                table: "Triage_GroupInjuryInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Ⅱ级患者人数");

            migrationBuilder.AddColumn<int>(
                name: "RdLevelCount",
                table: "Triage_GroupInjuryInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Ⅲ级患者人数");

            migrationBuilder.AddColumn<int>(
                name: "StLevelCount",
                table: "Triage_GroupInjuryInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Ⅰ级患者人数");

            migrationBuilder.AddColumn<int>(
                name: "ThaLevelCount",
                table: "Triage_GroupInjuryInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Ⅳa级患者人数");

            migrationBuilder.AddColumn<int>(
                name: "ThbLevelCount",
                table: "Triage_GroupInjuryInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Ⅳb级患者人数");
        }
    }
}
