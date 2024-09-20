using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class InsuplcAdmdvCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InsuplcAdmdvCode",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "参保地代码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuplcAdmdvCode",
                table: "Triage_PatientInfo");
        }
    }
}
