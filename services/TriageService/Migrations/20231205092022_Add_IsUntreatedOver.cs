using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Add_IsUntreatedOver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUntreatedOver",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: false,
                comment: "是否过号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUntreatedOver",
                table: "Triage_PatientInfo");
        }
    }
}
