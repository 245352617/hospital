using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddSixCenterValueToTriageConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SixCenterValue",
                table: "Dict_TriageConfig",
                nullable: true,
                comment: "六大中心值");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SixCenterValue",
                table: "Dict_TriageConfig");
        }
    }
}
