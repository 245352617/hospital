using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Add_LastDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstDoctorCode",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "首诊医生工号");

            migrationBuilder.AddColumn<string>(
                name: "FirstDoctorName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "首诊医生名称");

            migrationBuilder.AddColumn<string>(
                name: "LastDirectionCode",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "最终去向代码");

            migrationBuilder.AddColumn<string>(
                name: "LastDirectionName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "最终去向名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstDoctorCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "FirstDoctorName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "LastDirectionCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "LastDirectionName",
                table: "Triage_PatientInfo");
        }
    }
}
