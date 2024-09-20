using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class add_SkinTestSignChoseResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "护理医嘱状态:0=未知,1=已驳回,2=已确认,3=已确认,4=已提交(医生站)->无执行(护士站),5=已执行,6=已停止(医生站)->停复核(护士站),7=已停嘱(医生站)->停复核(护士站),99=已作废");

            migrationBuilder.AddColumn<int>(
                name: "SkinTestSignChoseResult",
                table: "NursingPrescribe",
                type: "int",
                nullable: true,
                comment: "皮试选择结果,默认空什么都没选择，0=否，1=是，2=续用");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkinTestSignChoseResult",
                table: "NursingPrescribe");

            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "护理医嘱状态:0=未知,1=已驳回,2=已确认,3=已确认,4=已提交(医生站)->无执行(护士站),5=已执行,6=已停止(医生站)->停复核(护士站),7=已停嘱(医生站)->停复核(护士站),99=已作废",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站)");
        }
    }
}
