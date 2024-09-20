using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Medicine_20220428 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "Dict_Medicine",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                comment: "剂量",
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "剂量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DosageQty",
                table: "Dict_Medicine",
                type: "float",
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
