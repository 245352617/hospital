using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addcolumnischildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChildren",
                table: "RC_Treat",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "当前是否是儿童");

            migrationBuilder.AlterColumn<int>(
                name: "BillState",
                table: "RC_Prescription",
                type: "int",
                nullable: false,
                comment: "订单状态, 0.未缴费 1.已缴费 -1.已作废 2.已执行,3.已退费",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "订单状态, 0.未缴费 1.已缴费 -1.已作废 2.已执行");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChildren",
                table: "RC_Treat");

            migrationBuilder.AlterColumn<int>(
                name: "BillState",
                table: "RC_Prescription",
                type: "int",
                nullable: false,
                comment: "订单状态, 0.未缴费 1.已缴费 -1.已作废 2.已执行",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "订单状态, 0.未缴费 1.已缴费 -1.已作废 2.已执行,3.已退费");
        }
    }
}
