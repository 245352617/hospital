using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_NursePatients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandoverDoctorCode",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "HandoverDoctorName",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "SuccessionDoctorCode",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "SuccessionDoctorName",
                table: "Handover_NursePatients");

            migrationBuilder.AddColumn<string>(
                name: "HandoverNurseCode",
                table: "Handover_NursePatients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "交班护士编码");

            migrationBuilder.AddColumn<string>(
                name: "HandoverNurseName",
                table: "Handover_NursePatients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "交班护士名称");

            migrationBuilder.AddColumn<string>(
                name: "SuccessionNurseCode",
                table: "Handover_NursePatients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "接班护士编码");

            migrationBuilder.AddColumn<string>(
                name: "SuccessionNurseName",
                table: "Handover_NursePatients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "接班护士名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandoverNurseCode",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "HandoverNurseName",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "SuccessionNurseCode",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "SuccessionNurseName",
                table: "Handover_NursePatients");

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
        }
    }
}
