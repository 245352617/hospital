using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class UpdateSomeTableColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpPermissionGrants");

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigTypeCode",
                table: "TriageConfigTypeDescription",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigCode",
                table: "TriageConfig",
                maxLength: 50,
                nullable: true,
                comment: "分诊设置代码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊设置代码");

            migrationBuilder.AlterColumn<string>(
                name: "Py",
                table: "TriageConfig",
                maxLength: 50,
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "Triage_VitalSignInfo",
                maxLength: 20,
                nullable: true,
                comment: "体重",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "体重");

            migrationBuilder.AlterColumn<string>(
                name: "Temp",
                table: "Triage_VitalSignInfo",
                maxLength: 20,
                nullable: true,
                comment: "体温",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "体温");

            migrationBuilder.AlterColumn<string>(
                name: "SpO2",
                table: "Triage_VitalSignInfo",
                maxLength: 20,
                nullable: true,
                comment: "血氧饱和度",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "血氧饱和度");

            migrationBuilder.AlterColumn<string>(
                name: "Sdp",
                table: "Triage_VitalSignInfo",
                maxLength: 20,
                nullable: true,
                comment: "舒张压",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "舒张压");

            migrationBuilder.AlterColumn<string>(
                name: "Sbp",
                table: "Triage_VitalSignInfo",
                maxLength: 20,
                nullable: true,
                comment: "收缩压",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "收缩压");

            migrationBuilder.AlterColumn<string>(
                name: "HeartRate",
                table: "Triage_VitalSignInfo",
                maxLength: 20,
                nullable: true,
                comment: "心率",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "心率");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceCode",
                table: "Triage_VitalSignInfo",
                maxLength: 100,
                nullable: true,
                comment: "设备编码",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true,
                oldComment: "设备编码");

            migrationBuilder.AlterColumn<string>(
                name: "BreathRate",
                table: "Triage_VitalSignInfo",
                maxLength: 20,
                nullable: true,
                comment: "呼吸",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "呼吸");

            migrationBuilder.AlterColumn<string>(
                name: "ScoreType",
                table: "Triage_ScoreInfo",
                maxLength: 60,
                nullable: true,
                comment: "评分类型",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "评分类型");

            migrationBuilder.AlterColumn<string>(
                name: "VisitNo",
                table: "Triage_RegisterInfo",
                maxLength: 20,
                nullable: true,
                comment: "就诊次数",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "就诊次数");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterNo",
                table: "Triage_RegisterInfo",
                maxLength: 50,
                nullable: true,
                comment: "挂号流水号",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "挂号流水号");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterDoctorCode",
                table: "Triage_RegisterInfo",
                maxLength: 50,
                nullable: true,
                comment: "挂号医生编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "挂号医生编码");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterDeptCode",
                table: "Triage_RegisterInfo",
                maxLength: 50,
                nullable: true,
                comment: "挂号科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "挂号科室编码");

            migrationBuilder.AddColumn<string>(
                name: "RegisterDoctorName",
                table: "Triage_RegisterInfo",
                maxLength: 50,
                nullable: true,
                comment: "挂号医生姓名");

            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "体重",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "体重");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfVisitCode",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "就诊类型",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "就诊类型");

            migrationBuilder.AlterColumn<string>(
                name: "TriageUserName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "分诊人名称",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊人名称");

            migrationBuilder.AlterColumn<string>(
                name: "TriageUserCode",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "分诊人code",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊人code");

            migrationBuilder.AlterColumn<string>(
                name: "TriageSource",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它");

            migrationBuilder.AlterColumn<string>(
                name: "ToHospitalWayCode",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "来院方式",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "来院方式");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "患者性别",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "患者性别");

            migrationBuilder.AlterColumn<string>(
                name: "RFID",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "RFID",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "RFID");

            migrationBuilder.AlterColumn<string>(
                name: "Py",
                table: "Triage_PatientInfo",
                maxLength: 100,
                nullable: true,
                comment: "患者姓名拼音首字母",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true,
                oldComment: "患者姓名拼音首字母");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "患者唯一标识(HIS)",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "患者唯一标识(HIS)");

            migrationBuilder.AlterColumn<string>(
                name: "Nation",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "民族",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "民族");

            migrationBuilder.AlterColumn<string>(
                name: "Narration",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "主诉",
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true,
                oldComment: "主诉");

            migrationBuilder.AlterColumn<string>(
                name: "MedicalNo",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "医保卡号",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "医保卡号");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNo",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "身份证号",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "身份证号");

            migrationBuilder.AlterColumn<string>(
                name: "Identity",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "患者身份",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "患者身份");

            migrationBuilder.AlterColumn<string>(
                name: "GreenRoadCode",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "绿色通道",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "绿色通道");

            migrationBuilder.AlterColumn<string>(
                name: "DiseaseCode",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "重点病种Code",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "重点病种Code");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "国家",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "国家");

            migrationBuilder.AlterColumn<string>(
                name: "ContactsPhone",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "联系电话",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "联系电话");

            migrationBuilder.AlterColumn<string>(
                name: "Consciousness",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "意识",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "意识");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                table: "Triage_PatientInfo",
                maxLength: 60,
                nullable: true,
                comment: "费别",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "费别");

            migrationBuilder.AlterColumn<string>(
                name: "CardNo",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "诊疗卡号",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "诊疗卡号");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Triage_PatientInfo",
                maxLength: 200,
                nullable: true,
                comment: "患者住址",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true,
                oldComment: "患者住址");

            migrationBuilder.AlterColumn<string>(
                name: "GroupInjuryCode",
                table: "Triage_GroupInjuryInfo",
                maxLength: 500,
                nullable: true,
                comment: "群伤事件类型",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "群伤事件类型");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Triage_FastTrackRegisterInfo",
                maxLength: 20,
                nullable: true,
                comment: "性别",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "性别");

            migrationBuilder.AlterColumn<string>(
                name: "ReceptionNurseName",
                table: "Triage_FastTrackRegisterInfo",
                maxLength: 20,
                nullable: true,
                comment: "接诊护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true,
                oldComment: "接诊护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "PoliceStationPhone",
                table: "Triage_FastTrackRegisterInfo",
                maxLength: 20,
                nullable: true,
                comment: "所属派出所电话号码",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "所属派出所电话号码");

            migrationBuilder.AlterColumn<string>(
                name: "PoliceStationName",
                table: "Triage_FastTrackRegisterInfo",
                maxLength: 50,
                nullable: true,
                comment: "所处派出所名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "所处派出所名称");

            migrationBuilder.AlterColumn<string>(
                name: "PoliceName",
                table: "Triage_FastTrackRegisterInfo",
                maxLength: 20,
                nullable: true,
                comment: "警务人员姓名",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "警务人员姓名");

            migrationBuilder.AlterColumn<string>(
                name: "PoliceCode",
                table: "Triage_FastTrackRegisterInfo",
                maxLength: 20,
                nullable: true,
                comment: "警务人员警号",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "警务人员警号");

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "Triage_FastTrackRegisterInfo",
                maxLength: 50,
                nullable: true,
                comment: "患者姓名",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "患者姓名");

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "Triage_FastTrackRegisterInfo",
                maxLength: 20,
                nullable: true,
                comment: "患者年龄",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "患者年龄");

            migrationBuilder.AlterColumn<string>(
                name: "TriageTargetCode",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "分诊去向编码",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "分诊去向编码");

            migrationBuilder.AlterColumn<string>(
                name: "TriageDeptCode",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "分诊科室编码",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "分诊科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeDept",
                table: "Triage_ConsequenceInfo",
                maxLength: 60,
                nullable: true,
                comment: "科室变更",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true,
                oldComment: "科室变更");

            migrationBuilder.AlterColumn<string>(
                name: "PastMedicalHistory",
                table: "Triage_AdmissionInfo",
                maxLength: 500,
                nullable: true,
                comment: "既往史",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true,
                oldComment: "既往史");

            migrationBuilder.AlterColumn<string>(
                name: "MedicalHistory",
                table: "Triage_AdmissionInfo",
                maxLength: 500,
                nullable: true,
                comment: "现病史",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true,
                oldComment: "现病史");

            migrationBuilder.AlterColumn<string>(
                name: "IsSoreThroatAndCough",
                table: "Triage_AdmissionInfo",
                maxLength: 10,
                nullable: true,
                comment: "是否咽痛咳嗽",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "是否咽痛咳嗽");

            migrationBuilder.AlterColumn<string>(
                name: "IsMediumAndHighRisk",
                table: "Triage_AdmissionInfo",
                maxLength: 10,
                nullable: true,
                comment: "是否去过中高风险区",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "是否去过中高风险区");

            migrationBuilder.AlterColumn<string>(
                name: "IsHot",
                table: "Triage_AdmissionInfo",
                maxLength: 10,
                nullable: true,
                comment: "是否发热",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "是否发热");

            migrationBuilder.AlterColumn<string>(
                name: "IsFocusIsolated",
                table: "Triage_AdmissionInfo",
                maxLength: 10,
                nullable: true,
                comment: "最近14天内您是否在集中隔离医学观察场所留观",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "最近14天内您是否在集中隔离医学观察场所留观");

            migrationBuilder.AlterColumn<string>(
                name: "IsContactNewCoronavirus",
                table: "Triage_AdmissionInfo",
                maxLength: 10,
                nullable: true,
                comment: "2周内是否接触过确诊新冠阳性患者",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "2周内是否接触过确诊新冠阳性患者");

            migrationBuilder.AlterColumn<string>(
                name: "IsContactHotPatient",
                table: "Triage_AdmissionInfo",
                maxLength: 10,
                nullable: true,
                comment: "2周内是否接触过中高风险区发热患者",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "2周内是否接触过中高风险区发热患者");

            migrationBuilder.AlterColumn<string>(
                name: "IsBeenAbroad",
                table: "Triage_AdmissionInfo",
                maxLength: 10,
                nullable: true,
                comment: "2周内是否有境外旅居史",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "2周内是否有境外旅居史");

            migrationBuilder.AlterColumn<string>(
                name: "IsAggregation",
                table: "Triage_AdmissionInfo",
                maxLength: 10,
                nullable: true,
                comment: "是否聚集性发病",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "是否聚集性发病");

            migrationBuilder.AlterColumn<string>(
                name: "CountrySpecific",
                table: "Triage_AdmissionInfo",
                maxLength: 200,
                nullable: true,
                comment: "具体国家/地区",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "具体国家/地区");

            migrationBuilder.AlterColumn<string>(
                name: "ThBLevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 200,
                nullable: true,
                comment: "Ⅳb级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "Ⅳb级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "ThALevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 200,
                nullable: true,
                comment: "Ⅳa级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "Ⅳa级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "StLevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 200,
                nullable: true,
                comment: "Ⅰ级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "Ⅰ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "RdLevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 200,
                nullable: true,
                comment: "Ⅲ级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "Ⅲ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "NdLevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 200,
                nullable: true,
                comment: "Ⅱ级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "Ⅱ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultThBLevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 300,
                nullable: true,
                comment: "默认Ⅳb级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldNullable: true,
                oldComment: "默认Ⅳb级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultThALevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 200,
                nullable: true,
                comment: "默认Ⅳa级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "默认Ⅳa级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultStLevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 200,
                nullable: true,
                comment: "默认Ⅰ级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "默认Ⅰ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultRdLevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 200,
                nullable: true,
                comment: "默认Ⅲ级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "默认Ⅲ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultNdLevelExpression",
                table: "Dict_VitalSignExpression",
                maxLength: 200,
                nullable: true,
                comment: "默认Ⅱ级评分表达式",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true,
                oldComment: "默认Ⅱ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "ScoreType",
                table: "Dict_ScoreManage",
                maxLength: 20,
                nullable: true,
                comment: "评分类型",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "评分类型");

            migrationBuilder.AlterColumn<string>(
                name: "ScoreName",
                table: "Dict_ScoreManage",
                maxLength: 50,
                nullable: true,
                comment: "评分名称",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "评分名称");

            migrationBuilder.AlterColumn<string>(
                name: "DynamicLibraryName",
                table: "Dict_ScoreManage",
                maxLength: 50,
                nullable: true,
                comment: "动态库名称",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "动态库名称");

            migrationBuilder.AlterColumn<string>(
                name: "ClassName",
                table: "Dict_ScoreManage",
                maxLength: 50,
                nullable: true,
                comment: "类名",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "类名");

            migrationBuilder.AlterColumn<string>(
                name: "QueryName",
                table: "Dict_ReportSettingQueryOption",
                maxLength: 50,
                nullable: true,
                comment: "查询名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "查询名称");

            migrationBuilder.AlterColumn<string>(
                name: "ReportTypeCode",
                table: "Dict_ReportSetting",
                maxLength: 20,
                nullable: true,
                comment: "报表分类",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "报表分类");

            migrationBuilder.AlterColumn<string>(
                name: "ReportSortFiled",
                table: "Dict_ReportSetting",
                maxLength: 50,
                nullable: true,
                comment: "报表排序字段",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "报表排序字段");

            migrationBuilder.AlterColumn<string>(
                name: "ReportName",
                table: "Dict_ReportSetting",
                maxLength: 50,
                nullable: true,
                comment: "报表名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "报表名称");

            migrationBuilder.AlterColumn<string>(
                name: "TriageLevelCode",
                table: "Dict_LevelTriageRelationDirection",
                maxLength: 50,
                nullable: true,
                comment: "分诊级别代码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊级别代码");

            migrationBuilder.AlterColumn<string>(
                name: "TriageDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                maxLength: 50,
                nullable: true,
                comment: "分诊去向code",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊去向code");

            migrationBuilder.AlterColumn<string>(
                name: "OtherDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                maxLength: 50,
                nullable: true,
                comment: "其他去向code",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "其他去向code");

            migrationBuilder.AlterColumn<string>(
                name: "LevelTriageDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                maxLength: 50,
                nullable: true,
                comment: "分诊去向级别代码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊去向级别代码");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceModel",
                table: "Config_TriageDevice",
                maxLength: 100,
                nullable: true,
                comment: "设备型号",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true,
                oldComment: "设备型号");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceIPOrCom",
                table: "Config_TriageDevice",
                maxLength: 100,
                nullable: true,
                comment: "设备IP或者串口",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true,
                oldComment: "设备IP或者串口");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceCode",
                table: "Config_TriageDevice",
                maxLength: 50,
                nullable: true,
                comment: "设备编号",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "设备编号");

            migrationBuilder.AlterColumn<string>(
                name: "AccessMode",
                table: "Config_TriageDevice",
                maxLength: 50,
                nullable: true,
                comment: "接入方式",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "接入方式");

            migrationBuilder.AlterColumn<string>(
                name: "TableCode",
                table: "Config_TableSetting",
                maxLength: 50,
                nullable: true,
                comment: "表格名称（不含中文）",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "表格名称（不含中文）");

            migrationBuilder.AlterColumn<string>(
                name: "ColumnValue",
                table: "Config_TableSetting",
                maxLength: 50,
                nullable: true,
                comment: "列值",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "列值");

            migrationBuilder.AlterColumn<string>(
                name: "ColumnName",
                table: "Config_TableSetting",
                maxLength: 20,
                nullable: true,
                comment: "列名",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "列名");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneAndName",
                table: "Config_FastTrackSetting",
                maxLength: 150,
                nullable: true,
                comment: "快速通道电话和名称",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldNullable: true,
                oldComment: "快速通道电话和名称");

            migrationBuilder.AlterColumn<string>(
                name: "FastTrackPhone",
                table: "Config_FastTrackSetting",
                maxLength: 20,
                nullable: true,
                comment: "快速通道电话",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "快速通道电话");

            migrationBuilder.AlterColumn<string>(
                name: "FastTrackName",
                table: "Config_FastTrackSetting",
                maxLength: 100,
                nullable: true,
                comment: "快速通道名称",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true,
                oldComment: "快速通道名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDoctorName",
                table: "Triage_RegisterInfo");

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigTypeCode",
                table: "TriageConfigTypeDescription",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigCode",
                table: "TriageConfig",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊设置代码",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "分诊设置代码");

            migrationBuilder.AlterColumn<string>(
                name: "Py",
                table: "TriageConfig",
                type: "varchar(50)",
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "Triage_VitalSignInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "体重",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "体重");

            migrationBuilder.AlterColumn<string>(
                name: "Temp",
                table: "Triage_VitalSignInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "体温",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "体温");

            migrationBuilder.AlterColumn<string>(
                name: "SpO2",
                table: "Triage_VitalSignInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "血氧饱和度",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "血氧饱和度");

            migrationBuilder.AlterColumn<string>(
                name: "Sdp",
                table: "Triage_VitalSignInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "舒张压",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "舒张压");

            migrationBuilder.AlterColumn<string>(
                name: "Sbp",
                table: "Triage_VitalSignInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "收缩压",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "收缩压");

            migrationBuilder.AlterColumn<string>(
                name: "HeartRate",
                table: "Triage_VitalSignInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "心率",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "心率");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceCode",
                table: "Triage_VitalSignInfo",
                type: "varchar(100)",
                nullable: true,
                comment: "设备编码",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "设备编码");

            migrationBuilder.AlterColumn<string>(
                name: "BreathRate",
                table: "Triage_VitalSignInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "呼吸",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "呼吸");

            migrationBuilder.AlterColumn<string>(
                name: "ScoreType",
                table: "Triage_ScoreInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "评分类型",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "评分类型");

            migrationBuilder.AlterColumn<string>(
                name: "VisitNo",
                table: "Triage_RegisterInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "就诊次数",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "就诊次数");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterNo",
                table: "Triage_RegisterInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "挂号流水号",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "挂号流水号");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterDoctorCode",
                table: "Triage_RegisterInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "挂号医生编码",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "挂号医生编码");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterDeptCode",
                table: "Triage_RegisterInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "挂号科室编码",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "挂号科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "体重",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "体重");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfVisitCode",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "就诊类型",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "就诊类型");

            migrationBuilder.AlterColumn<string>(
                name: "TriageUserName",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊人名称",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "分诊人名称");

            migrationBuilder.AlterColumn<string>(
                name: "TriageUserCode",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊人code",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "分诊人code");

            migrationBuilder.AlterColumn<string>(
                name: "TriageSource",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它");

            migrationBuilder.AlterColumn<string>(
                name: "ToHospitalWayCode",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "来院方式",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "来院方式");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "患者性别",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "患者性别");

            migrationBuilder.AlterColumn<string>(
                name: "RFID",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "RFID",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "RFID");

            migrationBuilder.AlterColumn<string>(
                name: "Py",
                table: "Triage_PatientInfo",
                type: "varchar(100)",
                nullable: true,
                comment: "患者姓名拼音首字母",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "患者姓名拼音首字母");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "患者唯一标识(HIS)",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "患者唯一标识(HIS)");

            migrationBuilder.AlterColumn<string>(
                name: "Nation",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "民族",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "民族");

            migrationBuilder.AlterColumn<string>(
                name: "Narration",
                table: "Triage_PatientInfo",
                type: "varchar(max)",
                nullable: true,
                comment: "主诉",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "主诉");

            migrationBuilder.AlterColumn<string>(
                name: "MedicalNo",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "医保卡号",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "医保卡号");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNo",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "身份证号",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "身份证号");

            migrationBuilder.AlterColumn<string>(
                name: "Identity",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "患者身份",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "患者身份");

            migrationBuilder.AlterColumn<string>(
                name: "GreenRoadCode",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "绿色通道",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "绿色通道");

            migrationBuilder.AlterColumn<string>(
                name: "DiseaseCode",
                table: "Triage_PatientInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "重点病种Code",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "重点病种Code");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "国家",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "国家");

            migrationBuilder.AlterColumn<string>(
                name: "ContactsPhone",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "联系电话",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "联系电话");

            migrationBuilder.AlterColumn<string>(
                name: "Consciousness",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "意识",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "意识");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                table: "Triage_PatientInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "费别",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "费别");

            migrationBuilder.AlterColumn<string>(
                name: "CardNo",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "诊疗卡号",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "诊疗卡号");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Triage_PatientInfo",
                type: "nvarchar(100)",
                nullable: true,
                comment: "患者住址",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "患者住址");

            migrationBuilder.AlterColumn<string>(
                name: "GroupInjuryCode",
                table: "Triage_GroupInjuryInfo",
                type: "varchar(200)",
                nullable: true,
                comment: "群伤事件类型",
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "群伤事件类型");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Triage_FastTrackRegisterInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "性别",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "性别");

            migrationBuilder.AlterColumn<string>(
                name: "ReceptionNurseName",
                table: "Triage_FastTrackRegisterInfo",
                type: "nvarchar(100)",
                nullable: true,
                comment: "接诊护士名称",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "接诊护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "PoliceStationPhone",
                table: "Triage_FastTrackRegisterInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "所属派出所电话号码",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "所属派出所电话号码");

            migrationBuilder.AlterColumn<string>(
                name: "PoliceStationName",
                table: "Triage_FastTrackRegisterInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "所处派出所名称",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "所处派出所名称");

            migrationBuilder.AlterColumn<string>(
                name: "PoliceName",
                table: "Triage_FastTrackRegisterInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "警务人员姓名",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "警务人员姓名");

            migrationBuilder.AlterColumn<string>(
                name: "PoliceCode",
                table: "Triage_FastTrackRegisterInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "警务人员警号",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "警务人员警号");

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "Triage_FastTrackRegisterInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "患者姓名",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "患者姓名");

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "Triage_FastTrackRegisterInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "患者年龄",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "患者年龄");

            migrationBuilder.AlterColumn<string>(
                name: "TriageTargetCode",
                table: "Triage_ConsequenceInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "分诊去向编码",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "分诊去向编码");

            migrationBuilder.AlterColumn<string>(
                name: "TriageDeptCode",
                table: "Triage_ConsequenceInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "分诊科室编码",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "分诊科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeDept",
                table: "Triage_ConsequenceInfo",
                type: "varchar(60)",
                nullable: true,
                comment: "科室变更",
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "科室变更");

            migrationBuilder.AlterColumn<string>(
                name: "PastMedicalHistory",
                table: "Triage_AdmissionInfo",
                type: "varchar(500)",
                nullable: true,
                comment: "既往史",
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "既往史");

            migrationBuilder.AlterColumn<string>(
                name: "MedicalHistory",
                table: "Triage_AdmissionInfo",
                type: "varchar(500)",
                nullable: true,
                comment: "现病史",
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "现病史");

            migrationBuilder.AlterColumn<string>(
                name: "IsSoreThroatAndCough",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "是否咽痛咳嗽",
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "是否咽痛咳嗽");

            migrationBuilder.AlterColumn<string>(
                name: "IsMediumAndHighRisk",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "是否去过中高风险区",
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "是否去过中高风险区");

            migrationBuilder.AlterColumn<string>(
                name: "IsHot",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "是否发热",
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "是否发热");

            migrationBuilder.AlterColumn<string>(
                name: "IsFocusIsolated",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "最近14天内您是否在集中隔离医学观察场所留观",
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "最近14天内您是否在集中隔离医学观察场所留观");

            migrationBuilder.AlterColumn<string>(
                name: "IsContactNewCoronavirus",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "2周内是否接触过确诊新冠阳性患者",
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "2周内是否接触过确诊新冠阳性患者");

            migrationBuilder.AlterColumn<string>(
                name: "IsContactHotPatient",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "2周内是否接触过中高风险区发热患者",
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "2周内是否接触过中高风险区发热患者");

            migrationBuilder.AlterColumn<string>(
                name: "IsBeenAbroad",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "2周内是否有境外旅居史",
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "2周内是否有境外旅居史");

            migrationBuilder.AlterColumn<string>(
                name: "IsAggregation",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "是否聚集性发病",
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "是否聚集性发病");

            migrationBuilder.AlterColumn<string>(
                name: "CountrySpecific",
                table: "Triage_AdmissionInfo",
                type: "varchar(200)",
                nullable: true,
                comment: "具体国家/地区",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "具体国家/地区");

            migrationBuilder.AlterColumn<string>(
                name: "ThBLevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(200)",
                nullable: true,
                comment: "Ⅳb级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Ⅳb级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "ThALevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(200)",
                nullable: true,
                comment: "Ⅳa级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Ⅳa级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "StLevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(200)",
                nullable: true,
                comment: "Ⅰ级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Ⅰ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "RdLevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(200)",
                nullable: true,
                comment: "Ⅲ级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Ⅲ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "NdLevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(200)",
                nullable: true,
                comment: "Ⅱ级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Ⅱ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultThBLevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(300)",
                nullable: true,
                comment: "默认Ⅳb级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true,
                oldComment: "默认Ⅳb级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultThALevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(200)",
                nullable: true,
                comment: "默认Ⅳa级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "默认Ⅳa级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultStLevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(200)",
                nullable: true,
                comment: "默认Ⅰ级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "默认Ⅰ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultRdLevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(200)",
                nullable: true,
                comment: "默认Ⅲ级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "默认Ⅲ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultNdLevelExpression",
                table: "Dict_VitalSignExpression",
                type: "varchar(200)",
                nullable: true,
                comment: "默认Ⅱ级评分表达式",
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "默认Ⅱ级评分表达式");

            migrationBuilder.AlterColumn<string>(
                name: "ScoreType",
                table: "Dict_ScoreManage",
                type: "varchar(20)",
                nullable: true,
                comment: "评分类型",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "评分类型");

            migrationBuilder.AlterColumn<string>(
                name: "ScoreName",
                table: "Dict_ScoreManage",
                type: "varchar(50)",
                nullable: true,
                comment: "评分名称",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "评分名称");

            migrationBuilder.AlterColumn<string>(
                name: "DynamicLibraryName",
                table: "Dict_ScoreManage",
                type: "varchar(50)",
                nullable: true,
                comment: "动态库名称",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "动态库名称");

            migrationBuilder.AlterColumn<string>(
                name: "ClassName",
                table: "Dict_ScoreManage",
                type: "varchar(50)",
                nullable: true,
                comment: "类名",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "类名");

            migrationBuilder.AlterColumn<string>(
                name: "QueryName",
                table: "Dict_ReportSettingQueryOption",
                type: "nvarchar(max)",
                nullable: true,
                comment: "查询名称",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "查询名称");

            migrationBuilder.AlterColumn<string>(
                name: "ReportTypeCode",
                table: "Dict_ReportSetting",
                type: "varchar(20)",
                nullable: true,
                comment: "报表分类",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "报表分类");

            migrationBuilder.AlterColumn<string>(
                name: "ReportSortFiled",
                table: "Dict_ReportSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "报表排序字段",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "报表排序字段");

            migrationBuilder.AlterColumn<string>(
                name: "ReportName",
                table: "Dict_ReportSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "报表名称",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "报表名称");

            migrationBuilder.AlterColumn<string>(
                name: "TriageLevelCode",
                table: "Dict_LevelTriageRelationDirection",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊级别代码",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "分诊级别代码");

            migrationBuilder.AlterColumn<string>(
                name: "TriageDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊去向code",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "分诊去向code");

            migrationBuilder.AlterColumn<string>(
                name: "OtherDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                type: "varchar(50)",
                nullable: true,
                comment: "其他去向code",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "其他去向code");

            migrationBuilder.AlterColumn<string>(
                name: "LevelTriageDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊去向级别代码",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "分诊去向级别代码");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceModel",
                table: "Config_TriageDevice",
                type: "varchar(100)",
                nullable: true,
                comment: "设备型号",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "设备型号");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceIPOrCom",
                table: "Config_TriageDevice",
                type: "varchar(100)",
                nullable: true,
                comment: "设备IP或者串口",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "设备IP或者串口");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceCode",
                table: "Config_TriageDevice",
                type: "varchar(50)",
                nullable: true,
                comment: "设备编号",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "设备编号");

            migrationBuilder.AlterColumn<string>(
                name: "AccessMode",
                table: "Config_TriageDevice",
                type: "varchar(50)",
                nullable: true,
                comment: "接入方式",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "接入方式");

            migrationBuilder.AlterColumn<string>(
                name: "TableCode",
                table: "Config_TableSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "表格名称（不含中文）",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "表格名称（不含中文）");

            migrationBuilder.AlterColumn<string>(
                name: "ColumnValue",
                table: "Config_TableSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "列值",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "列值");

            migrationBuilder.AlterColumn<string>(
                name: "ColumnName",
                table: "Config_TableSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "列名",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "列名");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneAndName",
                table: "Config_FastTrackSetting",
                type: "varchar(150)",
                nullable: true,
                comment: "快速通道电话和名称",
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true,
                oldComment: "快速通道电话和名称");

            migrationBuilder.AlterColumn<string>(
                name: "FastTrackPhone",
                table: "Config_FastTrackSetting",
                type: "varchar(20)",
                nullable: true,
                comment: "快速通道电话",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "快速通道电话");

            migrationBuilder.AlterColumn<string>(
                name: "FastTrackName",
                table: "Config_FastTrackSetting",
                type: "varchar(100)",
                nullable: true,
                comment: "快速通道名称",
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "快速通道名称");

            migrationBuilder.CreateTable(
                name: "AbpPermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissionGrants_Name_ProviderName_ProviderKey",
                table: "AbpPermissionGrants",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });
        }
    }
}
