using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Longgang3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuardianIdTypeCode",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuardianIdTypeName",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuardianIdTypeCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "GuardianIdTypeName",
                table: "Triage_PatientInfo");
        }
    }
}
