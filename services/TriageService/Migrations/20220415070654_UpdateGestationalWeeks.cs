using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class UpdateGestationalWeeks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GestationalWeeks",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "孕周",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "孕周");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GestationalWeeks",
                table: "Triage_PatientInfo",
                type: "int",
                nullable: false,
                comment: "孕周",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "孕周");
        }
    }
}
