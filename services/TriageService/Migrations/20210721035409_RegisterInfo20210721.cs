using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class RegisterInfo20210721 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitNo",
                table: "Triage_PatientInfo");

            migrationBuilder.AddColumn<string>(
                name: "VisitNo",
                table: "Triage_RegisterInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "就诊次数");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitNo",
                table: "Triage_RegisterInfo");

            migrationBuilder.AddColumn<string>(
                name: "VisitNo",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "就诊次数");
        }
    }
}
