using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class recipeaddDiagnosis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "NursingRecipe",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "临床诊断");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "NursingRecipe");
        }
    }
}
