using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Qty_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Qty",
                table: "Dict_LabTarget",
                type: "decimal(18,2)",
                nullable: false,
                comment: "数量",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "数量");

            migrationBuilder.AlterColumn<decimal>(
                name: "Qty",
                table: "Dict_ExamTarget",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Qty",
                table: "Dict_LabTarget",
                type: "int",
                nullable: false,
                comment: "数量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "数量");

            migrationBuilder.AlterColumn<int>(
                name: "Qty",
                table: "Dict_ExamTarget",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
