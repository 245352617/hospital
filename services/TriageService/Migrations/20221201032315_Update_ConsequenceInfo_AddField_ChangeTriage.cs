using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Update_ConsequenceInfo_AddField_ChangeTriage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTime",
                table: "Triage_PatientInfo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Triage_PatientInfo",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChangeTriage",
                table: "Triage_ConsequenceInfo",
                maxLength: 256,
                nullable: false,
                defaultValue: false,
                comment: "变更就诊");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginTime",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "ChangeTriage",
                table: "Triage_ConsequenceInfo");
        }
    }
}
