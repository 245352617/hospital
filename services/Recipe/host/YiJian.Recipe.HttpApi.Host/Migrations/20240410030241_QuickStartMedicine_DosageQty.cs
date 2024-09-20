using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class QuickStartMedicine_DosageQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "RC_QuickStartMedicine",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                comment: "剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "剂量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "RC_QuickStartMedicine",
                type: "decimal(18,2)",
                nullable: false,
                comment: "剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3,
                oldComment: "剂量");
        }
    }
}
