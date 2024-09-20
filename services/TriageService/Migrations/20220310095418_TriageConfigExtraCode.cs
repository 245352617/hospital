using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class TriageConfigExtraCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraCode",
                table: "Dict_TriageConfig",
                maxLength: 50,
                nullable: true,
                comment: "额外代码（费别对接省医保缴费档次代码）");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraCode",
                table: "Dict_TriageConfig");
        }
    }
}
