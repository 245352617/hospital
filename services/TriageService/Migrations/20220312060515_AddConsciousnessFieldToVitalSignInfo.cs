using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddConsciousnessFieldToVitalSignInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConsciousnessCode",
                table: "Triage_VitalSignInfo",
                maxLength: 100,
                nullable: true,
                comment: "意识Code");

            migrationBuilder.AddColumn<string>(
                name: "ConsciousnessName",
                table: "Triage_VitalSignInfo",
                maxLength: 100,
                nullable: true,
                comment: "意识名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsciousnessCode",
                table: "Triage_VitalSignInfo");

            migrationBuilder.DropColumn(
                name: "ConsciousnessName",
                table: "Triage_VitalSignInfo");
        }
    }
}
