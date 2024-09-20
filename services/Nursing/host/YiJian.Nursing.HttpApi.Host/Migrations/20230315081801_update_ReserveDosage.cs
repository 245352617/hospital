using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class update_ReserveDosage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ReserveDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "备用量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "备用量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ReserveDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                comment: "备用量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "备用量");
        }
    }
}
