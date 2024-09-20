using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class UpdatePersist : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
