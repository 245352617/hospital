using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class add_CallNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CallNo",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "叫号号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallNo",
                table: "Triage_PatientInfo");
        }
    }
}
