using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TraitsCode",
                table: "RpIntake",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "性状");

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "RpIntake",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "单位编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TraitsCode",
                table: "RpIntake");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "RpIntake");
        }
    }
}
