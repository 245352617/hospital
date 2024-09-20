using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations.Migrations
{
    public partial class V2251 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Duct_Canula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "导管主表主键"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理时间"),
                    CanulaWay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "置入方式"),
                    CanulaLength = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "置管长度"),
                    NurseId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "护士名称"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_Canula", x => x.Id);
                },
                comment: "表:导管护理记录信息");

            migrationBuilder.CreateTable(
                name: "Duct_CanulaDynamic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "导管主表主键"),
                    GroupName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "管道分类"),
                    ParaCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "参数代码"),
                    ParaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "参数名称"),
                    ParaValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "参数值"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_CanulaDynamic", x => x.Id);
                },
                comment: "表:导管参数动态");

            migrationBuilder.CreateTable(
                name: "Duct_CanulaPart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ModuleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PartName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_CanulaPart", x => x.Id);
                },
                comment: "表:人体图-编号字典");

            migrationBuilder.CreateTable(
                name: "Duct_Dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParaCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "参数代码"),
                    ParaName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "参数名称"),
                    DictCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "字典代码"),
                    DictValue = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "字典值"),
                    DictDesc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "字典值说明"),
                    ParentId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "上级代码"),
                    DictStandard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "字典标准（国标、自定义）"),
                    HisCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "HIS对照代码"),
                    HisName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "HIS对照"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室代码"),
                    ModuleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "模块代码"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, comment: "是否默认"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_Dict", x => x.Id);
                },
                comment: "表:导管字典-通用业务");

            migrationBuilder.CreateTable(
                name: "Duct_NursingCanula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者id"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "插管时间"),
                    StopTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "拔管时间"),
                    ModuleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "导管分类"),
                    ModuleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "排序"),
                    CanulaName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "管道名称"),
                    CanulaPart = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "插管部位"),
                    CanulaNumber = table.Column<int>(type: "int", nullable: true, comment: "插管次数"),
                    CanulaPosition = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "插管地点"),
                    DoctorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "置管人代码"),
                    DoctorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "置管人名称"),
                    CanulaWay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "置入方式"),
                    CanulaLength = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "置管长度"),
                    DrawReason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "拔管原因"),
                    TubeDrawState = table.Column<int>(type: "int", nullable: false, comment: "管道状态"),
                    UseFlag = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "使用标志：（Y在用，N已拔管）"),
                    NurseId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "护士名称"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "护理时间"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_NursingCanula", x => x.Id);
                },
                comment: "表:导管护理信息");

            migrationBuilder.CreateTable(
                name: "Duct_NursingEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NurseDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理日期"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理时间"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者id"),
                    Context = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "内容"),
                    NurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护士工号"),
                    NurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护士名称"),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "记录时间"),
                    AuditNurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "审核人"),
                    AuditNurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "审核人名称"),
                    AuditTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "审核时间"),
                    AuditState = table.Column<int>(type: "int", nullable: true, comment: "审核状态（0-未审核，1，已审核，2-取消审核）"),
                    SignatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "签名记录编号对应icu_signature的id"),
                    Sort = table.Column<int>(type: "int", nullable: true, comment: "排序"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_NursingEvent", x => x.Id);
                },
                comment: "表:护理记录");

            migrationBuilder.CreateTable(
                name: "Duct_ParaItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编号"),
                    ModuleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "参数所属模块"),
                    ParaCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "项目代码"),
                    ParaName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "项目名称"),
                    DisplayName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true, comment: "显示名称"),
                    ScoreCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "评分代码"),
                    GroupName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "导管分类"),
                    UnitName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位名称"),
                    ValueType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "数据类型"),
                    Style = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "文本类型"),
                    DecimalDigits = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "小数位数"),
                    MaxValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "最大值"),
                    MinValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "最小值"),
                    HighValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "高值"),
                    LowValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "低值"),
                    Threshold = table.Column<bool>(type: "bit", nullable: false, comment: "是否预警"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    DataSource = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "默认值"),
                    DictFlag = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "导管项目是否静态显示"),
                    GuidFlag = table.Column<bool>(type: "bit", nullable: true, comment: "是否评分"),
                    GuidId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "评分指引编号"),
                    StatisticalType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "特护单统计参数分类"),
                    DrawChartFlag = table.Column<int>(type: "int", nullable: false, comment: "绘制趋势图形"),
                    ColorParaCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "关联颜色"),
                    ColorParaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "关联颜色名称"),
                    PropertyParaCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "关联性状"),
                    PropertyParaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "关联性状名称"),
                    DeviceParaCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "设备参数代码"),
                    DeviceParaType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "设备参数类型（1-测量值，2-设定值）"),
                    HealthSign = table.Column<int>(type: "int", nullable: true, comment: "是否生命体征项目"),
                    KBCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "知识库代码"),
                    NuringViewCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护理概览参数"),
                    AbnormalSign = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "是否异常体征参数"),
                    IsUsage = table.Column<bool>(type: "bit", nullable: true, comment: "是否用药所属项目"),
                    AddSource = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "添加来源"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ParaItemType = table.Column<int>(type: "int", nullable: false, comment: "项目参数类型，用于区分监护仪或者呼吸机等"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_ParaItem", x => x.Id);
                },
                comment: "表:导管护理项目表");

            migrationBuilder.CreateTable(
                name: "Duct_ParaModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "模块代码"),
                    ModuleName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "模块名称"),
                    DisplayName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true, comment: "模块显示名称"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室代码"),
                    ModuleType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）"),
                    IsBloodFlow = table.Column<bool>(type: "bit", nullable: false, comment: "是否血流内导管"),
                    Py = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "模块拼音"),
                    Sort = table.Column<int>(type: "int", maxLength: 10, nullable: false, comment: "排序"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_ParaModule", x => x.Id);
                },
                comment: "表:导管模块参数");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveHeader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "项目编码"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "项目名称"),
                    Unit = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "单位"),
                    MaxValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "最大值"),
                    MinValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "最小值"),
                    MaxEarlyWarning = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "最大值预警"),
                    MinEarlyWarning = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "最小值预警"),
                    ParentCode = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "父级编码"),
                    Grade = table.Column<int>(type: "int", nullable: false, comment: "等级，1:一级，2：二级"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveHeader", x => x.Id);
                },
                comment: "病情观察头部");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveMain",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "traige患者id"),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "患者id"),
                    VisitNo = table.Column<int>(type: "int", nullable: false, comment: "就诊号"),
                    ObserveTime = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "时间"),
                    Temperature = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "体温"),
                    HeartRate = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "心率"),
                    Breathing = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "呼吸"),
                    Sbp = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血压BP收缩压"),
                    Sdp = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血压BP舒张压"),
                    SpO2 = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血氧饱和度"),
                    ConsciousnessCode = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "意识编码"),
                    ConsciousnessName = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "意识名称"),
                    BloodSugar = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "血糖"),
                    LeftPupilType = table.Column<int>(type: "int", nullable: false, comment: "左瞳类型，0：灵敏，1：迟钝，2：无反应"),
                    LeftPupilSize = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "左瞳大小"),
                    RightPupilType = table.Column<int>(type: "int", nullable: false, comment: "右瞳类型，0：灵敏，1：迟钝，2：无反应"),
                    RightPupilSize = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "右瞳大小"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveMain", x => x.Id);
                },
                comment: "病情观察");

            migrationBuilder.CreateTable(
                name: "NursingLis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱ID"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检验类别编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "检验类别"),
                    ClinicalSymptom = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "临床症状"),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false, comment: "是否紧急"),
                    IsBedSide = table.Column<bool>(type: "bit", nullable: false, comment: "是否在床旁"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "标本编码"),
                    SpecimenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "标本名称"),
                    SpecimenCollectDatetime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "标本采集时间"),
                    SpecimenReceivedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "标本接收时间"),
                    SpecimenPartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本采集部位编码"),
                    SpecimenPartName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本采集部位"),
                    ContainerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器代码"),
                    ContainerName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器"),
                    ContainerColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器颜色:0=红帽,1=蓝帽,2=紫帽"),
                    SpecimenDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "标本说明"),
                    HasReport = table.Column<bool>(type: "bit", nullable: false, comment: "报告标识"),
                    ReportTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "出报告时间"),
                    ReportDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认报告医生编码"),
                    ReportDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认报告医生"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingLis", x => x.Id);
                },
                comment: "检验");

            migrationBuilder.CreateTable(
                name: "NursingOwnMedicine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnMedicineId = table.Column<int>(type: "int", nullable: false, comment: "医生站OwnMedicineId"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "系统标识: 0=急诊，1=院前"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者唯一标识"),
                    PatientId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者Id"),
                    PatientName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "患者名称"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "医嘱名称"),
                    ApplyTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "开嘱时间"),
                    ApplyDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "申请医生编码"),
                    ApplyDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "申请医生"),
                    ApplyDeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室编码"),
                    ApplyDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室"),
                    RecieveQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "领量(数量)"),
                    RecieveUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "领量单位"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法名称"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "每次剂量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂量单位"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次"),
                    FrequencyTimes = table.Column<int>(type: "int", nullable: true, comment: "在一个周期内执行的次数"),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "医嘱说明")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingOwnMedicine", x => x.Id);
                },
                comment: "自备药");

            migrationBuilder.CreateTable(
                name: "NursingPacs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱ID"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检查类别编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "检查类别"),
                    ClinicalSymptom = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "临床症状"),
                    MedicalHistory = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "病史简要"),
                    PartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "检查部位编码"),
                    PartName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位"),
                    CatalogDisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "分类类型名称 例如心电图申请单、超声申请单"),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false, comment: "是否紧急"),
                    IsBedSide = table.Column<bool>(type: "bit", nullable: false, comment: "是否在床旁"),
                    HasReport = table.Column<bool>(type: "bit", nullable: false, comment: "报告标识"),
                    ReportTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "出报告时间"),
                    ReportDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认报告医生编码"),
                    ReportDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认报告医生"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingPacs", x => x.Id);
                },
                comment: "检查");

            migrationBuilder.CreateTable(
                name: "NursingPrescribe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱主表ID"),
                    IsOutDrug = table.Column<bool>(type: "bit", nullable: false, comment: "是否自备药：false=非自备药,true=自备药"),
                    MedicineProperty = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "药物属性：西药、中药、西药制剂、中药制剂"),
                    ToxicProperty = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "药理等级"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法名称"),
                    Speed = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "滴速"),
                    LongDays = table.Column<int>(type: "int", nullable: false, comment: "开药天数"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "每次剂量"),
                    QtyPerTimes = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "每次用量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂量单位"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "包装规格"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "频次"),
                    FrequencyTimes = table.Column<int>(type: "int", nullable: true, comment: "在一个周期内执行的次数"),
                    FrequencyUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时"),
                    FrequencyExecDayTimes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "一天内的执行时间"),
                    PharmacyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "药房编码"),
                    PharmacyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药房名称"),
                    IsSkinTest = table.Column<bool>(type: "bit", nullable: true, comment: "是否皮试 false=不需要皮试 true=需要皮试"),
                    MaterialPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "耗材金额"),
                    IsAmendedMark = table.Column<bool>(type: "bit", nullable: true, comment: "是否抢救后补：false=非抢救后补，true=抢救后补"),
                    IsAdaptationDisease = table.Column<bool>(type: "bit", nullable: true, comment: "是否医保适应症"),
                    IsFirstAid = table.Column<bool>(type: "bit", nullable: true, comment: "是否是急救药"),
                    SkinTestResult = table.Column<bool>(type: "bit", nullable: true, comment: "皮试结果:false=阴性 ture=阳性"),
                    SkinTestSignChoseResult = table.Column<int>(type: "int", nullable: true, comment: "皮试选择结果,默认空什么都没选择，0=否，1=是，2=续用"),
                    ActualDays = table.Column<int>(type: "int", nullable: true, comment: "实际天数"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingPrescribe", x => x.Id);
                },
                comment: "药物处方");

            migrationBuilder.CreateTable(
                name: "NursingRecipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HisOrderNo = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "HIS医嘱号"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "系统标识: 0=急诊，1=院前"),
                    ItemType = table.Column<int>(type: "int", nullable: false, comment: "医嘱各项分类: 0=药品,1=检查,2=检验,3=诊疗"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者入科流水号"),
                    PatientId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者Id"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "患者名称"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "医嘱名称"),
                    CategoryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱项目分类编码"),
                    CategoryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)"),
                    IsBackTracking = table.Column<bool>(type: "bit", nullable: false, comment: "是否补录"),
                    PrescriptionNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "处方号"),
                    RecipeNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱号"),
                    RecipeGroupNo = table.Column<int>(type: "int", nullable: false, comment: "医嘱子号"),
                    PrescribeTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型编码"),
                    PrescribeTypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型：临嘱、长嘱、出院带药等"),
                    ApplyTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "开嘱时间"),
                    ApplyDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "开嘱医生编码"),
                    ApplyDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "开嘱医生名称"),
                    ApplyDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "开嘱科室编码"),
                    ApplyDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "开嘱科室名称"),
                    TraineeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "管培生编码"),
                    TraineeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "管培生名称"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行科室编码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行科室名称"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    InsuranceType = table.Column<int>(type: "int", nullable: false, comment: "医保目录:0=自费,1=甲类,2=乙类,3=其它"),
                    IsChronicDisease = table.Column<bool>(type: "bit", nullable: true, comment: "是否慢性病"),
                    PrintedTimes = table.Column<int>(type: "int", nullable: false, comment: "打印次数"),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "临床诊断"),
                    NursingStatus = table.Column<int>(type: "int", nullable: false, comment: "护理医嘱状态:0=未知,1=已提交(医生站)->未执行(护士站),2=已确认,3=已作废,4=已停止(医生站)->停复核(护士站),5=已提交(医生站)->需要复核->未复核(护士站),6=已驳回,7=已执行,8:已停嘱(医生站)->停复核(护士站)"),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "医嘱说明"),
                    RecieveQty = table.Column<int>(type: "int", nullable: false, comment: "数量"),
                    RecieveUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "开始时间"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "结束时间"),
                    PayTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "付费类型编码"),
                    PayType = table.Column<int>(type: "int", nullable: false, comment: "付费类型: 0=自费,1=医保,2=其它"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "收费单位"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "收费单价"),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "总费用"),
                    ChargeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "收费类型编码"),
                    ChargeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费类型名称"),
                    PayStatus = table.Column<int>(type: "int", nullable: false, comment: "支付状态 , 0=未支付,1=已支付,2=部分支付,3=已退费"),
                    ExecutorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行者编码"),
                    ExecutorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行者名称"),
                    ExecTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "执行时间"),
                    StopDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "停嘱医生代码"),
                    StopDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "停嘱医生"),
                    StopTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "停嘱生效时间"),
                    StopOptTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "停嘱操作时间"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipe", x => x.Id);
                },
                comment: "医嘱");

            migrationBuilder.CreateTable(
                name: "NursingRecipeExec",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsInfusion = table.Column<bool>(type: "bit", nullable: false, comment: "是否输液"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "用法名称"),
                    ConversionTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "拆分时间"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "护理时间"),
                    PlanExcuteTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "计划执行时间"),
                    TotalDosage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "总剂量"),
                    DosageQty = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "每次剂量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "每次剂量单位"),
                    IfCalcIn = table.Column<int>(type: "int", nullable: true, comment: "是否计算进入量"),
                    SortNum = table.Column<int>(type: "int", nullable: false, comment: "排序编号"),
                    NursingStatus = table.Column<int>(type: "int", nullable: false, comment: "医嘱执行状态"),
                    CheckNurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "核对护士"),
                    CheckNurseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "核对护士名称"),
                    CheckTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "核对时间"),
                    CheckStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "核对结果"),
                    ExecuteType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "执行方式PC/PDA"),
                    ExcuteNurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行护士"),
                    ExcuteNurseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行护士名称"),
                    ExcuteNurseTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "护士执行时间"),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "执行完成时间"),
                    FinishNurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "完成护士"),
                    FinishNurse = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "完成护士名称"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "关联医嘱表编号"),
                    RecipeNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱号"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "病人标识"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "系统标识: 0=急诊，1=院前")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipeExec", x => x.Id);
                },
                comment: "医嘱执行");

            migrationBuilder.CreateTable(
                name: "NursingRecipeExecHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanExcuteTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "计划执行时间"),
                    OperationTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "实际操作时间"),
                    NurseCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "护士编码"),
                    NurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护士名称"),
                    LastStatus = table.Column<int>(type: "int", nullable: false, comment: "操作前状态"),
                    CombineStatus = table.Column<int>(type: "int", nullable: false, comment: "操作后状态"),
                    OperationType = table.Column<int>(type: "int", nullable: false, comment: "操作类型"),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱Id"),
                    RecipeExecId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱执行Id"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipeExecHistory", x => x.Id);
                },
                comment: "医嘱执行历史记录");

            migrationBuilder.CreateTable(
                name: "NursingRecipeExecRefund",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExecId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "关联医嘱执行表编号"),
                    RecipeNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱号"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "病人标识"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "系统标识: 0=急诊，1=院前"),
                    RefundType = table.Column<int>(type: "int", nullable: false, comment: "退药退费类型"),
                    IsWithDrawn = table.Column<bool>(type: "bit", nullable: false, comment: "是否退药退费"),
                    NursingRefundStatus = table.Column<int>(type: "int", nullable: false, comment: "退药退费状态"),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "申请时间"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "规格"),
                    RefundQty = table.Column<int>(type: "int", nullable: false, comment: "数量"),
                    NurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "申请护士编码"),
                    NurseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请护士名称"),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "原因"),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "审批时间"),
                    ConfirmmerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "审批人编码"),
                    ConfirmmerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "审批人名称"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipeExecRefund", x => x.Id);
                },
                comment: "医嘱执行退款退费表");

            migrationBuilder.CreateTable(
                name: "NursingRecipeHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱ID"),
                    Operation = table.Column<int>(type: "int", nullable: false, comment: "操作类型"),
                    OperatorCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "操作人编码"),
                    OperatorName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "操作人名称"),
                    OperaTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "操作时间"),
                    Remark = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "备注"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipeHistory", x => x.Id);
                },
                comment: "医嘱操作历史");

            migrationBuilder.CreateTable(
                name: "NursingTreat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱ID"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "其它价格"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "规格"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认频次代码"),
                    FeeTypeMainCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费大类代码"),
                    FeeTypeSubCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费小类代码"),
                    AdditionalItemsType = table.Column<int>(type: "int", nullable: false, comment: "附加类型"),
                    AdditionalItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "处置关联处方医嘱ID"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingTreat", x => x.Id);
                },
                comment: "诊疗");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveInput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IllnessObserveMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主表id"),
                    TotalInput = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "入量合计"),
                    BloodTransfusion = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "输血"),
                    Diet = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "饮食"),
                    Water = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "水"),
                    NasalFeeding = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "鼻饲"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveInput", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingIllnessObserveInput_NursingIllnessObserveMain_IllnessObserveMainId",
                        column: x => x.IllnessObserveMainId,
                        principalTable: "NursingIllnessObserveMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "病情观察入量表");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveOther",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IllnessObserveMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主表id"),
                    G = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "GCS分"),
                    C = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "GCS分"),
                    S = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "GCS分"),
                    ElectricCardioversion = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "电复律"),
                    TurnOver = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "翻身"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveOther", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingIllnessObserveOther_NursingIllnessObserveMain_IllnessObserveMainId",
                        column: x => x.IllnessObserveMainId,
                        principalTable: "NursingIllnessObserveMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "病情观察其他");

            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveOutput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IllnessObserveMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主表id"),
                    TotalOutput = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "出量合计"),
                    UrineVolume = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "尿量"),
                    Shit = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "大便"),
                    Vomit = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "呕吐"),
                    Sweat = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "汗液"),
                    SputumSuction = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "吸痰"),
                    VenousInflow = table.Column<string>(type: "nvarchar(10)", nullable: true, comment: "静脉入量"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingIllnessObserveOutput", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingIllnessObserveOutput_NursingIllnessObserveMain_IllnessObserveMainId",
                        column: x => x.IllnessObserveMainId,
                        principalTable: "NursingIllnessObserveMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "病情观察出量表");

            migrationBuilder.CreateIndex(
                name: "IX_Duct_Dict_ParaCode",
                table: "Duct_Dict",
                column: "ParaCode");

            migrationBuilder.CreateIndex(
                name: "IX_Duct_ParaItem_DeptCode",
                table: "Duct_ParaItem",
                column: "DeptCode");

            migrationBuilder.CreateIndex(
                name: "IX_Duct_ParaModule_ModuleCode",
                table: "Duct_ParaModule",
                column: "ModuleCode");

            migrationBuilder.CreateIndex(
                name: "IX_NursingIllnessObserveInput_IllnessObserveMainId",
                table: "NursingIllnessObserveInput",
                column: "IllnessObserveMainId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NursingIllnessObserveOther_IllnessObserveMainId",
                table: "NursingIllnessObserveOther",
                column: "IllnessObserveMainId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NursingIllnessObserveOutput_IllnessObserveMainId",
                table: "NursingIllnessObserveOutput",
                column: "IllnessObserveMainId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipe_Code",
                table: "NursingRecipe",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipe_Name",
                table: "NursingRecipe",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipeHistory_RecipeId",
                table: "NursingRecipeHistory",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Duct_Canula");

            migrationBuilder.DropTable(
                name: "Duct_CanulaDynamic");

            migrationBuilder.DropTable(
                name: "Duct_CanulaPart");

            migrationBuilder.DropTable(
                name: "Duct_Dict");

            migrationBuilder.DropTable(
                name: "Duct_NursingCanula");

            migrationBuilder.DropTable(
                name: "Duct_NursingEvent");

            migrationBuilder.DropTable(
                name: "Duct_ParaItem");

            migrationBuilder.DropTable(
                name: "Duct_ParaModule");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveHeader");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveInput");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveOther");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveOutput");

            migrationBuilder.DropTable(
                name: "NursingLis");

            migrationBuilder.DropTable(
                name: "NursingOwnMedicine");

            migrationBuilder.DropTable(
                name: "NursingPacs");

            migrationBuilder.DropTable(
                name: "NursingPrescribe");

            migrationBuilder.DropTable(
                name: "NursingRecipe");

            migrationBuilder.DropTable(
                name: "NursingRecipeExec");

            migrationBuilder.DropTable(
                name: "NursingRecipeExecHistory");

            migrationBuilder.DropTable(
                name: "NursingRecipeExecRefund");

            migrationBuilder.DropTable(
                name: "NursingRecipeHistory");

            migrationBuilder.DropTable(
                name: "NursingTreat");

            migrationBuilder.DropTable(
                name: "NursingIllnessObserveMain");
        }
    }
}
