using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210remvoeretrycolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Retry",
                table: "RC_Prescription");

            migrationBuilder.DropColumn(
                name: "RetryTimes",
                table: "RC_Prescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Retry",
                table: "RC_Prescription",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RetryTimes",
                table: "RC_Prescription",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
