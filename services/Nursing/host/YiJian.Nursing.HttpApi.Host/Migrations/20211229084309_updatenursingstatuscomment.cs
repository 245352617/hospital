using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class updatenursingstatuscomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站),18=已缴费,19=已退费",
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站),18=已缴费,19=已退费");
        }
    }
}
