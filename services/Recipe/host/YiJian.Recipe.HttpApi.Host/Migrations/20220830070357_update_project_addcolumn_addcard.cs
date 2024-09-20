using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class update_project_addcolumn_addcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddCard",
                table: "RC_ProjectLabProp",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "加卡片类型 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)14.新型冠状病毒RNA检测申请单13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单");

            migrationBuilder.AddColumn<string>(
                name: "AddCard",
                table: "RC_ProjectExamProp",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "附加卡片类型 12.TCT细胞学检查申请单 11.病理检验申请单 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddCard",
                table: "RC_ProjectLabProp");

            migrationBuilder.DropColumn(
                name: "AddCard",
                table: "RC_ProjectExamProp");
        }
    }
}
