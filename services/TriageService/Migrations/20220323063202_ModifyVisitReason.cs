using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class ModifyVisitReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VisitReasonName",
                table: "Triage_PatientInfo",
                maxLength: 4000,
                nullable: true,
                comment: "就诊原因",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "就诊原因");

            migrationBuilder.AlterColumn<string>(
                name: "VisitReasonCode",
                table: "Triage_PatientInfo",
                maxLength: 4000,
                nullable: true,
                comment: "就诊原因编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "就诊原因编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VisitReasonName",
                table: "Triage_PatientInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "就诊原因",
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true,
                oldComment: "就诊原因");

            migrationBuilder.AlterColumn<string>(
                name: "VisitReasonCode",
                table: "Triage_PatientInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "就诊原因编码",
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true,
                oldComment: "就诊原因编码");
        }
    }
}
