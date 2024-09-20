using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddNarrationComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NarrationComments",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "主诉备注");

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperature",
                table: "Triage_Covid19Exam",
                type: "decimal(18, 4)",
                nullable: false,
                comment: "体温",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "体温");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NarrationComments",
                table: "Triage_PatientInfo");

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperature",
                table: "Triage_Covid19Exam",
                type: "decimal(18,2)",
                nullable: false,
                comment: "体温",
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 4)",
                oldComment: "体温");
        }
    }
}
