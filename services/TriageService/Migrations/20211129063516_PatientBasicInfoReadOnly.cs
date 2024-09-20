using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class PatientBasicInfoReadOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBasicInfoReadOnly",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: false,
                comment: "患者基本信息是否不可编译");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBasicInfoReadOnly",
                table: "Triage_PatientInfo");
        }
    }
}
