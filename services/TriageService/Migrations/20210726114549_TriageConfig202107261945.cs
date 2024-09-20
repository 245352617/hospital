using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class TriageConfig202107261945 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TriageConfig_TriageConfigCode",
                table: "TriageConfig");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TriageConfig_TriageConfigCode",
                table: "TriageConfig",
                column: "TriageConfigCode",
                unique: true,
                filter: "[TriageConfigCode] IS NOT NULL");
        }
    }
}
