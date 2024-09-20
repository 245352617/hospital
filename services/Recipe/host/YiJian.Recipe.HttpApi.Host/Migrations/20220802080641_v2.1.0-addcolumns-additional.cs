using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addcolumnsadditional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Additional",
                table: "RC_Treat",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "加收标志");

            migrationBuilder.AddColumn<bool>(
                name: "Additional",
                table: "RC_Project",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "加收标志");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Additional",
                table: "RC_Treat");

            migrationBuilder.DropColumn(
                name: "Additional",
                table: "RC_Project");
        }
    }
}
