using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class TriageConfig202107261934 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TriageConfig_TriageConfigCode",
                table: "TriageConfig",
                column: "TriageConfigCode",
                unique: true,
                filter: "[TriageConfigCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TriageConfig_TriageConfigCode",
                table: "TriageConfig");
        }
    }
}
