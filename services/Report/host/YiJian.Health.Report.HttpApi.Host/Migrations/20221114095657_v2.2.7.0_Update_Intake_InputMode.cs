using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2270_Update_Intake_InputMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InputMode",
                table: "RpIntake",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "方式");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputMode",
                table: "RpIntake");
        }
    }
}
