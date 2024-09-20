using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_NurseHandover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandoverDoctorCode",
                table: "Handover_NurseHandovers");

            migrationBuilder.DropColumn(
                name: "HandoverDoctorName",
                table: "Handover_NurseHandovers");

            migrationBuilder.DropColumn(
                name: "HandoverTime",
                table: "Handover_NurseHandovers");

            migrationBuilder.DropColumn(
                name: "SuccessionDoctorCode",
                table: "Handover_NurseHandovers");

            migrationBuilder.DropColumn(
                name: "SuccessionDoctorName",
                table: "Handover_NurseHandovers");

            migrationBuilder.AlterTable(
                name: "Handover_NursePatients",
                comment: "护士交班患者",
                oldComment: "护士交班id");

            migrationBuilder.AddColumn<string>(
                name: "HandoverDoctorCode",
                table: "Handover_NursePatients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "交班医生编码");

            migrationBuilder.AddColumn<string>(
                name: "HandoverDoctorName",
                table: "Handover_NursePatients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "交班医生名称");

            migrationBuilder.AddColumn<DateTime>(
                name: "HandoverTime",
                table: "Handover_NursePatients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "交班时间");

            migrationBuilder.AddColumn<string>(
                name: "SuccessionDoctorCode",
                table: "Handover_NursePatients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "接班医生编码");

            migrationBuilder.AddColumn<string>(
                name: "SuccessionDoctorName",
                table: "Handover_NursePatients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "接班医生名称");

            migrationBuilder.AddColumn<string>(
                name: "CreationCode",
                table: "Handover_NurseHandovers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreationName",
                table: "Handover_NurseHandovers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandoverDoctorCode",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "HandoverDoctorName",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "HandoverTime",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "SuccessionDoctorCode",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "SuccessionDoctorName",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "CreationCode",
                table: "Handover_NurseHandovers");

            migrationBuilder.DropColumn(
                name: "CreationName",
                table: "Handover_NurseHandovers");

            migrationBuilder.AlterTable(
                name: "Handover_NursePatients",
                comment: "护士交班id",
                oldComment: "护士交班患者");

            migrationBuilder.AddColumn<string>(
                name: "HandoverDoctorCode",
                table: "Handover_NurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "",
                comment: "交班医生编码");

            migrationBuilder.AddColumn<string>(
                name: "HandoverDoctorName",
                table: "Handover_NurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "",
                comment: "交班医生名称");

            migrationBuilder.AddColumn<DateTime>(
                name: "HandoverTime",
                table: "Handover_NurseHandovers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "交班时间");

            migrationBuilder.AddColumn<string>(
                name: "SuccessionDoctorCode",
                table: "Handover_NurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "",
                comment: "接班医生编码");

            migrationBuilder.AddColumn<string>(
                name: "SuccessionDoctorName",
                table: "Handover_NurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "",
                comment: "接班医生名称");
        }
    }
}
