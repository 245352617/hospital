using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class GuardianIdCardNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuardianIdCardNo",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "监护人身份证号码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuardianIdCardNo",
                table: "Triage_PatientInfo");
        }
    }
}
