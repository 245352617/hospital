using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class CancelPatientInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationTime",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "作废时间");

            migrationBuilder.AddColumn<string>(
                name: "CancellationUser",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "作废人");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: false,
                comment: "是否作废");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationTime",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "CancellationUser",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Triage_PatientInfo");
        }
    }
}
