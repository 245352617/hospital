using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddTriageTimeForPatientInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "患者性别",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "患者性别");

            migrationBuilder.AddColumn<DateTime>(
                name: "TriageTime",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "分诊时间");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReportSettingId",
                table: "Dict_ReportSettingQueryOption",
                nullable: false,
                comment: "报表设置表主键Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriageTime",
                table: "Triage_PatientInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Triage_PatientInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "患者性别",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "患者性别");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReportSettingId",
                table: "Dict_ReportSettingQueryOption",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldComment: "报表设置表主键Id");
        }
    }
}
