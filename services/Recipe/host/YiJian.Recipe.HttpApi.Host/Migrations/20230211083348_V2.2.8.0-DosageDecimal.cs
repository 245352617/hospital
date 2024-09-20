using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class V2280DosageDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "RC_Prescribe",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "每次剂量");

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultDosageQty",
                table: "RC_Prescribe",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "默认规格剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "默认规格剂量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "RC_Prescribe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "每次剂量");

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultDosageQty",
                table: "RC_Prescribe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "默认规格剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "默认规格剂量");
        }
    }
}
