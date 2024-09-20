using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Config_TableSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "启用状态 0：已启用 1：未启用"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    TableCode = table.Column<string>(nullable: true, comment: "表格名称（不含中文）"),
                    ColumnValue = table.Column<string>(nullable: true, comment: "列值"),
                    ColumnName = table.Column<string>(nullable: true, comment: "列名"),
                    ColumnWidth = table.Column<int>(nullable: false, comment: "列宽"),
                    SequenceNo = table.Column<int>(nullable: false, comment: "序号"),
                    Visible = table.Column<bool>(nullable: false, comment: "显示 0：不显示  1：显示"),
                    ShowOverflowTooltip = table.Column<bool>(nullable: false, comment: "单元格内文本是否换行 0：不换行，鼠标移上显示更多  1：换行，展示所有数据"),
                    DefaultColumnName = table.Column<string>(nullable: true, comment: "列名(默认值)"),
                    DefaultColumnWidth = table.Column<int>(nullable: false, comment: "列宽(默认值)"),
                    DefaultSequenceNo = table.Column<int>(nullable: false, comment: "序号(默认值)"),
                    DefaultVisible = table.Column<bool>(nullable: false, comment: "显示(默认值) 0：不显示  1：显示"),
                    DefaultShowOverflowTooltip = table.Column<bool>(nullable: false, comment: "单元格内文本是否换行(默认值) 0：不换行，鼠标移上显示更多  1：换行，展示所有数据")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config_TableSetting", x => x.Id);
                },
                comment: "表格配置表");

            migrationBuilder.CreateTable(
                name: "Dict_JudgmentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    DeptName = table.Column<string>(nullable: true, comment: "科室分类名称"),
                    AdditionalScore = table.Column<int>(nullable: false, comment: "额外提升分诊等级"),
                    TriageDeptCode = table.Column<string>(nullable: true, comment: "对应科室"),
                    Py = table.Column<string>(type: "varchar(50)", nullable: true, comment: "拼音码")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_JudgmentType", x => x.Id);
                },
                comment: "院前分诊判定依据科室分类表");

            migrationBuilder.CreateTable(
                name: "Dict_LevelTriageRelationDirection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    LevelTriageDirectionCode = table.Column<string>(type: "varchar(20)", nullable: true, comment: "分诊去向级别代码"),
                    TriageDirectionId = table.Column<Guid>(nullable: false, comment: "分诊去向Id"),
                    OtherDirectionId = table.Column<Guid>(nullable: false, comment: "其他去向ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_LevelTriageRelationDirection", x => x.Id);
                },
                comment: "院前分诊级别关联取消字典表");

            migrationBuilder.CreateTable(
                name: "Dict_ScoreManage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    ScoreName = table.Column<string>(type: "varchar(50)", nullable: true, comment: "评分名称"),
                    ScoreType = table.Column<string>(type: "varchar(20)", nullable: true, comment: "评分类型"),
                    DynamicLibraryName = table.Column<string>(type: "varchar(50)", nullable: true, comment: "动态库名称"),
                    ClassName = table.Column<string>(type: "varchar(50)", nullable: true, comment: "类名"),
                    IsEnable = table.Column<bool>(nullable: false, comment: "是否启用")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ScoreManage", x => x.Id);
                },
                comment: "评分管理表");

            migrationBuilder.CreateTable(
                name: "Dict_VitalSignExpression",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    ItemName = table.Column<string>(type: "nvarchar(200)", nullable: true, comment: "评分项"),
                    StLevelExpression = table.Column<string>(type: "varchar(200)", nullable: true, comment: "Ⅰ级评分表达式"),
                    NdLevelExpression = table.Column<string>(type: "varchar(200)", nullable: true, comment: "Ⅱ级评分表达式"),
                    RdLevelExpression = table.Column<string>(type: "varchar(200)", nullable: true, comment: "Ⅲ级评分表达式"),
                    ThALevelExpression = table.Column<string>(type: "varchar(200)", nullable: true, comment: "Ⅳa级评分表达式"),
                    ThBLevelExpression = table.Column<string>(type: "varchar(200)", nullable: true, comment: "Ⅳb级评分表达式"),
                    DefaultStLevelExpression = table.Column<string>(type: "varchar(200)", nullable: true, comment: "默认Ⅰ级评分表达式"),
                    DefaultNdLevelExpression = table.Column<string>(type: "varchar(200)", nullable: true, comment: "默认Ⅱ级评分表达式"),
                    DefaultRdLevelExpression = table.Column<string>(type: "varchar(200)", nullable: true, comment: "默认Ⅲ级评分表达式"),
                    DefaultThALevelExpression = table.Column<string>(type: "varchar(200)", nullable: true, comment: "默认Ⅳa级评分表达式"),
                    DefaultThBLevelExpression = table.Column<string>(type: "varchar(300)", nullable: true, comment: "默认Ⅳb级评分表达式")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_VitalSignExpression", x => x.Id);
                },
                comment: "院前分诊生命体征评级标准表");

            migrationBuilder.CreateTable(
                name: "Triage_GroupInjuryInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "详细描述"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    HappeningTime = table.Column<DateTime>(nullable: false, comment: "事件发生时间"),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "概要说明"),
                    GroupInjuryCode = table.Column<string>(type: "varchar(200)", nullable: true, comment: "群伤事件类型")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_GroupInjuryInfo", x => x.Id);
                },
                comment: "院前分诊群伤表");

            migrationBuilder.CreateTable(
                name: "Triage_MergeRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_MergeRecord", x => x.Id);
                },
                comment: "院前分诊患者档案合并记录表");

            migrationBuilder.CreateTable(
                name: "Triage_PatientInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "分诊时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    TaskInfoId = table.Column<Guid>(nullable: false, comment: "任务单号"),
                    CarNum = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "车牌号"),
                    VisitNo = table.Column<string>(type: "varchar(20)", nullable: true, comment: "就诊次数"),
                    PatientId = table.Column<string>(type: "varchar(50)", nullable: true, comment: "患者唯一标识(HIS)"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "患者姓名"),
                    Sex = table.Column<string>(type: "varchar(10)", nullable: true, comment: "患者性别"),
                    Birthday = table.Column<DateTime>(nullable: true, comment: "患者出生日期"),
                    Address = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "患者住址"),
                    ContactsPerson = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "紧急联系人"),
                    ContactsPhone = table.Column<string>(type: "varchar(20)", nullable: true, comment: "联系电话"),
                    ToHospitalWayCode = table.Column<string>(type: "varchar(20)", nullable: true, comment: "来院方式"),
                    Identity = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "患者身份"),
                    ChargeType = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "费别"),
                    IdentityNo = table.Column<string>(type: "varchar(20)", nullable: true, comment: "身份证号"),
                    Nation = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "民族"),
                    Country = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "国家"),
                    GreenRoadCode = table.Column<string>(type: "varchar(20)", nullable: true, comment: "绿色通道"),
                    OnsetTime = table.Column<DateTime>(nullable: true, comment: "发病时间"),
                    TriageUserCode = table.Column<string>(type: "varchar(20)", nullable: true, comment: "分诊人"),
                    CardNo = table.Column<string>(type: "varchar(20)", nullable: true, comment: "诊疗卡号"),
                    MedicalNo = table.Column<string>(type: "varchar(20)", nullable: true, comment: "医保卡号"),
                    RFID = table.Column<string>(type: "varchar(20)", nullable: true, comment: "RFID"),
                    Age = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "年龄"),
                    Py = table.Column<string>(type: "varchar(100)", nullable: true, comment: "患者姓名拼音首字母"),
                    DiseaseCode = table.Column<string>(type: "varchar(20)", nullable: true, comment: "重点病种Code"),
                    StartTriageTime = table.Column<DateTime>(nullable: true, comment: "开始分诊时间"),
                    MergeRecordId = table.Column<Guid>(nullable: false, comment: "合并档案表主键Id"),
                    GroupInjuryInfoId = table.Column<Guid>(nullable: false, comment: "群伤事件Id"),
                    TriageSource = table.Column<string>(type: "varchar(20)", nullable: true, comment: "分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它"),
                    TriageStatus = table.Column<int>(nullable: false, comment: "分诊状态，0：暂存，1：分诊")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_PatientInfo", x => x.Id);
                },
                comment: "院前分诊患者信息表");

            migrationBuilder.CreateTable(
                name: "TriageConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(maxLength: 256, nullable: true),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true),
                    TriageConfigCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    TriageConfigName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsDisable = table.Column<int>(nullable: false),
                    TriageConfigType = table.Column<int>(nullable: false),
                    Py = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriageConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TriageConfigTypeDescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(maxLength: 256, nullable: true),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true),
                    TriageConfigTypeCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    TriageConfigTypeName = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriageConfigTypeDescription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dict_JudgmentMaster",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    JudgmentTypeId = table.Column<Guid>(nullable: false, comment: "判定依据科室分类主键Id"),
                    EmergencyLevel = table.Column<int>(nullable: false, comment: "级别"),
                    ItemDescription = table.Column<string>(nullable: true, comment: "主诉名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_JudgmentMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dict_JudgmentMaster_Dict_JudgmentType_JudgmentTypeId",
                        column: x => x.JudgmentTypeId,
                        principalTable: "Dict_JudgmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "院前分诊判定依据主诉分类表");

            migrationBuilder.CreateTable(
                name: "Triage_ConsequenceInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    PatientInfoId = table.Column<Guid>(nullable: false, comment: "院前分诊患者建档表主键Id"),
                    TriageDeptCode = table.Column<string>(type: "varchar(20)", nullable: true, comment: "分诊科室编码"),
                    TriageTargetCode = table.Column<string>(type: "varchar(20)", nullable: true, comment: "分诊去向编码"),
                    ActTriageLevel = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "实际分诊级别"),
                    AutoTriageLevel = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "自动分诊级别"),
                    ChangeLevel = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "分诊级别变更")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_ConsequenceInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triage_ConsequenceInfo_Triage_PatientInfo_PatientInfoId",
                        column: x => x.PatientInfoId,
                        principalTable: "Triage_PatientInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "院前分诊结果表");

            migrationBuilder.CreateTable(
                name: "Triage_RegisterInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "挂号状态 0：已挂号 1：已退号"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    RegisterNo = table.Column<string>(nullable: true, comment: "挂号流水号"),
                    RegisterDeptCode = table.Column<string>(nullable: true, comment: "挂号科室编码"),
                    RegisterDoctorCode = table.Column<string>(nullable: true, comment: "挂号医生编码"),
                    RegisterTime = table.Column<DateTime>(nullable: false, comment: "挂号时间"),
                    PatientInfoId = table.Column<Guid>(nullable: false, comment: "分诊患者基本信息表Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_RegisterInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triage_RegisterInfo_Triage_PatientInfo_PatientInfoId",
                        column: x => x.PatientInfoId,
                        principalTable: "Triage_PatientInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "患者挂号信息表");

            migrationBuilder.CreateTable(
                name: "Triage_ScoreInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    PatientInfoId = table.Column<Guid>(nullable: false, comment: "院前分诊患者建档表主键Id"),
                    ScoreType = table.Column<string>(type: "varchar(20)", nullable: true, comment: "评分类型"),
                    ScoreValue = table.Column<int>(nullable: false, comment: "评分数值"),
                    ScoreDescription = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "评分等级"),
                    ScoreContent = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "评分内容 Json字符串")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_ScoreInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triage_ScoreInfo_Triage_PatientInfo_PatientInfoId",
                        column: x => x.PatientInfoId,
                        principalTable: "Triage_PatientInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "院前分诊评分表");

            migrationBuilder.CreateTable(
                name: "Triage_VitalSignInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    PatientInfoId = table.Column<Guid>(nullable: false, comment: "院前分诊患者建档表主键Id"),
                    Sbp = table.Column<string>(type: "varchar(20)", nullable: true, comment: "收缩压"),
                    Sdp = table.Column<string>(type: "varchar(20)", nullable: true, comment: "舒张压"),
                    SpO2 = table.Column<string>(type: "varchar(20)", nullable: true, comment: "血氧饱和度"),
                    BreathRate = table.Column<string>(type: "varchar(20)", nullable: true, comment: "呼吸"),
                    Temp = table.Column<string>(type: "varchar(20)", nullable: true, comment: "体温"),
                    HeartRate = table.Column<string>(type: "varchar(20)", nullable: true, comment: "心率"),
                    Weight = table.Column<string>(type: "varchar(20)", nullable: true, comment: "体重")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_VitalSignInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triage_VitalSignInfo_Triage_PatientInfo_PatientInfoId",
                        column: x => x.PatientInfoId,
                        principalTable: "Triage_PatientInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "院前分诊生命体征表");

            migrationBuilder.CreateTable(
                name: "Dict_JudgmentItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    JudgmentMasterId = table.Column<Guid>(nullable: false, comment: "判定依据主诉分类主键Id"),
                    EmergencyLevel = table.Column<int>(nullable: false, comment: "级别"),
                    ItemDescription = table.Column<string>(nullable: true, comment: "分诊依据"),
                    IsGreenRoad = table.Column<bool>(nullable: false, comment: "是否属于绿色通道 0：不属于 1：属于"),
                    Expression = table.Column<string>(nullable: true, comment: "表达式")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_JudgmentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dict_JudgmentItem_Dict_JudgmentMaster_JudgmentMasterId",
                        column: x => x.JudgmentMasterId,
                        principalTable: "Dict_JudgmentMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "院前分诊判定依据项目表");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_JudgmentItem_JudgmentMasterId",
                table: "Dict_JudgmentItem",
                column: "JudgmentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_JudgmentMaster_JudgmentTypeId",
                table: "Dict_JudgmentMaster",
                column: "JudgmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Triage_ConsequenceInfo_PatientInfoId",
                table: "Triage_ConsequenceInfo",
                column: "PatientInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Triage_RegisterInfo_PatientInfoId",
                table: "Triage_RegisterInfo",
                column: "PatientInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Triage_ScoreInfo_PatientInfoId",
                table: "Triage_ScoreInfo",
                column: "PatientInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Triage_VitalSignInfo_PatientInfoId",
                table: "Triage_VitalSignInfo",
                column: "PatientInfoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config_TableSetting");

            migrationBuilder.DropTable(
                name: "Dict_JudgmentItem");

            migrationBuilder.DropTable(
                name: "Dict_LevelTriageRelationDirection");

            migrationBuilder.DropTable(
                name: "Dict_ScoreManage");

            migrationBuilder.DropTable(
                name: "Dict_VitalSignExpression");

            migrationBuilder.DropTable(
                name: "Triage_ConsequenceInfo");

            migrationBuilder.DropTable(
                name: "Triage_GroupInjuryInfo");

            migrationBuilder.DropTable(
                name: "Triage_MergeRecord");

            migrationBuilder.DropTable(
                name: "Triage_RegisterInfo");

            migrationBuilder.DropTable(
                name: "Triage_ScoreInfo");

            migrationBuilder.DropTable(
                name: "Triage_VitalSignInfo");

            migrationBuilder.DropTable(
                name: "TriageConfig");

            migrationBuilder.DropTable(
                name: "TriageConfigTypeDescription");

            migrationBuilder.DropTable(
                name: "Dict_JudgmentMaster");

            migrationBuilder.DropTable(
                name: "Triage_PatientInfo");

            migrationBuilder.DropTable(
                name: "Dict_JudgmentType");
        }
    }
}
