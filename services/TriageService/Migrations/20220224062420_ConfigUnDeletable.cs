using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class ConfigUnDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UnDeletable",
                table: "Dict_TriageConfig",
                nullable: false,
                defaultValue: false,
                comment: "是否不允许删除");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnDeletable",
                table: "Dict_TriageConfig");
        }
    }
}
