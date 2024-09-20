using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddDefaultValueToScoreDict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersistMinutes",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "持续时间（分）",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "持续时间（分）");

            migrationBuilder.AlterColumn<int>(
                name: "PersistHours",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "持续时间（时）",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "持续时间（时）");

            migrationBuilder.AlterColumn<int>(
                name: "PersistDays",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "持续时间（天）",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "持续时间（天）");

            migrationBuilder.AlterColumn<int>(
                name: "GestationalWeeks",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "孕周",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "孕周");

            migrationBuilder.AlterColumn<int>(
                name: "RemarkStyle",
                table: "Dict_ScoreDict",
                nullable: false,
                comment: "备注文本样式，-1：无，1：纯文本，2：Html",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "DefaultValue",
                table: "Dict_ScoreDict",
                nullable: true,
                comment: "默认值选项");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultValue",
                table: "Dict_ScoreDict");

            migrationBuilder.AlterColumn<int>(
                name: "PersistMinutes",
                table: "Triage_PatientInfo",
                type: "int",
                nullable: false,
                comment: "持续时间（分）",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "持续时间（分）");

            migrationBuilder.AlterColumn<int>(
                name: "PersistHours",
                table: "Triage_PatientInfo",
                type: "int",
                nullable: false,
                comment: "持续时间（时）",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "持续时间（时）");

            migrationBuilder.AlterColumn<int>(
                name: "PersistDays",
                table: "Triage_PatientInfo",
                type: "int",
                nullable: false,
                comment: "持续时间（天）",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "持续时间（天）");

            migrationBuilder.AlterColumn<int>(
                name: "GestationalWeeks",
                table: "Triage_PatientInfo",
                type: "int",
                nullable: false,
                comment: "孕周",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "孕周");

            migrationBuilder.AlterColumn<int>(
                name: "RemarkStyle",
                table: "Dict_ScoreDict",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "备注文本样式，-1：无，1：纯文本，2：Html");
        }
    }
}
