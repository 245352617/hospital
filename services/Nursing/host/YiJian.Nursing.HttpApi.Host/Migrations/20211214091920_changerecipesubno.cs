using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class changerecipesubno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipeSubNo",
                table: "NursingRecipe",
                newName: "RecipeGroupNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipeGroupNo",
                table: "NursingRecipe",
                newName: "RecipeSubNo");
        }
    }
}
