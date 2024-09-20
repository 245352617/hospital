using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class ResetSomeFiledsLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ScoreType",
                table: "Triage_ScoreInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "评分类型",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "评分类型");

            migrationBuilder.AlterColumn<string>(
                name: "ScoreDescription",
                table: "Triage_ScoreInfo",
                type: "nvarchar(60)",
                nullable: true,
                comment: "评分等级",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "评分等级");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfVisitCode",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "就诊类型",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "就诊类型");

            migrationBuilder.AlterColumn<string>(
                name: "TriageUserCode",
                table: "Triage_PatientInfo",
                type: "varchar(30)",
                nullable: true,
                comment: "分诊人",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "分诊人");

            migrationBuilder.AlterColumn<string>(
                name: "TriageSource",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它");

            migrationBuilder.AlterColumn<string>(
                name: "ToHospitalWayCode",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "来院方式",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "来院方式");

            migrationBuilder.AlterColumn<string>(
                name: "Nation",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "民族",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "民族");

            migrationBuilder.AlterColumn<string>(
                name: "Narration",
                table: "Triage_PatientInfo",
                type: "varchar(max)",
                nullable: true,
                comment: "主诉",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "主诉");

            migrationBuilder.AlterColumn<string>(
                name: "Identity",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "患者身份",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "患者身份");

            migrationBuilder.AlterColumn<string>(
                name: "GreenRoadCode",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "绿色通道",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "绿色通道");

            migrationBuilder.AlterColumn<string>(
                name: "DiseaseCode",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "重点病种Code",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "重点病种Code");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "国家",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "国家");

            migrationBuilder.AlterColumn<string>(
                name: "Consciousness",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "意识",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "意识");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "费别",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "费别");

            migrationBuilder.AlterColumn<string>(
                name: "TriageTargetCode",
                table: "Triage_ConsequenceInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "分诊去向编码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊去向编码");

            migrationBuilder.AlterColumn<string>(
                name: "TriageDeptCode",
                table: "Triage_ConsequenceInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "分诊科室编码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(60)",
                nullable: true,
                comment: "分诊级别变更",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "分诊级别变更");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeDept",
                table: "Triage_ConsequenceInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "科室变更",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "科室变更");

            migrationBuilder.AlterColumn<string>(
                name: "AutoTriageLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(60)",
                nullable: true,
                comment: "自动分诊级别",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "自动分诊级别");

            migrationBuilder.AlterColumn<string>(
                name: "ActTriageLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(60)",
                nullable: true,
                comment: "实际分诊级别",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "实际分诊级别");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ScoreType",
                table: "Triage_ScoreInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "评分类型",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "评分类型");

            migrationBuilder.AlterColumn<string>(
                name: "ScoreDescription",
                table: "Triage_ScoreInfo",
                type: "nvarchar(50)",
                nullable: true,
                comment: "评分等级",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldNullable: true,
                oldComment: "评分等级");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfVisitCode",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "就诊类型",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "就诊类型");

            migrationBuilder.AlterColumn<string>(
                name: "TriageUserCode",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "分诊人",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true,
                oldComment: "分诊人");

            migrationBuilder.AlterColumn<string>(
                name: "TriageSource",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它");

            migrationBuilder.AlterColumn<string>(
                name: "ToHospitalWayCode",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "来院方式",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "来院方式");

            migrationBuilder.AlterColumn<string>(
                name: "Nation",
                table: "Triage_PatientInfo",
                type: "nvarchar(50)",
                nullable: true,
                comment: "民族",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "民族");

            migrationBuilder.AlterColumn<string>(
                name: "Narration",
                table: "Triage_PatientInfo",
                type: "varchar(200)",
                nullable: true,
                comment: "主诉",
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true,
                oldComment: "主诉");

            migrationBuilder.AlterColumn<string>(
                name: "Identity",
                table: "Triage_PatientInfo",
                type: "nvarchar(50)",
                nullable: true,
                comment: "患者身份",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "患者身份");

            migrationBuilder.AlterColumn<string>(
                name: "GreenRoadCode",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "绿色通道",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "绿色通道");

            migrationBuilder.AlterColumn<string>(
                name: "DiseaseCode",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "重点病种Code",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "重点病种Code");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Triage_PatientInfo",
                type: "nvarchar(50)",
                nullable: true,
                comment: "国家",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "国家");

            migrationBuilder.AlterColumn<string>(
                name: "Consciousness",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "意识",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "意识");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                table: "Triage_PatientInfo",
                type: "nvarchar(50)",
                nullable: true,
                comment: "费别",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "费别");

            migrationBuilder.AlterColumn<string>(
                name: "TriageTargetCode",
                table: "Triage_ConsequenceInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊去向编码",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "分诊去向编码");

            migrationBuilder.AlterColumn<string>(
                name: "TriageDeptCode",
                table: "Triage_ConsequenceInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊科室编码",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "分诊科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(50)",
                nullable: true,
                comment: "分诊级别变更",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldNullable: true,
                oldComment: "分诊级别变更");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeDept",
                table: "Triage_ConsequenceInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "科室变更",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "科室变更");

            migrationBuilder.AlterColumn<string>(
                name: "AutoTriageLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(50)",
                nullable: true,
                comment: "自动分诊级别",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldNullable: true,
                oldComment: "自动分诊级别");

            migrationBuilder.AlterColumn<string>(
                name: "ActTriageLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(50)",
                nullable: true,
                comment: "实际分诊级别",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldNullable: true,
                oldComment: "实际分诊级别");
        }
    }
}
