using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2277_update_intake_Source : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "RpWardRound",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "级别",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "护理单Id(外键)");

            migrationBuilder.AddColumn<int>(
                name: "Source",
                table: "RpIntake",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "来源");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "RpIntake");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "RpWardRound",
                type: "nvarchar(max)",
                nullable: true,
                comment: "护理单Id(外键)",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "级别");
        }
    }
}
