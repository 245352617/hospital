using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class PatientInfo2021071915 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TriageUserCode",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊人code",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true,
                oldComment: "分诊人code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TriageUserCode",
                table: "Triage_PatientInfo",
                type: "varchar(30)",
                nullable: true,
                comment: "分诊人code",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊人code");
        }
    }
}
