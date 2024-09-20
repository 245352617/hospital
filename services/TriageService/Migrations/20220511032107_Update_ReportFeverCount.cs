using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Update_ReportFeverCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeathCount",
                table: "ReportFeverCount");

            migrationBuilder.DropColumn(
                name: "DeathCountChanged",
                table: "ReportFeverCount");

            migrationBuilder.AddColumn<int>(
                name: "FeverCount",
                table: "ReportFeverCount",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FeverCountChanged",
                table: "ReportFeverCount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeverCount",
                table: "ReportFeverCount");

            migrationBuilder.DropColumn(
                name: "FeverCountChanged",
                table: "ReportFeverCount");

            migrationBuilder.AddColumn<int>(
                name: "DeathCount",
                table: "ReportFeverCount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeathCountChanged",
                table: "ReportFeverCount",
                type: "int",
                nullable: true);
        }
    }
}
