using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.BodyParts.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanulaDynamic",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "主键"),
                    CanulaId = table.Column<Guid>(nullable: false, comment: "主键"),
                    GroupName = table.Column<string>(maxLength: 40, nullable: true, comment: "管道分类"),
                    ParaCode = table.Column<string>(maxLength: 40, nullable: true, comment: "参数代码"),
                    ParaName = table.Column<string>(maxLength: 255, nullable: true, comment: "参数名称"),
                    ParaValue = table.Column<string>(maxLength: 4000, nullable: true, comment: "参数值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanulaDynamic", x => x.Id);
                },
                comment: "导管动态列表");

            migrationBuilder.CreateTable(
                name: "Dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParaCode = table.Column<string>(maxLength: 20, nullable: false, comment: "参数代码"),
                    ParaName = table.Column<string>(maxLength: 40, nullable: false, comment: "参数名称"),
                    DictCode = table.Column<string>(maxLength: 20, nullable: false, comment: "字典代码"),
                    DictValue = table.Column<string>(maxLength: 80, nullable: false, comment: "字典值"),
                    DictDesc = table.Column<string>(maxLength: 200, nullable: true),
                    ParentId = table.Column<string>(maxLength: 20, nullable: true, comment: "上级代码"),
                    DictStandard = table.Column<string>(maxLength: 20, nullable: true, comment: "字典标准（国标、自定义）"),
                    HisCode = table.Column<string>(maxLength: 20, nullable: true, comment: "HIS对照代码"),
                    HisName = table.Column<string>(maxLength: 40, nullable: true, comment: "HIS对照"),
                    DeptCode = table.Column<string>(maxLength: 20, nullable: true, comment: "科室代码"),
                    ModuleCode = table.Column<string>(maxLength: 20, nullable: true, comment: "模块代码"),
                    SortNum = table.Column<int>(nullable: false, comment: "排序"),
                    IsDefault = table.Column<bool>(nullable: false, comment: "是否默认"),
                    IsEnable = table.Column<bool>(nullable: false, comment: "是否启用"),
                    ValidState = table.Column<int>(nullable: false, comment: "是否有效（1-是，0-否）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict", x => x.Id);
                },
                comment: "字典-参数字典");

            migrationBuilder.CreateTable(
                name: "DictCanulaPart",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeptCode = table.Column<string>(maxLength: 20, nullable: false, comment: "科室代码"),
                    ModuleCode = table.Column<string>(maxLength: 20, nullable: false, comment: "模块代码"),
                    PartName = table.Column<string>(maxLength: 80, nullable: false, comment: "部位名称"),
                    PartNumber = table.Column<string>(maxLength: 20, nullable: true, comment: "部位编号"),
                    SortNum = table.Column<int>(nullable: false, comment: "排序"),
                    IsEnable = table.Column<bool>(nullable: false, comment: "是否可用"),
                    IsDeleted = table.Column<bool>(nullable: false, comment: "是否删除"),
                    RiskLevel = table.Column<string>(nullable: true, comment: "风险级别 默认空，1低危 2中危 3高危  皮肤分期 默认空  1-1期 2-2期 3-3期 4-4期 5-深部组织损伤 6-不可分期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictCanulaPart", x => x.Id);
                },
                comment: "人体图-编号字典");

            migrationBuilder.CreateTable(
                name: "DictSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParaType = table.Column<string>(maxLength: 10, nullable: false, comment: "参数类型(S-系统参数，D-科室参数)"),
                    DeptCode = table.Column<string>(maxLength: 20, nullable: true, comment: "科室代码"),
                    ModuleCode = table.Column<string>(maxLength: 20, nullable: true),
                    ModuleName = table.Column<string>(maxLength: 50, nullable: true, comment: "模板名称"),
                    Pid = table.Column<Guid>(nullable: true, comment: "父级Id"),
                    ParaCode = table.Column<string>(maxLength: 50, nullable: true, comment: "参数代码"),
                    ParaName = table.Column<string>(maxLength: 50, nullable: false, comment: "参数名称"),
                    ParaValue = table.Column<bool>(nullable: false, comment: "参数值"),
                    IsEnable = table.Column<bool>(nullable: false, comment: "是否启用"),
                    SortNum = table.Column<int>(nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictSource", x => x.Id);
                },
                comment: "字典-基础字典设置表");

            migrationBuilder.CreateTable(
                name: "FileRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(maxLength: 50, nullable: false, comment: "文件名称"),
                    FileSuffix = table.Column<string>(maxLength: 20, nullable: false, comment: "文件后缀（类型）"),
                    BucketName = table.Column<string>(maxLength: 50, nullable: false, comment: "桶名称，如果文件同时在多个桶中，则英文逗号相隔"),
                    UploadTime = table.Column<DateTime>(nullable: false, comment: "上传时间"),
                    DeptCode = table.Column<string>(maxLength: 20, nullable: true, comment: "科室代码"),
                    PI_ID = table.Column<string>(maxLength: 20, nullable: true, comment: "患者ID"),
                    IsDel = table.Column<bool>(nullable: false, comment: "是否删除"),
                    DelTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    Size = table.Column<long>(nullable: false, comment: "文件大小"),
                    ModuleType = table.Column<string>(maxLength: 20, nullable: false, comment: "regulation = ，literatur = 文献")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileRecord", x => x.Id);
                },
                comment: "文件表");

            migrationBuilder.CreateTable(
                name: "IcuCanula",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "主键"),
                    CanulaId = table.Column<Guid>(nullable: false, comment: "导管主表主键"),
                    NurseTime = table.Column<DateTime>(nullable: false, comment: "护理时间"),
                    CanulaWay = table.Column<string>(maxLength: 10, nullable: true, comment: "置入方式"),
                    CanulaLength = table.Column<string>(maxLength: 20, nullable: true, comment: "置管长度"),
                    NurseId = table.Column<string>(maxLength: 10, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(maxLength: 20, nullable: true, comment: "护士名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuCanula", x => x.Id);
                },
                comment: "导管护理记录信息");

            migrationBuilder.CreateTable(
                name: "IcuDeptSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "ID"),
                    DeptCode = table.Column<string>(maxLength: 20, nullable: false, comment: "科室代码"),
                    ScheduleCode = table.Column<string>(maxLength: 20, nullable: false, comment: "班次代码"),
                    ScheduleName = table.Column<string>(maxLength: 20, nullable: false, comment: "班次名称"),
                    StartTime = table.Column<string>(maxLength: 10, nullable: false, comment: "开始时间"),
                    EndTime = table.Column<string>(maxLength: 10, nullable: false, comment: "结束时间"),
                    Period = table.Column<string>(maxLength: 100, nullable: false, comment: "周期"),
                    Days = table.Column<string>(maxLength: 4, nullable: false, comment: "跨天(包含天数)"),
                    Hours = table.Column<string>(maxLength: 10, nullable: false, comment: "小时数"),
                    SortNum = table.Column<int>(nullable: false, comment: "排序"),
                    ScheduleTimeTypeEnum = table.Column<int>(nullable: false, defaultValue: 1, comment: "前闭后开 = 1,前开后闭 = 2"),
                    Type = table.Column<int>(nullable: false, defaultValue: 0, comment: "班次类别，观察项：0，出入量：1 血液净化：2，ECMO：3，PICCO：4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuDeptSchedule", x => x.Id);
                },
                comment: "科室班次");

            migrationBuilder.CreateTable(
                name: "IcuNursingCanula",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "主键"),
                    PI_ID = table.Column<string>(maxLength: 20, nullable: false, comment: "患者id"),
                    StartTime = table.Column<DateTime>(nullable: true, comment: "插管时间"),
                    StopTime = table.Column<DateTime>(nullable: true, comment: "拔管时间"),
                    ModuleCode = table.Column<string>(maxLength: 20, nullable: true, comment: "导管分类"),
                    ModuleName = table.Column<string>(maxLength: 20, nullable: true, comment: "导管名称"),
                    CanulaName = table.Column<string>(maxLength: 40, nullable: true, comment: "管道名称"),
                    CanulaPart = table.Column<string>(maxLength: 40, nullable: true, comment: "插管部位"),
                    CanulaNumber = table.Column<int>(nullable: true, comment: "插管次数"),
                    CanulaPosition = table.Column<string>(maxLength: 40, nullable: true, comment: "插管地点"),
                    DoctorId = table.Column<string>(maxLength: 10, nullable: true, comment: "置管人代码"),
                    DoctorName = table.Column<string>(maxLength: 40, nullable: true, comment: "置管人名称"),
                    CanulaWay = table.Column<string>(maxLength: 10, nullable: true, comment: "置入方式"),
                    CanulaLength = table.Column<string>(maxLength: 20, nullable: true, comment: "置管长度"),
                    DrawReason = table.Column<string>(maxLength: 255, nullable: true, comment: "拔管原因"),
                    TubeDrawState = table.Column<int>(nullable: false, comment: "管道状态（0拔管，1换管，2取消拔管，-1其他）"),
                    UseFlag = table.Column<string>(maxLength: 4, nullable: false, comment: "使用标志：（Y在用，N已拔管）"),
                    NurseId = table.Column<string>(maxLength: 10, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(maxLength: 20, nullable: true, comment: "护士名称"),
                    NurseTime = table.Column<DateTime>(nullable: true, comment: "护理时间"),
                    ValidState = table.Column<int>(nullable: false, comment: "有效状态（1-有效，0-无效）"),
                    RiskLevel = table.Column<string>(nullable: true, comment: "风险级别 默认空，1低危 2中危 3高危")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuNursingCanula", x => x.Id);
                },
                comment: "导管护理信息");

            migrationBuilder.CreateTable(
                name: "IcuNursingEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "主键"),
                    EventType = table.Column<int>(nullable: false, defaultValue: 1, comment: "事件类型"),
                    NurseDate = table.Column<DateTime>(nullable: false, comment: "护理日期"),
                    NurseTime = table.Column<DateTime>(nullable: false, comment: "护理时间"),
                    PI_ID = table.Column<string>(maxLength: 20, nullable: false, comment: "患者id"),
                    Context = table.Column<string>(maxLength: 4000, nullable: false, comment: "内容"),
                    SkinDescription = table.Column<string>(maxLength: 4000, nullable: true, comment: "皮肤情况描述"),
                    Measure = table.Column<string>(maxLength: 4000, nullable: true, comment: "处理措施"),
                    NurseCode = table.Column<string>(maxLength: 20, nullable: true, comment: "护士工号"),
                    NurseName = table.Column<string>(maxLength: 20, nullable: true, comment: "护士名称"),
                    RecordTime = table.Column<DateTime>(nullable: true, comment: "记录时间"),
                    AuditNurseCode = table.Column<string>(maxLength: 20, nullable: true, comment: "审核人"),
                    AuditNurseName = table.Column<string>(maxLength: 20, nullable: true, comment: "审核人名称"),
                    AuditTime = table.Column<DateTime>(nullable: true, comment: "审核时间"),
                    AuditState = table.Column<int>(nullable: true, comment: "审核状态（0-未审核，1，已审核，2-取消审核）"),
                    SignatureId = table.Column<Guid>(nullable: true, comment: "签名记录编号对应icu_signature的id"),
                    AuditSignatureId = table.Column<Guid>(nullable: true, comment: "审核者签名"),
                    SortNum = table.Column<int>(nullable: true, comment: "排序"),
                    ValidState = table.Column<int>(nullable: false, comment: "有效状态（1-有效，0-无效）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuNursingEvent", x => x.Id);
                },
                comment: "护理记录表");

            migrationBuilder.CreateTable(
                name: "IcuNursingSkin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "主键"),
                    NurseTime = table.Column<DateTime>(nullable: false, comment: "发生时间"),
                    PI_ID = table.Column<string>(maxLength: 20, nullable: false, comment: "患者id"),
                    PressPart = table.Column<string>(maxLength: 20, nullable: true, comment: "压疮部位"),
                    ModuleCode = table.Column<string>(maxLength: 20, nullable: true, comment: "压疮分类编码"),
                    PressType = table.Column<string>(maxLength: 20, nullable: true, comment: "压疮类型"),
                    PressSource = table.Column<string>(maxLength: 20, nullable: true, comment: "压疮来源"),
                    PressStage = table.Column<string>(maxLength: 255, nullable: true, comment: "压疮分期"),
                    PressArea = table.Column<string>(maxLength: 20, nullable: true, comment: "压疮面积"),
                    PressColor = table.Column<string>(maxLength: 20, nullable: true, comment: "伤口颜色"),
                    PressSmell = table.Column<string>(maxLength: 20, nullable: true, comment: "伤口气味"),
                    ExudateColor = table.Column<string>(maxLength: 20, nullable: true, comment: "渗出液颜色"),
                    ExudateAmount = table.Column<string>(maxLength: 20, nullable: true, comment: "渗出液量"),
                    NurseId = table.Column<string>(maxLength: 10, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(maxLength: 20, nullable: true, comment: "护士名称"),
                    Finished = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否结束"),
                    FinishTime = table.Column<DateTime>(nullable: true, comment: "结束时间"),
                    ValidState = table.Column<int>(nullable: false, comment: "有效状态（1-有效，0-无效）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuNursingSkin", x => x.Id);
                },
                comment: "皮肤主表");

            migrationBuilder.CreateTable(
                name: "IcuParaItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "ID"),
                    DeptCode = table.Column<string>(maxLength: 20, nullable: true, comment: "科室代码"),
                    ModuleCode = table.Column<string>(maxLength: 20, nullable: true, comment: "模块代码"),
                    ParaCode = table.Column<string>(maxLength: 20, nullable: false, comment: "项目代码"),
                    ParaName = table.Column<string>(maxLength: 80, nullable: false, comment: "项目名称"),
                    DisplayName = table.Column<string>(maxLength: 80, nullable: true, comment: "显示名称"),
                    ScoreCode = table.Column<string>(maxLength: 50, nullable: true, comment: "评分代码"),
                    GroupName = table.Column<string>(maxLength: 40, nullable: true, comment: "导管分类"),
                    UnitName = table.Column<string>(maxLength: 20, nullable: true, comment: "单位名称"),
                    ValueType = table.Column<string>(maxLength: 20, nullable: true, comment: "数据类型"),
                    Style = table.Column<string>(maxLength: 20, nullable: true, comment: "文本类型"),
                    DecimalDigits = table.Column<string>(maxLength: 20, nullable: true, comment: "小数位数"),
                    MaxValue = table.Column<string>(maxLength: 20, nullable: true, comment: "最大值"),
                    MinValue = table.Column<string>(maxLength: 20, nullable: true, comment: "最小值"),
                    HighValue = table.Column<string>(maxLength: 20, nullable: true, comment: "高值"),
                    LowValue = table.Column<string>(maxLength: 20, nullable: true, comment: "低值"),
                    Threshold = table.Column<bool>(nullable: false, comment: "是否预警"),
                    SortNum = table.Column<int>(nullable: false, comment: "排序号"),
                    DataSource = table.Column<string>(maxLength: 10, nullable: true, comment: "默认值"),
                    DictFlag = table.Column<string>(maxLength: 1, nullable: true, comment: "插管部位特殊标记"),
                    GuidFlag = table.Column<bool>(nullable: true, comment: "是否评分"),
                    GuidId = table.Column<string>(maxLength: 50, nullable: true, comment: "评分指引编号"),
                    StatisticalType = table.Column<string>(maxLength: 20, nullable: true, comment: "特护单统计参数分类"),
                    DrawChartFlag = table.Column<int>(nullable: false, comment: "绘制趋势图形"),
                    ColorParaCode = table.Column<string>(maxLength: 20, nullable: true, comment: "关联颜色"),
                    ColorParaName = table.Column<string>(maxLength: 255, nullable: true, comment: "关联颜色名称"),
                    PropertyParaCode = table.Column<string>(maxLength: 20, nullable: true, comment: "关联性状"),
                    PropertyParaName = table.Column<string>(maxLength: 255, nullable: true, comment: "关联性状名称"),
                    DeviceParaCode = table.Column<string>(maxLength: 40, nullable: true, comment: "设备参数代码"),
                    DeviceParaType = table.Column<string>(maxLength: 10, nullable: true, comment: "设备参数类型"),
                    DeviceTimePoint = table.Column<string>(maxLength: 40, nullable: true, comment: "采集时间点"),
                    HealthSign = table.Column<int>(nullable: true, comment: "是否生命体征项目"),
                    KBCode = table.Column<string>(maxLength: 20, nullable: true, comment: "知识库代码"),
                    NuringViewCode = table.Column<string>(maxLength: 20, nullable: true, comment: "护理概览参数"),
                    AbnormalSign = table.Column<string>(maxLength: 1, nullable: true, comment: "是否异常体征参数"),
                    IsUsage = table.Column<bool>(nullable: true, comment: "是否用药所属项目"),
                    AddSource = table.Column<string>(maxLength: 20, nullable: true, comment: "添加来源"),
                    IsEnable = table.Column<bool>(nullable: false, comment: "是否启用"),
                    ValidState = table.Column<int>(nullable: false, comment: "有效状态"),
                    ParaItemType = table.Column<int>(nullable: false, defaultValue: 0, comment: "项目参数类型，用于区分监护仪： 3，呼吸机：2等")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuParaItem", x => x.Id);
                },
                comment: "护理项目表");

            migrationBuilder.CreateTable(
                name: "IcuParaModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "ID"),
                    ModuleCode = table.Column<string>(maxLength: 50, nullable: false, comment: "模块代码"),
                    ModuleName = table.Column<string>(maxLength: 80, nullable: false, comment: "模块名称"),
                    DisplayName = table.Column<string>(maxLength: 80, nullable: true, comment: "模块显示名称"),
                    DeptCode = table.Column<string>(maxLength: 20, nullable: false, comment: "科室代码"),
                    ModuleType = table.Column<string>(maxLength: 20, nullable: true, comment: "模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）"),
                    IsBloodflow = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否血流内导管"),
                    Enname = table.Column<string>(maxLength: 40, nullable: true, comment: "拼音"),
                    SortNum = table.Column<int>(maxLength: 10, nullable: false, comment: "排序"),
                    IsEnable = table.Column<bool>(nullable: false, comment: "是否启用"),
                    ValidState = table.Column<int>(nullable: false, comment: "是否有效(1-有效，0-无效)"),
                    RiskLevel = table.Column<string>(nullable: true, comment: "风险级别 默认空，1低危 2中危 3高危"),
                    PartNumber = table.Column<string>(nullable: true, comment: "部位编号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuParaModule", x => x.Id);
                },
                comment: "模块参数");

            migrationBuilder.CreateTable(
                name: "IcuPatientRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "ID"),
                    PI_ID = table.Column<string>(maxLength: 20, nullable: false, comment: "患者id(通过业务构造的流水号，每个患者每次入科号码唯一)"),
                    ArchiveId = table.Column<string>(maxLength: 20, nullable: true, comment: "档案号"),
                    VisitNum = table.Column<string>(maxLength: 20, nullable: true, comment: "就诊号"),
                    InHosId = table.Column<string>(maxLength: 20, nullable: true, comment: "住院号"),
                    VisitTimes = table.Column<int>(nullable: true, comment: "住院次数"),
                    Age = table.Column<string>(maxLength: 10, nullable: true, comment: "年龄"),
                    BedNum = table.Column<string>(maxLength: 20, nullable: true, comment: "采集器(绑定床位)的编号 "),
                    WardCode = table.Column<string>(maxLength: 20, nullable: true, comment: "病区代码"),
                    DeptCode = table.Column<string>(maxLength: 20, nullable: true, comment: "科室代码"),
                    DeptName = table.Column<string>(maxLength: 100, nullable: true, comment: "科室名称"),
                    DoctorCode = table.Column<string>(maxLength: 20, nullable: true, comment: "主管医生"),
                    SpDoctorCode = table.Column<string>(maxLength: 20, nullable: true, comment: "专科医生"),
                    SpDoctorName = table.Column<string>(maxLength: 20, nullable: true, comment: "专科医生名称"),
                    DoctorName = table.Column<string>(maxLength: 20, nullable: true, comment: "主管医生名称"),
                    NurseCode = table.Column<string>(maxLength: 20, nullable: true, comment: "责任护士"),
                    NurseName = table.Column<string>(maxLength: 40, nullable: true, comment: "责任护士名称"),
                    NurseGrade = table.Column<int>(nullable: true, comment: "护理级别（1：一级护理，2：二级护理，3：三级护理，4.特级护理）"),
                    NurseType = table.Column<int>(nullable: false, defaultValue: 0, comment: "护理类型（0：其他，1：基础护理，2：特殊护理，3：辩证施护）"),
                    Height = table.Column<string>(maxLength: 20, nullable: true, comment: "身高"),
                    Weight = table.Column<string>(maxLength: 20, nullable: true, comment: "体重"),
                    InDeptState = table.Column<int>(nullable: false, comment: "是否在科（1：在科；0：出科；2，取消入科，3:待入科，4:待出科)"),
                    InDeptTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "入科时间"),
                    InDeptNurseCode = table.Column<string>(maxLength: 20, nullable: true, comment: "入科交接护士代码"),
                    InDeptNurseName = table.Column<string>(maxLength: 20, nullable: true, comment: "入科交接护士名称"),
                    InDeptTransferTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "入科交接时间"),
                    Indiagnosis = table.Column<string>(maxLength: 1000, nullable: true, comment: "入科诊断"),
                    ClinicDiagnosis = table.Column<string>(maxLength: 1000, nullable: true, comment: "临床诊断"),
                    InSource = table.Column<int>(nullable: true, comment: "入科来源（7-入院、5-手术、6-外院转入、8-转入）"),
                    InDeptCode = table.Column<string>(maxLength: 20, nullable: true, comment: "来源科室代码"),
                    InDeptName = table.Column<string>(maxLength: 100, nullable: true, comment: "来源科室名称"),
                    InPlan = table.Column<int>(nullable: true, comment: "入科计划（0：非计划转入，1：计划转入）"),
                    InReason = table.Column<int>(nullable: true, comment: "转入原因（非计划转入原因：1：缺少病情变化的预警，2：手术因素，3：麻醉因素；计划转入原因：4：危及生命的急性器官功能不全，5：具有潜在危及生命的高危因素，6：慢性器官功能不全急性加重，7：围手术期危重患者，0：其他）"),
                    InStandard = table.Column<string>(maxLength: 1000, nullable: true, comment: "转入标准"),
                    OutStandard = table.Column<string>(maxLength: 1000, nullable: true, comment: "转出标准"),
                    IsDoctorConfirm = table.Column<bool>(nullable: false, comment: "是否医生确认"),
                    InReturn = table.Column<int>(nullable: true, comment: "重返（0：否，1：24小时重返，2：48小时重返）"),
                    CriticaDegree = table.Column<int>(nullable: true, comment: "危重程度（0：其他，1：病危，2：病重）"),
                    OutDeptTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "出科时间"),
                    OutTurnover = table.Column<int>(nullable: true, comment: "出科转归(1：出院，2：转出，3：死亡，4：转上级医院)"),
                    OutState = table.Column<int>(nullable: true, comment: "出科状态(1：恶化，2：好转，3：未愈)"),
                    OutDeptCode = table.Column<string>(maxLength: 20, nullable: true, comment: "转出科室"),
                    OutDeptName = table.Column<string>(maxLength: 100, nullable: true, comment: "转出科室名称"),
                    Outdiagnosis = table.Column<string>(maxLength: 1000, nullable: true, comment: "出科诊断"),
                    OutDeptNurseCode = table.Column<string>(maxLength: 20, nullable: true, comment: "出科交接护士工号"),
                    OutDeptNurseName = table.Column<string>(maxLength: 40, nullable: true, comment: "出科交接护士名称"),
                    OutDeptTransferTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "出科交接时间"),
                    Allergy = table.Column<string>(maxLength: 255, nullable: true, comment: "过敏史"),
                    PreHistory = table.Column<string>(maxLength: 255, nullable: true, comment: "既往史"),
                    PayType = table.Column<string>(maxLength: 40, nullable: true, comment: "付费方式"),
                    PrePayMent = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, comment: "预交金额"),
                    TotalCost = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, comment: "已消费"),
                    UnsettledCost = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, comment: "结余"),
                    InsulateFlag = table.Column<string>(maxLength: 20, nullable: true, comment: "隔离标志（Y ：隔离，N：不隔离）"),
                    OperationState = table.Column<string>(maxLength: 4, nullable: true, comment: "病人操作状态(Y：锁住，N：解锁)"),
                    OperationNurseId = table.Column<string>(maxLength: 20, nullable: true, comment: "最后操作人ID"),
                    OperationNurseName = table.Column<string>(maxLength: 20, nullable: true, comment: "最后操作人名称"),
                    OperationTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "最后操作时间"),
                    PatState = table.Column<int>(nullable: true, comment: "病人状态 1:正常入科(入院/入科转icu实床) 2:紧急入科 3:虚床转实床 4:病人紧急入科，通过消息更新后")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuPatientRecord", x => x.Id);
                },
                comment: "患者记录表");

            migrationBuilder.CreateTable(
                name: "IcuPhrase",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TypeCode = table.Column<string>(maxLength: 10, nullable: true, comment: "类型代码"),
                    TypeName = table.Column<string>(maxLength: 50, nullable: true, comment: "类型名称"),
                    ParentId = table.Column<string>(maxLength: 10, nullable: true, comment: "上级编号"),
                    DeptCode = table.Column<string>(maxLength: 10, nullable: true, comment: "科室代码"),
                    StaffCode = table.Column<string>(maxLength: 20, nullable: true, comment: "员工代码"),
                    PhraseText = table.Column<string>(maxLength: 4000, nullable: true, comment: "模板内容"),
                    SortNum = table.Column<int>(nullable: false, comment: "排序"),
                    ValidState = table.Column<int>(nullable: false, comment: "是否有效(1-有效，0-无效)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuPhrase", x => x.Id);
                },
                comment: "常用语模板");

            migrationBuilder.CreateTable(
                name: "IcuSignature",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "ID"),
                    PI_ID = table.Column<string>(maxLength: 20, nullable: true, comment: "患者id"),
                    NurseTime = table.Column<DateTime>(nullable: false, comment: "护理时间"),
                    SignNurseCode = table.Column<string>(maxLength: 20, nullable: true, comment: "签名工号"),
                    SignNurseName = table.Column<string>(maxLength: 20, nullable: true, comment: "签名名称"),
                    SignTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "签名时间"),
                    SignImage = table.Column<string>(nullable: true, comment: "签名图片"),
                    SignState = table.Column<int>(nullable: true, comment: "签名标志"),
                    SignNurseCode2 = table.Column<string>(maxLength: 20, nullable: true, comment: "二级签名工号"),
                    SignNurseName2 = table.Column<string>(maxLength: 40, nullable: true, comment: "二级签名名称"),
                    SignTime2 = table.Column<DateTime>(type: "datetime", nullable: true, comment: "二级签名时间"),
                    SignImage2 = table.Column<string>(nullable: true, comment: "二级签名图片"),
                    SignState2 = table.Column<int>(nullable: true, comment: "二级签名标志"),
                    SignNurseCode3 = table.Column<string>(maxLength: 20, nullable: true, comment: "三级签名工号"),
                    SignNurseName3 = table.Column<string>(maxLength: 20, nullable: true, comment: "三级签名名称"),
                    SignTime3 = table.Column<DateTime>(type: "datetime", nullable: true, comment: "三级签名时间"),
                    SignImage3 = table.Column<string>(nullable: true, comment: "三级签名图片"),
                    SignState3 = table.Column<int>(nullable: true, comment: "三级签名标志"),
                    ReviewState = table.Column<string>(maxLength: 20, nullable: true, comment: "特护单审核状态（S:已审核）"),
                    IsDeleted = table.Column<bool>(nullable: false, comment: "是否有效（1-是，0-否）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuSignature", x => x.Id);
                },
                comment: "签名");

            migrationBuilder.CreateTable(
                name: "IcuSkin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "主键"),
                    SkinId = table.Column<Guid>(nullable: true, comment: "压疮Id"),
                    NurseTime = table.Column<DateTime>(nullable: false, comment: "护理时间"),
                    PressStage = table.Column<string>(maxLength: 255, nullable: true, comment: "压疮分期"),
                    PressArea = table.Column<string>(maxLength: 20, nullable: true, comment: "压疮面积"),
                    PressColor = table.Column<string>(maxLength: 20, nullable: true, comment: "伤口颜色"),
                    PressSmell = table.Column<string>(maxLength: 20, nullable: true, comment: "伤口气味"),
                    ExudateColor = table.Column<string>(maxLength: 20, nullable: true, comment: "渗出液颜色"),
                    ExudateAmount = table.Column<string>(maxLength: 20, nullable: true, comment: "渗出液量"),
                    NursingMeasure = table.Column<string>(maxLength: 255, nullable: true, comment: "护理措施"),
                    NurseId = table.Column<string>(maxLength: 10, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(maxLength: 20, nullable: true, comment: "护士名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuSkin", x => x.Id);
                },
                comment: "皮肤详细信息记录表");

            migrationBuilder.CreateTable(
                name: "IcuSysPara",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "ID"),
                    Type = table.Column<int>(maxLength: 10, nullable: false, comment: "类型（系统参数 = 1,特护单参数 = 2）"),
                    ParaType = table.Column<string>(maxLength: 10, nullable: false, comment: "参数类型(S-系统参数，D-科室参数)"),
                    DeptCode = table.Column<string>(maxLength: 20, nullable: true, comment: "科室代码"),
                    ModuleName = table.Column<string>(maxLength: 1000, nullable: true, comment: "模板名称"),
                    ParaCode = table.Column<string>(maxLength: 50, nullable: false, comment: "参数代码"),
                    ParaName = table.Column<string>(maxLength: 1000, nullable: false, comment: "参数名称"),
                    ParaValue = table.Column<string>(maxLength: 8000, nullable: true, comment: "参数值"),
                    ValueType = table.Column<int>(nullable: false, defaultValue: 1, comment: "值类型"),
                    DataSource = table.Column<string>(maxLength: 100, nullable: true, comment: "数据源"),
                    ModuleSort = table.Column<string>(nullable: true, defaultValue: "1", comment: "模块排序号"),
                    SortNum = table.Column<int>(nullable: false, defaultValue: 1, comment: "排序号"),
                    IsAddiable = table.Column<bool>(nullable: false, defaultValue: false, comment: "表格模式下是否可添加"),
                    Desc = table.Column<string>(nullable: true, comment: "描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuSysPara", x => x.Id);
                },
                comment: "系统-参数设置表");

            migrationBuilder.CreateTable(
                name: "SkinDynamic",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "主键"),
                    CanulaId = table.Column<Guid>(nullable: false, comment: "主键"),
                    GroupName = table.Column<string>(maxLength: 40, nullable: true, comment: "皮肤分类"),
                    ParaCode = table.Column<string>(maxLength: 40, nullable: true, comment: "参数代码"),
                    ParaName = table.Column<string>(maxLength: 255, nullable: true, comment: "参数名称"),
                    ParaValue = table.Column<string>(maxLength: 255, nullable: true, comment: "参数值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinDynamic", x => x.Id);
                },
                comment: "人体图动态表");

            migrationBuilder.CreateIndex(
                name: "Index_DeptCode",
                table: "Dict",
                column: "DeptCode");

            migrationBuilder.CreateIndex(
                name: "Index_ModuleCode",
                table: "Dict",
                column: "ModuleCode");

            migrationBuilder.CreateIndex(
                name: "Index_ParaCode",
                table: "Dict",
                column: "ParaCode");

            migrationBuilder.CreateIndex(
                name: "Index_ValidState",
                table: "Dict",
                column: "ValidState");

            migrationBuilder.CreateIndex(
                name: "Index_CanulaId",
                table: "IcuCanula",
                column: "CanulaId");

            migrationBuilder.CreateIndex(
                name: "Index_NurseTime",
                table: "IcuCanula",
                column: "NurseTime");

            migrationBuilder.CreateIndex(
                name: "Index_PI_ID",
                table: "IcuNursingCanula",
                column: "PI_ID");

            migrationBuilder.CreateIndex(
                name: "Index_ValidState",
                table: "IcuNursingCanula",
                column: "ValidState");

            migrationBuilder.CreateIndex(
                name: "Index_NurseTime",
                table: "IcuNursingEvent",
                column: "NurseTime");

            migrationBuilder.CreateIndex(
                name: "Index_PI_ID",
                table: "IcuNursingEvent",
                column: "PI_ID");

            migrationBuilder.CreateIndex(
                name: "Index_ValidState",
                table: "IcuNursingEvent",
                column: "ValidState");

            migrationBuilder.CreateIndex(
                name: "Index_DeptCode",
                table: "IcuParaItem",
                column: "DeptCode");

            migrationBuilder.CreateIndex(
                name: "Index_ModuleCode",
                table: "IcuParaItem",
                column: "ModuleCode");

            migrationBuilder.CreateIndex(
                name: "Index_ParaCode",
                table: "IcuParaItem",
                column: "ParaCode");

            migrationBuilder.CreateIndex(
                name: "Index_ValidState",
                table: "IcuParaItem",
                column: "ValidState");

            migrationBuilder.CreateIndex(
                name: "Index_DeptCode",
                table: "IcuParaModule",
                column: "DeptCode");

            migrationBuilder.CreateIndex(
                name: "Index_ModuleCode",
                table: "IcuParaModule",
                column: "ModuleCode");

            migrationBuilder.CreateIndex(
                name: "Index_ValidState",
                table: "IcuParaModule",
                column: "ValidState");

            migrationBuilder.CreateIndex(
                name: "Index_ArchiveId",
                table: "IcuPatientRecord",
                column: "ArchiveId");

            migrationBuilder.CreateIndex(
                name: "Index_InDeptState",
                table: "IcuPatientRecord",
                column: "InDeptState");

            migrationBuilder.CreateIndex(
                name: "Index_InDeptTime",
                table: "IcuPatientRecord",
                column: "InDeptTime");

            migrationBuilder.CreateIndex(
                name: "Index_InHosId",
                table: "IcuPatientRecord",
                column: "InHosId");

            migrationBuilder.CreateIndex(
                name: "Index_PI_ID",
                table: "IcuPatientRecord",
                column: "PI_ID");

            migrationBuilder.CreateIndex(
                name: "Index_VisitNum",
                table: "IcuPatientRecord",
                column: "VisitNum");

            migrationBuilder.CreateIndex(
                name: "Unique_ParaCode",
                table: "IcuSysPara",
                columns: new[] { "DeptCode", "ParaCode" },
                unique: true,
                filter: "[DeptCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanulaDynamic");

            migrationBuilder.DropTable(
                name: "Dict");

            migrationBuilder.DropTable(
                name: "DictCanulaPart");

            migrationBuilder.DropTable(
                name: "DictSource");

            migrationBuilder.DropTable(
                name: "FileRecord");

            migrationBuilder.DropTable(
                name: "IcuCanula");

            migrationBuilder.DropTable(
                name: "IcuDeptSchedule");

            migrationBuilder.DropTable(
                name: "IcuNursingCanula");

            migrationBuilder.DropTable(
                name: "IcuNursingEvent");

            migrationBuilder.DropTable(
                name: "IcuNursingSkin");

            migrationBuilder.DropTable(
                name: "IcuParaItem");

            migrationBuilder.DropTable(
                name: "IcuParaModule");

            migrationBuilder.DropTable(
                name: "IcuPatientRecord");

            migrationBuilder.DropTable(
                name: "IcuPhrase");

            migrationBuilder.DropTable(
                name: "IcuSignature");

            migrationBuilder.DropTable(
                name: "IcuSkin");

            migrationBuilder.DropTable(
                name: "IcuSysPara");

            migrationBuilder.DropTable(
                name: "SkinDynamic");
        }
    }
}
