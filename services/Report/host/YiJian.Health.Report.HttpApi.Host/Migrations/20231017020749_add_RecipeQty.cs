using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Health.Report.Migrations
{
    public partial class add_RecipeQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipeQty",
                table: "RpIntake",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医嘱药品剂量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeQty",
                table: "RpIntake");
        }
    }
}
