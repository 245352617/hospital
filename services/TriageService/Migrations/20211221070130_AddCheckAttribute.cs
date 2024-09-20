using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddCheckAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: false,
                comment: "患者唯一标识(HIS)",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "患者唯一标识(HIS)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Triage_PatientInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "患者唯一标识(HIS)",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldComment: "患者唯一标识(HIS)");
        }
    }
}
