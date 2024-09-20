using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class ChangeMedcineColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EmergencySign",
                table: "Dict_Medicine",
                type: "decimal(18,2)",
                nullable: false,
                comment: "（急诊处方标志）1.急诊 0.普通",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "（急诊处方标志）1.急诊 0.普通");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmergencySign",
                table: "Dict_Medicine",
                type: "int",
                nullable: false,
                comment: "（急诊处方标志）1.急诊 0.普通",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "（急诊处方标志）1.急诊 0.普通");
        }
    }
}
