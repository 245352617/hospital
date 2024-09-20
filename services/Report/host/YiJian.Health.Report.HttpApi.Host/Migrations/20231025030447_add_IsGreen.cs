using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Health.Report.Migrations
{
    public partial class add_IsGreen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGreen",
                table: "RpNursingDocument",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "绿色通道");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGreen",
                table: "RpNursingDocument");
        }
    }
}
