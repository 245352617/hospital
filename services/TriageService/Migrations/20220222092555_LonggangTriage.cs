using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class LonggangTriage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "BloodGlucose",
                table: "Triage_VitalSignInfo",
                nullable: true,
                comment: "血糖（单位 mmol/L）");

            migrationBuilder.AddColumn<string>(
                name: "CrowdCode",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "人群编码");

            migrationBuilder.AddColumn<string>(
                name: "CrowdName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "人群");

            migrationBuilder.AddColumn<int>(
                name: "GestationalWeeks",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: 0,
                comment: "孕周");

            migrationBuilder.AddColumn<string>(
                name: "IdTypeCode",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "证件类型编码");

            migrationBuilder.AddColumn<string>(
                name: "IdTypeName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "证件类型编码");

            migrationBuilder.AddColumn<int>(
                name: "PersistDays",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: 0,
                comment: "持续时间（天）");

            migrationBuilder.AddColumn<int>(
                name: "PersistHours",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: 0,
                comment: "持续时间（时）");

            migrationBuilder.AddColumn<int>(
                name: "PersistMinutes",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: 0,
                comment: "持续时间（分）");

            migrationBuilder.AddColumn<string>(
                name: "VisitReasonCode",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "就诊原因编码");

            migrationBuilder.AddColumn<string>(
                name: "VisitReasonName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "就诊原因");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodGlucose",
                table: "Triage_VitalSignInfo");

            migrationBuilder.DropColumn(
                name: "CrowdCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "CrowdName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "GestationalWeeks",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "IdTypeCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "IdTypeName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "PersistDays",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "PersistHours",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "PersistMinutes",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "VisitReasonCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "VisitReasonName",
                table: "Triage_PatientInfo");
        }
    }
}
