using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class LonggangTriage1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuardianPhone",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AllergyHistory",
                table: "Triage_AdmissionInfo",
                maxLength: 500,
                nullable: true,
                comment: "过敏史");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuardianPhone",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "AllergyHistory",
                table: "Triage_AdmissionInfo");
        }
    }
}
