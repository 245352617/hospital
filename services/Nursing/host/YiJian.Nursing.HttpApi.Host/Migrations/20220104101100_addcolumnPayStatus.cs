using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class addcolumnPayStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "收费单价",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "单价");

            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站),18=已缴费,19=已退费");

            migrationBuilder.AddColumn<int>(
                name: "PayStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "支付状态 , 0=未支付,1=已支付,2=部分支付,3=已退费");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "收费单位");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayStatus",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "NursingRecipe");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "单价",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "收费单价");

            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站),18=已缴费,19=已退费",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站)");
        }
    }
}
