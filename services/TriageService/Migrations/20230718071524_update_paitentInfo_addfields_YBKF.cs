using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class update_paitentInfo_addfields_YBKF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrMDTRTId",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "当前就诊标识");

            migrationBuilder.AddColumn<string>(
                name: "InsureType",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "险种类型");

            migrationBuilder.AddColumn<string>(
                name: "OutSetlFlag",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "异地结算标志");

            migrationBuilder.AddColumn<string>(
                name: "PatnId",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "参保人标识");

            migrationBuilder.AddColumn<string>(
                name: "PoolArea",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "统筹区编码");

            migrationBuilder.AlterColumn<int>(
                name: "TriageConfigType",
                table: "Dict_TriageConfig",
                nullable: false,
                comment: "分诊设置类型  1001:绿色通道 1002:群伤事件 1003:费别 1004:来院方式 1005:科室配置 1006:院前分诊去向 1013:院前分诊评分类型 具体以TriageDict数据为准",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "分诊设置类型 1001:绿色通道 1002:群伤事件 1003:院前分诊评分类型 1004:来院方式  1005:科室配置 1006:院前分诊去向");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrMDTRTId",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "InsureType",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "OutSetlFlag",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "PatnId",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "PoolArea",
                table: "Triage_PatientInfo");

            migrationBuilder.AlterColumn<int>(
                name: "TriageConfigType",
                table: "Dict_TriageConfig",
                type: "int",
                nullable: false,
                comment: "分诊设置类型 1001:绿色通道 1002:群伤事件 1003:院前分诊评分类型 1004:来院方式  1005:科室配置 1006:院前分诊去向",
                oldClrType: typeof(int),
                oldComment: "分诊设置类型  1001:绿色通道 1002:群伤事件 1003:费别 1004:来院方式 1005:科室配置 1006:院前分诊去向 1013:院前分诊评分类型 具体以TriageDict数据为准");
        }
    }
}
