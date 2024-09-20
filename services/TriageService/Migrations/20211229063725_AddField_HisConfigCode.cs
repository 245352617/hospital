using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddField_HisConfigCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HisConfigCode",
                table: "Dict_TriageConfig",
                maxLength: 50,
                nullable: true,
                comment: "对应 HIS 系统中的代码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HisConfigCode",
                table: "Dict_TriageConfig");
        }
    }
}
