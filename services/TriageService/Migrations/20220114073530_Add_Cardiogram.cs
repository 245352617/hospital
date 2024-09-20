using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Add_Cardiogram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardiogramCode",
                table: "Triage_VitalSignInfo",
                maxLength: 100,
                nullable: true,
                comment: "心电图 Code");

            migrationBuilder.AddColumn<string>(
                name: "CardiogramName",
                table: "Triage_VitalSignInfo",
                maxLength: 100,
                nullable: true,
                comment: "心电图 名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardiogramCode",
                table: "Triage_VitalSignInfo");

            migrationBuilder.DropColumn(
                name: "CardiogramName",
                table: "Triage_VitalSignInfo");
        }
    }
}
