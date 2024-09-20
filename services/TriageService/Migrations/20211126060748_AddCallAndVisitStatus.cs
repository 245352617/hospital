using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddCallAndVisitStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CallingSn",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "叫号排队号");

            migrationBuilder.AddColumn<DateTime>(
                name: "LogTime",
                table: "Triage_PatientInfo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitStatus",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: 0,
                comment: "就诊状态");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallingSn",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "LogTime",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "VisitStatus",
                table: "Triage_PatientInfo");
        }
    }
}
