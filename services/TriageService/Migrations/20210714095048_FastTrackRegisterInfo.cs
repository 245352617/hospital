using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class FastTrackRegisterInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TriageUserCode",
                table: "Triage_PatientInfo",
                type: "varchar(30)",
                nullable: true,
                comment: "分诊人code",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true,
                oldComment: "分诊人");

            migrationBuilder.AddColumn<string>(
                name: "TriageUserName",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊人名称");

            migrationBuilder.AddColumn<string>(
                name: "ReceptionNurseName",
                table: "Triage_FastTrackRegisterInfo",
                type: "nvarchar(100)",
                nullable: true,
                comment: "接诊护士名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriageUserName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "ReceptionNurseName",
                table: "Triage_FastTrackRegisterInfo");

            migrationBuilder.AlterColumn<string>(
                name: "TriageUserCode",
                table: "Triage_PatientInfo",
                type: "varchar(30)",
                nullable: true,
                comment: "分诊人",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true,
                oldComment: "分诊人code");
        }
    }
}
