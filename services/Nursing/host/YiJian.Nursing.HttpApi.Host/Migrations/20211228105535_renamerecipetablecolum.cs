using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class renamerecipetablecolum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipeCategoryName",
                table: "NursingRecipe",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "RecipeCategoryCode",
                table: "NursingRecipe",
                newName: "CategoryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "NursingRecipe",
                newName: "RecipeCategoryName");

            migrationBuilder.RenameColumn(
                name: "CategoryCode",
                table: "NursingRecipe",
                newName: "RecipeCategoryCode");
        }
    }
}
