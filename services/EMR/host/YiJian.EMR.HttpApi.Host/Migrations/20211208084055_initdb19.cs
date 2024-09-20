using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmrCatalogue_Lv",
                table: "EmrCatalogue");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EmrDataBindMap",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "EmrDataBindMap",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "EmrDataBindMap",
                newName: "DataSource");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "EmrDataBindMap",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "EmrDataBindMap",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DataSource",
                table: "EmrDataBindMap",
                newName: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_EmrCatalogue_Lv",
                table: "EmrCatalogue",
                column: "Lv");
        }
    }
}
