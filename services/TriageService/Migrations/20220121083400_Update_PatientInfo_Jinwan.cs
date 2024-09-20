using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Update_PatientInfo_Jinwan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeqNumber",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "预约流水号");

            migrationBuilder.AddColumn<string>(
                name: "DoctorCode",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "医生编码");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "医生姓名");

            migrationBuilder.AddColumn<string>(
                name: "WorkType",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "班次");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeqNumber",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "DoctorCode",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "WorkType",
                table: "Triage_ConsequenceInfo");
        }
    }
}
