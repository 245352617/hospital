using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class add_column_isfirstaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAid",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: false,
                comment: "是否是院前患者");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstAid",
                table: "Triage_PatientInfo");
        }
    }
}
