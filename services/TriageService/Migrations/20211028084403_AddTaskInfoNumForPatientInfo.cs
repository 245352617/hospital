using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddTaskInfoNumForPatientInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskInfoNum",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "任务单流水号");

            migrationBuilder.AddColumn<string>(
                name: "GroupInjuryName",
                table: "Triage_GroupInjuryInfo",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskInfoNum",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "GroupInjuryName",
                table: "Triage_GroupInjuryInfo");
        }
    }
}
