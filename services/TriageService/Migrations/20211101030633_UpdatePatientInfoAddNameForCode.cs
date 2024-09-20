using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class UpdatePatientInfoAddNameForCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Triage_VitalSignInfo");

            migrationBuilder.AddColumn<string>(
                name: "RemarkName",
                table: "Triage_VitalSignInfo",
                maxLength: 100,
                nullable: true,
                comment: "生命体征备注名称");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfVisitCode",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "就诊类型Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "就诊类型");

            migrationBuilder.AlterColumn<string>(
                name: "ToHospitalWayCode",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "来院方式Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "来院方式");

            migrationBuilder.AlterColumn<string>(
                name: "TaskInfoNum",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "任务单流水号",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "任务单流水号");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "患者性别Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "患者性别");

            migrationBuilder.AlterColumn<string>(
                name: "Nation",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "民族Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "民族");

            migrationBuilder.AlterColumn<string>(
                name: "Narration",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "主诉Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "主诉");

            migrationBuilder.AlterColumn<string>(
                name: "Identity",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "患者身份Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "患者身份");

            migrationBuilder.AlterColumn<string>(
                name: "GreenRoadCode",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "绿色通道Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "绿色通道");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "国家Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "国家");

            migrationBuilder.AlterColumn<string>(
                name: "Consciousness",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "意识Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "意识");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "费别Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "费别");

            migrationBuilder.AddColumn<string>(
                name: "ChargeTypeName",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "费别名称");

            migrationBuilder.AddColumn<string>(
                name: "ConsciousnessName",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "意识名称");

            migrationBuilder.AddColumn<string>(
                name: "DiseaseName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "重点病种名称");

            migrationBuilder.AddColumn<string>(
                name: "GreenRoadName",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "绿色通道名称");

            migrationBuilder.AddColumn<string>(
                name: "IdentityName",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "患者身份名称");

            migrationBuilder.AddColumn<string>(
                name: "NarrationName",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "主诉名称");

            migrationBuilder.AddColumn<string>(
                name: "NationName",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "民族名称");

            migrationBuilder.AddColumn<string>(
                name: "SexName",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "患者性别名称");

            migrationBuilder.AddColumn<string>(
                name: "ToHospitalWayName",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "来院方式名称");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfVisitName",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "就诊类型名称");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeLevel",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "分诊级别变更Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "分诊级别变更");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeDept",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "科室变更Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "科室变更");

            migrationBuilder.AlterColumn<string>(
                name: "AutoTriageLevel",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "自动分诊级别Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "自动分诊级别");

            migrationBuilder.AlterColumn<string>(
                name: "ActTriageLevel",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "实际分诊级别Code",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "实际分诊级别");

            migrationBuilder.AddColumn<string>(
                name: "ActTriageLevelName",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "实际分诊级别名称");

            migrationBuilder.AddColumn<string>(
                name: "AutoTriageLevelName",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "自动分诊级别名称");

            migrationBuilder.AddColumn<string>(
                name: "ChangeDeptName",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "科室变更名称");

            migrationBuilder.AddColumn<string>(
                name: "ChangeLevelName",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "分诊级别变更名称");

            migrationBuilder.AddColumn<string>(
                name: "TriageDeptName",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "分诊科室名称");

            migrationBuilder.AddColumn<string>(
                name: "TriageTargetName",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "分诊去向名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemarkName",
                table: "Triage_VitalSignInfo");

            migrationBuilder.DropColumn(
                name: "ChargeTypeName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "ConsciousnessName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "DiseaseName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "GreenRoadName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "IdentityName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "NarrationName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "NationName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "SexName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "ToHospitalWayName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "TypeOfVisitName",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "ActTriageLevelName",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "AutoTriageLevelName",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "ChangeDeptName",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "ChangeLevelName",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "TriageDeptName",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.DropColumn(
                name: "TriageTargetName",
                table: "Triage_ConsequenceInfo");

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Triage_VitalSignInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "体重");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfVisitCode",
                table: "Triage_PatientInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "就诊类型",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "就诊类型Code");

            migrationBuilder.AlterColumn<string>(
                name: "ToHospitalWayCode",
                table: "Triage_PatientInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "来院方式",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "来院方式Code");

            migrationBuilder.AlterColumn<string>(
                name: "TaskInfoNum",
                table: "Triage_PatientInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "任务单流水号",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "任务单流水号");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Triage_PatientInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "患者性别",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "患者性别Code");

            migrationBuilder.AlterColumn<string>(
                name: "Nation",
                table: "Triage_PatientInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "民族",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "民族Code");

            migrationBuilder.AlterColumn<string>(
                name: "Narration",
                table: "Triage_PatientInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "主诉",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "主诉Code");

            migrationBuilder.AlterColumn<string>(
                name: "Identity",
                table: "Triage_PatientInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "患者身份",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "患者身份Code");

            migrationBuilder.AlterColumn<string>(
                name: "GreenRoadCode",
                table: "Triage_PatientInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "绿色通道",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "绿色通道Code");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Triage_PatientInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "国家",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "国家Code");

            migrationBuilder.AlterColumn<string>(
                name: "Consciousness",
                table: "Triage_PatientInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "意识",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "意识Code");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                table: "Triage_PatientInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "费别",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "费别Code");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "分诊级别变更",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "分诊级别变更Code");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeDept",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "科室变更",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "科室变更Code");

            migrationBuilder.AlterColumn<string>(
                name: "AutoTriageLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "自动分诊级别",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "自动分诊级别Code");

            migrationBuilder.AlterColumn<string>(
                name: "ActTriageLevel",
                table: "Triage_ConsequenceInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "实际分诊级别",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "实际分诊级别Code");
        }
    }
}
