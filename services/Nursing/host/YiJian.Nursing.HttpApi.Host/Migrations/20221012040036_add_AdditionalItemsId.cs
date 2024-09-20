using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class add_AdditionalItemsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdditionalItemsId",
                table: "NursingTreat",
                type: "uniqueidentifier",
                nullable: true,
                comment: "处置关联处方医嘱ID");

            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "护理医嘱状态:0=未知,1=已驳回,2=已确认,3=已确认,4=已提交(医生站)->无执行(护士站),5=已执行,6=已停止(医生站)->停复核(护士站),7=已停嘱(医生站)->停复核(护士站),99=已作废",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站)");

            migrationBuilder.AlterColumn<int>(
                name: "ItemType",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "医嘱各项分类: 0=药品,1=检查,2=检验,3=诊疗",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalItemsId",
                table: "NursingTreat");

            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "护理医嘱状态:0=未知,1=已驳回,2=已确认,3=已确认,4=已提交(医生站)->无执行(护士站),5=已执行,6=已停止(医生站)->停复核(护士站),7=已停嘱(医生站)->停复核(护士站),99=已作废");

            migrationBuilder.AlterColumn<int>(
                name: "ItemType",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱各项分类: 0=药品,1=检查,2=检验,3=诊疗");
        }
    }
}
