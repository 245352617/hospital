using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class triage_patientInfo_add_electroncertno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ElectronCertNo",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "医保电子凭证");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectronCertNo",
                table: "Triage_PatientInfo");
        }
    }
}
