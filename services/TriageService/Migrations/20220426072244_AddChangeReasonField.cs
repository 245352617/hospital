using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddChangeReasonField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "国籍代码");

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "国籍名称");

            migrationBuilder.AddColumn<string>(
                name: "ChangeTriageReasonCode",
                table: "Triage_ConsequenceInfo",
                maxLength: 256,
                nullable: true,
                comment: "变更就诊原因代码");

            migrationBuilder.AddColumn<string>(
                name: "ChangeTriageReasonName",
                table: "Triage_ConsequenceInfo",
                maxLength: 256,
                nullable: true,
                comment: "变更就诊原因");

            migrationBuilder.AddColumn<string>(
                name: "TriageAreaCode",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "分诊分区代码");

            migrationBuilder.AddColumn<string>(
                name: "TriageAreaName",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "分诊分区");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "ChangeTriageReasonCode",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "ChangeTriageReasonName",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "TriageAreaCode",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "TriageAreaName",
                table: "Triage_ConsequenceInfo");
        }
    }
}
