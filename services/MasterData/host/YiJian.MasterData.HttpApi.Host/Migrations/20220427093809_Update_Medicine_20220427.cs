using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Medicine_20220427 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DosageQty",
                table: "Dict_Medicine",
                type: "float",
                nullable: false,
                comment: "剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "剂量");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabCatalog_CatalogName",
                table: "Dict_LabCatalog",
                column: "CatalogName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dict_LabCatalog_CatalogName",
                table: "Dict_LabCatalog");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "Dict_Medicine",
                type: "decimal(18,2)",
                nullable: false,
                comment: "剂量",
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "剂量");
        }
    }
}
