using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lv",
                table: "EmrTemplateCatalogue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "目录结构层级Level");

            migrationBuilder.AddColumn<int>(
                name: "Lv",
                table: "EmrCatalogue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "目录结构层级Level");

            migrationBuilder.CreateIndex(
                name: "IX_EmrTemplateCatalogue_Lv",
                table: "EmrTemplateCatalogue",
                column: "Lv");

            migrationBuilder.CreateIndex(
                name: "IX_EmrCatalogue_Lv",
                table: "EmrCatalogue",
                column: "Lv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmrTemplateCatalogue_Lv",
                table: "EmrTemplateCatalogue");

            migrationBuilder.DropIndex(
                name: "IX_EmrCatalogue_Lv",
                table: "EmrCatalogue");

            migrationBuilder.DropColumn(
                name: "Lv",
                table: "EmrTemplateCatalogue");

            migrationBuilder.DropColumn(
                name: "Lv",
                table: "EmrCatalogue");
        }
    }
}
