using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Update_PatientInfo_AddField_DoctorName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "就诊医生名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Triage_PatientInfo");
        }
    }
}
