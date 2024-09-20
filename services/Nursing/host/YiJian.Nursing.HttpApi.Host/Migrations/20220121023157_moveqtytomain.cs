using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class moveqtytomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "NursingTreat");

            migrationBuilder.DropColumn(
                name: "RecieveQty",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "RecieveUnit",
                table: "NursingPrescribe");

            migrationBuilder.AddColumn<int>(
                name: "RecieveQty",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "数量");

            migrationBuilder.AddColumn<string>(
                name: "RecieveUnit",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "单位");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecieveQty",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "RecieveUnit",
                table: "NursingRecipe");

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "NursingTreat",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "数量");

            migrationBuilder.AddColumn<int>(
                name: "RecieveQty",
                table: "NursingPrescribe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "领量(数量)");

            migrationBuilder.AddColumn<string>(
                name: "RecieveUnit",
                table: "NursingPrescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "领量单位");
        }
    }
}
