using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations
{
    public partial class v210base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oper_OperationApply",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "分诊患者id"),
                    PatientId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者唯一标识(HIS)"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "患者姓名"),
                    Sex = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "患者性别"),
                    SexName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "患者性别"),
                    Age = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "年龄"),
                    IDNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "身份证号"),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "生日"),
                    ApplyNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "申请单号"),
                    ApplicantId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "申请人Id"),
                    ApplicantName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请人名称"),
                    ApplyTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "申请时间"),
                    BloodType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "血型"),
                    RHCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "RH阴性阳性"),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "身高"),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "体重"),
                    ProposedOperationCode = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "拟施手术编码"),
                    ProposedOperationName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "拟施手术名称"),
                    OperationLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "手术等级"),
                    ApplyDeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室编码"),
                    ApplyDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室名称"),
                    OperationLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "手术位置"),
                    DoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "手术医生编码"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "手术医生名称"),
                    OperationAssistant = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "手术助手"),
                    OperationPlanTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "手术计划时间"),
                    OperationDuration = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "手术时长"),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "备注"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回"),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "手术申请提交日期"),
                    OperationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "手术类型"),
                    DiagnoseCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "术前诊断编码"),
                    DiagnoseName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "术前诊断名称"),
                    Anesthesiologist = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "麻醉医生"),
                    AnesthesiologistAssistant = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "麻醉助手"),
                    TourNurse = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "巡回护士"),
                    InstrumentNurse = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "器械护士"),
                    AnaestheticCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "麻醉方式编码"),
                    AnaestheticName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "麻醉方式名称"),
                    Sequence = table.Column<int>(type: "int", nullable: false, comment: "手术台次"),
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
                    table.PrimaryKey("PK_Oper_OperationApply", x => x.Id);
                },
                comment: "手术申请");

            migrationBuilder.CreateTable(
                name: "RC_DoctorsAdvice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetailId = table.Column<int>(type: "int", nullable: false, comment: "明细ID，给医院使用，唯一（备用候选键）"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "系统标识: 0=急诊，1=院前"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者唯一标识"),
                    PatientId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者Id"),
                    PatientName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "患者名称"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "医嘱名称"),
                    CategoryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱项目分类编码"),
                    CategoryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)"),
                    IsBackTracking = table.Column<bool>(type: "bit", nullable: false, comment: "是否补录"),
                    PrescriptionNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "处方号"),
                    RecipeNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱号"),
                    RecipeGroupNo = table.Column<int>(type: "int", nullable: false, comment: "医嘱子号"),
                    ApplyTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "开嘱时间"),
                    ApplyDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "申请医生编码"),
                    ApplyDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "申请医生"),
                    ApplyDeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室编码"),
                    ApplyDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室"),
                    TraineeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "管培生代码"),
                    TraineeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "管培生名称"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行科室编码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行科室名称"),
                    ExecutorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行者编码"),
                    ExecutorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行者名称"),
                    ExecTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "执行时间"),
                    StopDoctorCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "停嘱医生编码"),
                    StopDoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "停嘱医生名称"),
                    StopDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "停嘱时间"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费"),
                    PayTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "付费类型编码"),
                    PayType = table.Column<int>(type: "int", nullable: false, comment: "付费类型: 0=自费,1=医保,2=其它"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "单价"),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "总费用"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    InsuranceType = table.Column<int>(type: "int", nullable: false, comment: "医保目录:0=自费,1=甲类,2=乙类,3=其它"),
                    IsChronicDisease = table.Column<bool>(type: "bit", nullable: true, comment: "是否慢性病"),
                    IsRecipePrinted = table.Column<bool>(type: "bit", nullable: false, comment: "是否打印过"),
                    HisOrderNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "HIS医嘱号"),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "临床诊断"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    PositionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    ItemType = table.Column<int>(type: "int", nullable: false, comment: "医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项"),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "医嘱说明"),
                    ChargeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "收费类型编码"),
                    ChargeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费类型名称"),
                    PayStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "支付状态 , 0=待支付,1=已支付,2=部分支付,3=已退费"),
                    PrescribeTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型编码"),
                    PrescribeTypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型：临嘱、长嘱、出院带药等"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "开始时间"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "结束时间"),
                    RecieveQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m, comment: "领量(数量)"),
                    RecieveUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "领量单位"),
                    ScientificName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "学名"),
                    Alias = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "别名"),
                    AliasPyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名拼音"),
                    AliasWbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名五笔码"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
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
                    table.PrimaryKey("PK_RC_DoctorsAdvice", x => x.Id);
                },
                comment: "医嘱主表");

            migrationBuilder.CreateTable(
                name: "RC_DrugStockQuery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Storage = table.Column<int>(type: "int", nullable: false),
                    DrugCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药品编号, 医院药品唯一编码"),
                    DrugName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药品名称"),
                    DrugSpec = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药品规格"),
                    MinPackageUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "包装单位,大/小包装单位 小包装单位=片"),
                    FirmID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "厂商id"),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", maxLength: 50, nullable: false, comment: "进货价格 元"),
                    RetailPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "零售价格 元"),
                    PharSpec = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药房规格, 门急诊药房规格"),
                    PharmUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药品单位, 门急诊药房单位"),
                    PackageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "包装数量, 大/小包装数量 小包装数量 =（例如一盒有36片）=36"),
                    MinPackageIndicator = table.Column<int>(type: "int", nullable: false, comment: "药 包装类型, 1 表示小包装，0表示大包装"),
                    Dosage = table.Column<int>(type: "int", nullable: false, comment: "最小单位数量 "),
                    DosageUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "最小单位"),
                    DrugDose = table.Column<decimal>(type: "decimal(18,2)", maxLength: 50, nullable: false, comment: "药品剂量"),
                    DrugUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "剂量单位"),
                    ReturnDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "备注信息"),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "库存数量"),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "药品效期"),
                    DrugBatchNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药品批号"),
                    DoctorsAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_DrugStockQuery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RC_GroupConsultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "分诊患者id"),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "患者id"),
                    TypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "会诊类型编码"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "类型名称"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "会诊开始时间"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "会诊状态"),
                    ObjectiveCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "会诊目的编码"),
                    ObjectiveContent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "会诊目的内容"),
                    ApplyDeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室编码"),
                    ApplyDeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "申请科室名称"),
                    ApplyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请人编码"),
                    ApplyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "申请人名称"),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "联系方式"),
                    ApplyTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "申请时间"),
                    Place = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "地点"),
                    VitalSigns = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "生命体征"),
                    Test = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "检验"),
                    Inspect = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "检查"),
                    DoctorOrder = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "医嘱"),
                    Diagnose = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "诊断"),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "病历摘要"),
                    Summary = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "总结"),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "结束时间"),
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
                    table.PrimaryKey("PK_RC_GroupConsultations", x => x.Id);
                },
                comment: "会诊管理");

            migrationBuilder.CreateTable(
                name: "RC_Lis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检验类别编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "检验类别"),
                    ClinicalSymptom = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "临床症状"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "标本编码"),
                    SpecimenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "标本名称"),
                    SpecimenPartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本采集部位编码"),
                    SpecimenPartName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本采集部位"),
                    ContainerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器代码"),
                    ContainerName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器"),
                    ContainerColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器颜色:0=红帽,1=蓝帽,2=紫帽"),
                    SpecimenDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "标本说明"),
                    SpecimenCollectDatetime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "标本采集时间"),
                    SpecimenReceivedDatetime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "标本接收时间"),
                    ReportTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "出报告时间"),
                    ReportDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认报告医生编码"),
                    ReportDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认报告医生"),
                    HasReportName = table.Column<bool>(type: "bit", nullable: false, comment: "报告标识"),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false, comment: "是否紧急"),
                    IsBedSide = table.Column<bool>(type: "bit", nullable: false, comment: "是否在床旁"),
                    AddCard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "附加卡片类型: 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期);14.新型冠状病毒RNA检测申请单;13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单"),
                    DoctorsAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱Id"),
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
                    table.PrimaryKey("PK_RC_Lis", x => x.Id);
                },
                comment: "检验项");

            migrationBuilder.CreateTable(
                name: "RC_MedDetailResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HisNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lgzxyy_payurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lgjkzx_payurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_MedDetailResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RC_MySequence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "受控表名"),
                    FiledName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "受控的字段"),
                    Increment = table.Column<int>(type: "int", nullable: false, comment: "增长计数器")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_MySequence", x => x.Id);
                },
                comment: "我的系列号管理器");

            migrationBuilder.CreateTable(
                name: "RC_NovelCoronavirusRna",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者分诊id"),
                    DoctorsAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱id"),
                    SpecimenType = table.Column<int>(type: "int", nullable: false, comment: "标本类型"),
                    ConsultationOpinions = table.Column<int>(type: "int", nullable: false, comment: "专家会诊意见"),
                    EpidemicHistory = table.Column<int>(type: "int", nullable: false, comment: "流行病学史"),
                    IsFever = table.Column<bool>(type: "bit", nullable: false, comment: "是否发热"),
                    IsPneumonia = table.Column<bool>(type: "bit", nullable: false, comment: "是否有肺炎影像学特征"),
                    IsLymphopenia = table.Column<bool>(type: "bit", nullable: false, comment: "淋巴细胞是否降低"),
                    PatientSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "人员来源"),
                    PatientIdentity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "人员身份"),
                    PlaceToShenzhen = table.Column<int>(type: "int", nullable: false, comment: "来深地点"),
                    LisesName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "检验项目名称"),
                    ApplyTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "申请时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_NovelCoronavirusRna", x => x.Id);
                },
                comment: "新冠RNA检测申请");

            migrationBuilder.CreateTable(
                name: "RC_Package",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "名称"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "序号"),
                    InputCode = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "输入码"),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "分组 ID"),
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
                    table.PrimaryKey("PK_RC_Package", x => x.Id);
                },
                comment: "医嘱套餐");

            migrationBuilder.CreateTable(
                name: "RC_PackageGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "类型"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "分组名称"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "序号"),
                    UserOrDepartmentId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true, comment: "用户/科室 ID"),
                    ParentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "父级分组 ID"),
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
                    table.PrimaryKey("PK_RC_PackageGroup", x => x.Id);
                },
                comment: "医嘱套餐分组");

            migrationBuilder.CreateTable(
                name: "RC_Pacs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检查类别编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "检查类别"),
                    ClinicalSymptom = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "临床症状"),
                    MedicalHistory = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "病史简要"),
                    PartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "检查部位编码"),
                    PartName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位"),
                    CatalogDisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "分类类型名称 例如心电图申请单、超声申请单"),
                    ReportTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "出报告时间"),
                    ReportDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认报告医生编码"),
                    ReportDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认报告医生"),
                    HasReport = table.Column<bool>(type: "bit", nullable: false, comment: "报告标识"),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false, comment: "是否紧急"),
                    IsBedSide = table.Column<bool>(type: "bit", nullable: false, comment: "是否在床旁"),
                    AddCard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "附加卡片类型: 12.TCT细胞学检查申请单 ,11.病理检验申请单,16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用"),
                    DoctorsAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱Id"),
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
                    table.PrimaryKey("PK_RC_Pacs", x => x.Id);
                },
                comment: "检查项");

            migrationBuilder.CreateTable(
                name: "RC_Prescribe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOutDrug = table.Column<bool>(type: "bit", nullable: false, comment: "是否自备药：false=非自备药,true=自备药"),
                    MedicineProperty = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "药物属性：西药、中药、西药制剂、中药制剂"),
                    ToxicProperty = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "药理等级"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法名称"),
                    Speed = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "滴速"),
                    LongDays = table.Column<int>(type: "int", nullable: false, comment: "开药天数"),
                    ActualDays = table.Column<int>(type: "int", nullable: true, comment: "实际天数"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "每次剂量"),
                    DefaultDosageQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "默认规格剂量"),
                    QtyPerTimes = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "每次用量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂量单位"),
                    DefaultDosageUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unpack = table.Column<int>(type: "int", nullable: false, comment: "门诊拆分属性 0=最小单位总量取整 1=包装单位总量取整 2=最小单位每次取整 3=包装单位每次取整 4=最小单位可拆分"),
                    BigPackPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "包装价格"),
                    BigPackFactor = table.Column<int>(type: "int", nullable: false, comment: "大包装系数(拆零系数)"),
                    BigPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "包装单位"),
                    SmallPackPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "小包装单价"),
                    SmallPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "小包装单位"),
                    SmallPackFactor = table.Column<int>(type: "int", nullable: false, comment: "小包装系数(拆零系数)"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次"),
                    FrequencyTimes = table.Column<int>(type: "int", nullable: true, comment: "在一个周期内执行的次数"),
                    FrequencyUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时"),
                    FrequencyExecDayTimes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "一天内的执行时间"),
                    DailyFrequency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "HIS频次编码"),
                    PharmacyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "药房编码"),
                    PharmacyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药房名称"),
                    FactoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "厂家名称"),
                    FactoryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "厂家代码"),
                    BatchNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "批次号"),
                    ExpirDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "失效期"),
                    IsSkinTest = table.Column<bool>(type: "bit", nullable: true, comment: "是否皮试 false=不需要皮试 true=需要皮试"),
                    SkinTestResult = table.Column<bool>(type: "bit", nullable: true, comment: "皮试结果:false=阴性 ture=阳性"),
                    SkinTestSignChoseResult = table.Column<int>(type: "int", nullable: true, comment: "皮试选择结果,默认空什么都没选择，0=否，1=是，2=续用"),
                    MaterialPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true, comment: "耗材金额"),
                    IsBindingTreat = table.Column<bool>(type: "bit", nullable: true, comment: "用于判断关联耗材是否手动删除"),
                    IsAmendedMark = table.Column<bool>(type: "bit", nullable: true, comment: "是否抢救后补：false=非抢救后补，true=抢救后补"),
                    IsAdaptationDisease = table.Column<bool>(type: "bit", nullable: true, comment: "是否医保适应症"),
                    IsFirstAid = table.Column<bool>(type: "bit", nullable: true, comment: "是否是急救药"),
                    AntibioticPermission = table.Column<int>(type: "int", nullable: false, comment: "抗生素权限"),
                    PrescriptionPermission = table.Column<int>(type: "int", nullable: false, comment: "处方权"),
                    DoctorsAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱Id"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "包装规格"),
                    LimitType = table.Column<int>(type: "int", nullable: false, comment: "限制用药标记"),
                    RestrictedDrugs = table.Column<int>(type: "int", nullable: true, comment: "收费类型"),
                    ChildrenPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true, comment: "儿童价格"),
                    FixPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true, comment: "批发价格"),
                    RetPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true, comment: "零售价格"),
                    MedicineId = table.Column<int>(type: "int", nullable: false, comment: "药品id"),
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
                    table.PrimaryKey("PK_RC_Prescribe", x => x.Id);
                },
                comment: "药品");

            migrationBuilder.CreateTable(
                name: "RC_Prescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorsAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MyPrescriptionNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "我方映射处方号"),
                    PrescriptionNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "处方号"),
                    VisSerialNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "就诊流水号"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "就诊患者姓名"),
                    DeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "就诊科室"),
                    DoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "就诊医生编号"),
                    BillState = table.Column<int>(type: "int", nullable: false, comment: "订单状态, 0.未缴费 1.已缴费 -1.已作废 2.已执行"),
                    Retry = table.Column<bool>(type: "bit", nullable: false),
                    RetryTimes = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_Prescription", x => x.Id);
                },
                comment: "处方号管理类");

            migrationBuilder.CreateTable(
                name: "RC_PrintInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPrintAgain = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PrintCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "印人的编码"),
                    PrintName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "印人的名称"),
                    PrintTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "补打时间"),
                    PrescriptionNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_RC_PrintInfo", x => x.Id);
                },
                comment: "医嘱打印信息");

            migrationBuilder.CreateTable(
                name: "RC_Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "类别编码"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "类别名称"),
                    SourceId = table.Column<int>(type: "int", nullable: false, comment: "源ID"),
                    Code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    ScientificName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "学名"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "规格"),
                    Sort = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "排序号"),
                    CategoryPyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "类别拼音码"),
                    CategoryWbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "类别五笔码"),
                    PyCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "五笔码"),
                    Alias = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "别名"),
                    AliasPyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名拼音码"),
                    AliasWbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名五笔码"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行科室编码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "执行科室"),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "单位"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "单位价格"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "其他价格"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "备注/说明"),
                    CanUseInFirstAid = table.Column<bool>(type: "bit", nullable: false, comment: "是否可用于院前急救急救"),
                    ChargeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费分类代码"),
                    ChargeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费分类名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_Project", x => x.Id);
                },
                comment: "医嘱项目");

            migrationBuilder.CreateTable(
                name: "RC_QuickStartCatalogue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "分类名称"),
                    DoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医生编号"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医生名称"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "平台标识"),
                    CanModify = table.Column<bool>(type: "bit", nullable: false, comment: "平台标识"),
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
                    table.PrimaryKey("PK_RC_QuickStartCatalogue", x => x.Id);
                },
                comment: "快速开嘱目录");

            migrationBuilder.CreateTable(
                name: "RC_SettingPara",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "模式"),
                    Value = table.Column<int>(type: "int", nullable: false, comment: "数值"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "备注")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_SettingPara", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RC_Toxic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSkinTest = table.Column<bool>(type: "bit", nullable: true, comment: "皮试药"),
                    IsCompound = table.Column<bool>(type: "bit", nullable: true, comment: "复方药"),
                    IsDrunk = table.Column<bool>(type: "bit", nullable: true, comment: "麻醉药"),
                    ToxicLevel = table.Column<int>(type: "int", nullable: true, comment: "精神药  0非精神药,1一类精神药,2二类精神药"),
                    IsHighRisk = table.Column<bool>(type: "bit", nullable: true, comment: "高危药"),
                    IsRefrigerated = table.Column<bool>(type: "bit", nullable: true, comment: "冷藏药"),
                    IsTumour = table.Column<bool>(type: "bit", nullable: true, comment: "肿瘤药"),
                    AntibioticLevel = table.Column<int>(type: "int", nullable: true, comment: "抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级"),
                    IsPrecious = table.Column<bool>(type: "bit", nullable: true, comment: "贵重药"),
                    IsInsulin = table.Column<bool>(type: "bit", nullable: true, comment: "胰岛素"),
                    IsAnaleptic = table.Column<bool>(type: "bit", nullable: true, comment: "兴奋剂"),
                    IsAllergyTest = table.Column<bool>(type: "bit", nullable: true, comment: "试敏药"),
                    IsLimited = table.Column<bool>(type: "bit", nullable: true, comment: "限制性用药标识"),
                    LimitedNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "限制性用药描述"),
                    MedicineId = table.Column<int>(type: "int", nullable: false, comment: "药品id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_Toxic", x => x.Id);
                },
                comment: "药理");

            migrationBuilder.CreateTable(
                name: "RC_Treat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true, comment: "其它价格"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "默认频次代码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "频次"),
                    FeeTypeMainCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费大类代码"),
                    FeeTypeSubCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费小类代码"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "包装规格"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "用法名称"),
                    LongDays = table.Column<int>(type: "int", nullable: false, comment: "开药天数"),
                    ProjectType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "项目类型"),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "项目类型名称"),
                    ProjectMerge = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "项目归类"),
                    TreatId = table.Column<int>(type: "int", nullable: false, comment: "诊疗项Id"),
                    DoctorsAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱Id"),
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
                    table.PrimaryKey("PK_RC_Treat", x => x.Id);
                },
                comment: "诊疗项");

            migrationBuilder.CreateTable(
                name: "RC_DoctorsAdviceAudit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    OperationCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "操作人编码（驳回操作人，复核操作人，执行操作人）"),
                    OperationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "操作人名称"),
                    OperationTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "操作时间（驳回时间，复核时间，执行时间）"),
                    OriginType = table.Column<int>(type: "int", nullable: false, comment: "操作来源，0=医生操作，1=护士操作"),
                    DoctorsAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_DoctorsAdviceAudit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_DoctorsAdviceAudit_RC_DoctorsAdvice_DoctorsAdviceId",
                        column: x => x.DoctorsAdviceId,
                        principalTable: "RC_DoctorsAdvice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "医嘱审计");

            migrationBuilder.CreateTable(
                name: "RC_DoctorSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupConsultationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "会诊id"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生编码"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "医生名称"),
                    DoctorTitle = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "医生职称"),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "联系电话"),
                    DeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "科室编码"),
                    DeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "科室名称"),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "报道时间"),
                    Opinion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "意见")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_DoctorSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_DoctorSummary_RC_GroupConsultations_GroupConsultationId",
                        column: x => x.GroupConsultationId,
                        principalTable: "RC_GroupConsultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RC_InviteDoctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupConsultationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "会诊id"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生编码"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "医生名称"),
                    DoctorTitle = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "医生职称"),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "联系电话"),
                    DeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "科室编码"),
                    DeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "科室名称"),
                    CheckInStatus = table.Column<int>(type: "int", nullable: false, comment: "状态，0：已邀请，1：已报到"),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "报道时间"),
                    Opinion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "意见")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_InviteDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_InviteDoctors_RC_GroupConsultations_GroupConsultationId",
                        column: x => x.GroupConsultationId,
                        principalTable: "RC_GroupConsultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "会诊邀请医生");

            migrationBuilder.CreateTable(
                name: "RC_LisItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "小项编码"),
                    TargetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "小项名称"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "单价"),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "数量"),
                    TargetUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    InsuranceType = table.Column<int>(type: "int", nullable: false, comment: "医保目录：0-自费项目1-医保项目(甲、乙)"),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "项目编码"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "其它价格"),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "规格"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
                    SpecialFlag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "特殊标识"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ProjectType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "项目类型"),
                    ProjectMerge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "项目归类"),
                    LisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "检验项Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_LisItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_LisItem_RC_Lis_LisId",
                        column: x => x.LisId,
                        principalTable: "RC_Lis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检验小项");

            migrationBuilder.CreateTable(
                name: "RC_PacsItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "小项编码"),
                    TargetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "小项名称"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "单价"),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "数量"),
                    TargetUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    InsuranceType = table.Column<int>(type: "int", nullable: false, comment: "医保目录:0=自费,1=甲类,2=乙类,3=其它"),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "项目编码"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "其它价格"),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "规格"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
                    SpecialFlag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "特殊标识"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ProjectType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "项目类型"),
                    ProjectMerge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "项目归类"),
                    PacsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "检查项Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_PacsItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_PacsItem_RC_Pacs_PacsId",
                        column: x => x.PacsId,
                        principalTable: "RC_Pacs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检查小项");

            migrationBuilder.CreateTable(
                name: "RC_PackageProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱套餐 ID"),
                    EntryId = table.Column<int>(type: "int", nullable: false, comment: "分录 ID"),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "项目 ID"),
                    RecipeNo = table.Column<int>(type: "int", nullable: false, comment: "项目号"),
                    RecipeGroupNo = table.Column<int>(type: "int", nullable: false, comment: "子号"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true, comment: "剂量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "剂量单位"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "用途/途径编码"),
                    UsageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "用途/途径名称"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "频次编码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "频次名称"),
                    FrequencyTimes = table.Column<int>(type: "int", nullable: false, comment: "频次系数"),
                    FrequencyUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "频次单位"),
                    DailyFrequency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "HIS频次编码"),
                    FrequencyExecDayTimes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行时间"),
                    Qty = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "数量"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位"),
                    LongDays = table.Column<int>(type: "int", nullable: false, comment: "天数"),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "备注/说明"),
                    PrescribeTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型编码"),
                    PrescribeTypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型：临嘱、长嘱、出院带药等"),
                    RecieveQty = table.Column<int>(type: "int", nullable: false, comment: "领量(数量)"),
                    RecieveUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "领量单位"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSkinTest = table.Column<bool>(type: "bit", nullable: true, comment: "是否皮试 false=不需要皮试 true=需要皮试"),
                    ChargeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_PackageProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Package_PackageProject",
                        column: x => x.PackageId,
                        principalTable: "RC_Package",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RC_PackageProject_RC_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "RC_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RC_ProjectExamProp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "目录编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "分类名称"),
                    PartName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位"),
                    PartCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位编码"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    PositionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    RoomCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行机房编码"),
                    RoomName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行机房描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_ProjectExamProp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_ExamProp",
                        column: x => x.Id,
                        principalTable: "RC_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "医嘱-检查属性");

            migrationBuilder.CreateTable(
                name: "RC_ProjectLabProp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检验目录编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "目录分类名称"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "标本编码"),
                    SpecimenName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "标本"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    PositionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    ContainerCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "容器编码"),
                    ContainerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "容器名称"),
                    PartCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位编码"),
                    PartName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_ProjectLabProp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_LabProp",
                        column: x => x.Id,
                        principalTable: "RC_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "医嘱-检验属性");

            migrationBuilder.CreateTable(
                name: "RC_ProjectMedicineProp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineProperty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "药物属性：西药、中药、西药制剂、中药制剂"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "基本单位"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "基本单位价格"),
                    BigPackPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "包装价格"),
                    BigPackFactor = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "大包装量"),
                    BigPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "包装单位"),
                    SmallPackPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "小包装单价"),
                    SmallPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "小包装单位"),
                    SmallPackFactor = table.Column<int>(type: "int", nullable: false, comment: "小包装量"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "剂量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "剂量单位"),
                    PharmacyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "药房代码"),
                    PharmacyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "药房"),
                    IsSkinTest = table.Column<bool>(type: "bit", nullable: false, comment: "皮试药"),
                    IsCompound = table.Column<bool>(type: "bit", nullable: false, comment: "复方药"),
                    IsDrunk = table.Column<bool>(type: "bit", nullable: false, comment: "麻醉药"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "用途/途径编码"),
                    UsageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "用途/途径名称"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "频次编码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "频次名称"),
                    FactoryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "厂家代码"),
                    FactoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "厂家名称"),
                    BatchNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "批次"),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "失效日期"),
                    ToxicProperty = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    InsuranceType = table.Column<int>(type: "int", nullable: false, comment: "医保类型：0自费,1甲类,2乙类，3丙类"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "医保类型代码"),
                    ChargeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费分类代码"),
                    ChargeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费分类名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_ProjectMedicineProp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Medicine",
                        column: x => x.Id,
                        principalTable: "RC_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "医嘱-药品属性");

            migrationBuilder.CreateTable(
                name: "RC_ProjectTreatrop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrequencyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认频次代码"),
                    FeeTypeMainCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费大类代码"),
                    FeeTypeSubCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费小类代码"),
                    ProjectMerge = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "项目归类--龙岗字典所需")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_ProjectTreatrop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_TreatProp",
                        column: x => x.Id,
                        principalTable: "RC_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "医嘱-处置属性");

            migrationBuilder.CreateTable(
                name: "RC_QuickStartAdvice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsageCount = table.Column<int>(type: "int", nullable: false, comment: " 统计使用过的次数（个人统计）"),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    QuickStartCatalogueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_RC_QuickStartAdvice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_QuickStartAdvice_RC_QuickStartCatalogue_QuickStartCatalogueId",
                        column: x => x.QuickStartCatalogueId,
                        principalTable: "RC_QuickStartCatalogue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "快速开嘱信息");

            migrationBuilder.CreateTable(
                name: "RC_QuickStartMedicine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false, comment: "药品id"),
                    CategoryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱项目分类编码"),
                    CategoryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)"),
                    IsBackTracking = table.Column<bool>(type: "bit", nullable: false, comment: "是否补录"),
                    PrescriptionNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "处方号"),
                    ApplyTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "开嘱时间"),
                    ApplyDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "申请医生编码"),
                    ApplyDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请医生"),
                    ApplyDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "申请科室编码"),
                    ApplyDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室"),
                    TraineeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "管培生代码"),
                    TraineeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "管培生名称"),
                    ExecutorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行者编码"),
                    ExecutorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行者名称"),
                    StopDoctorCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "停嘱医生编码"),
                    StopDoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "停嘱医生名称"),
                    StopDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "停嘱时间"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费"),
                    PayTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "付费类型编码"),
                    PayType = table.Column<int>(type: "int", nullable: false, comment: "付费类型: 0=自费,1=医保,2=其它"),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "医嘱说明"),
                    ChargeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "收费类型编码"),
                    ChargeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费类型名称"),
                    MedicineCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "药品编码"),
                    MedicineName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "药品名称"),
                    ScientificName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "学名"),
                    Alias = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "别名"),
                    AliasPyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名拼音"),
                    AliasWbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名五笔码"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "五笔"),
                    MedicineProperty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "药物属性：西药、中药、西药制剂、中药制剂"),
                    DefaultDosage = table.Column<double>(type: "float", maxLength: 20, nullable: false, comment: "默认剂量"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "剂量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂量单位"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "基本单位"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "基本单位价格"),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "数量"),
                    BigPackPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "大包装价格"),
                    BigPackFactor = table.Column<int>(type: "int", nullable: false, comment: "大包装量(大包装系数)"),
                    BigPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "大包装单位"),
                    SmallPackPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "小包装单价"),
                    SmallPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "小包装单位"),
                    SmallPackFactor = table.Column<int>(type: "int", nullable: false, comment: "小包装量(小包装系数)"),
                    ChildrenPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "儿童价格"),
                    FixPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "批发价格"),
                    RetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "零售价格"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    InsuranceType = table.Column<int>(type: "int", nullable: false, comment: "医保目录:0=自费,1=甲类,2=乙类,3=其它"),
                    InsurancePayRate = table.Column<int>(type: "int", nullable: true, comment: "医保支付比例"),
                    FactoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "厂家名称"),
                    FactoryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "厂家代码"),
                    BatchNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "批次号"),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: true, comment: "失效期"),
                    Weight = table.Column<double>(type: "float", nullable: true, comment: "重量"),
                    WeightUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "重量单位"),
                    Volume = table.Column<double>(type: "float", nullable: true, comment: "体积"),
                    VolumeUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "体积单位"),
                    Remark = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "备注"),
                    IsSkinTest = table.Column<bool>(type: "bit", nullable: true, comment: "皮试药"),
                    IsCompound = table.Column<bool>(type: "bit", nullable: true, comment: "复方药"),
                    IsDrunk = table.Column<bool>(type: "bit", nullable: true, comment: "麻醉药"),
                    ToxicLevel = table.Column<int>(type: "int", nullable: true, comment: "精神药  0非精神药,1一类精神药,2二类精神药"),
                    IsHighRisk = table.Column<bool>(type: "bit", nullable: true, comment: "高危药"),
                    IsRefrigerated = table.Column<bool>(type: "bit", nullable: true, comment: "冷藏药"),
                    IsTumour = table.Column<bool>(type: "bit", nullable: true, comment: "肿瘤药"),
                    AntibioticLevel = table.Column<int>(type: "int", nullable: true, comment: "抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级"),
                    IsPrecious = table.Column<bool>(type: "bit", nullable: true, comment: "贵重药"),
                    IsInsulin = table.Column<bool>(type: "bit", nullable: true, comment: "胰岛素"),
                    IsAnaleptic = table.Column<bool>(type: "bit", nullable: true, comment: "兴奋剂"),
                    IsAllergyTest = table.Column<bool>(type: "bit", nullable: true, comment: "试敏药"),
                    IsLimited = table.Column<bool>(type: "bit", nullable: true, comment: "限制性用药标识"),
                    LimitedNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "限制性用药描述"),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "规格"),
                    DosageForm = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂型"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行科室编码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行科室名称"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "默认用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认用法名称"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "默认频次编码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认频次名称"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    IsFirstAid = table.Column<bool>(type: "bit", nullable: true, comment: "急救药"),
                    PharmacyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "药房代码"),
                    PharmacyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "药房"),
                    AntibioticPermission = table.Column<int>(type: "int", nullable: false, comment: "抗生素权限"),
                    PrescriptionPermission = table.Column<int>(type: "int", nullable: false, comment: "处方权"),
                    Stock = table.Column<int>(type: "int", nullable: false, comment: "库存"),
                    BaseFlag = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "基药标准  N普通,Y国基,P省基,C市基"),
                    Unpack = table.Column<int>(type: "int", nullable: false, comment: "门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分"),
                    RecieveQty = table.Column<int>(type: "int", nullable: false, comment: "领量(数量)"),
                    RecieveUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "领量单位"),
                    ToxicProperty = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "药理等级"),
                    LongDays = table.Column<int>(type: "int", nullable: false, comment: "开药天数"),
                    ActualDays = table.Column<int>(type: "int", nullable: true, comment: "实际天数"),
                    DefaultDosageQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "默认规格剂量"),
                    QtyPerTimes = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "每次用量"),
                    DefaultDosageUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrequencyTimes = table.Column<int>(type: "int", nullable: true, comment: "在一个周期内执行的次数"),
                    FrequencyUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时"),
                    FrequencyExecDayTimes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "一天内的执行时间"),
                    DailyFrequency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "HIS频次编码"),
                    SkinTestResult = table.Column<bool>(type: "bit", nullable: true, comment: "皮试结果:false=阴性 ture=阳性"),
                    SkinTestSignChoseResult = table.Column<int>(type: "int", nullable: true, comment: "皮试选择结果,默认空什么都没选择，0=否，1=是，2=续用"),
                    MaterialPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "耗材金额"),
                    IsBindingTreat = table.Column<bool>(type: "bit", nullable: true, comment: "用于判断关联耗材是否手动删除"),
                    IsAmendedMark = table.Column<bool>(type: "bit", nullable: true, comment: "是否抢救后补：false=非抢救后补，true=抢救后补"),
                    IsAdaptationDisease = table.Column<bool>(type: "bit", nullable: true, comment: "是否医保适应症"),
                    Speed = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "滴速"),
                    RestrictedDrugs = table.Column<int>(type: "int", nullable: true, comment: "收费类型"),
                    QuickStartAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_RC_QuickStartMedicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_QuickStartMedicine_RC_QuickStartAdvice_QuickStartAdviceId",
                        column: x => x.QuickStartAdviceId,
                        principalTable: "RC_QuickStartAdvice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "快速开嘱的药品");

            migrationBuilder.CreateIndex(
                name: "IX_Oper_OperationApply_PI_Id",
                table: "Oper_OperationApply",
                column: "PI_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RC_DoctorsAdvice_Code",
                table: "RC_DoctorsAdvice",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_RC_DoctorsAdvice_Name",
                table: "RC_DoctorsAdvice",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RC_DoctorsAdvice_PatientId",
                table: "RC_DoctorsAdvice",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_DoctorsAdvice_PIID",
                table: "RC_DoctorsAdvice",
                column: "PIID");

            migrationBuilder.CreateIndex(
                name: "IX_RC_DoctorsAdviceAudit_DoctorsAdviceId",
                table: "RC_DoctorsAdviceAudit",
                column: "DoctorsAdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_DoctorSummary_GroupConsultationId",
                table: "RC_DoctorSummary",
                column: "GroupConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_GroupConsultations_PI_ID",
                table: "RC_GroupConsultations",
                column: "PI_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RC_InviteDoctors_GroupConsultationId",
                table: "RC_InviteDoctors",
                column: "GroupConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_LisItem_LisId",
                table: "RC_LisItem",
                column: "LisId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageProject_PackageId",
                table: "RC_PackageProject",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_PackageProject_ProjectId",
                table: "RC_PackageProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_PacsItem_PacsId",
                table: "RC_PacsItem",
                column: "PacsId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_Prescribe_MedicineId",
                table: "RC_Prescribe",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_Prescription_MyPrescriptionNo",
                table: "RC_Prescription",
                column: "MyPrescriptionNo");

            migrationBuilder.CreateIndex(
                name: "IDX_Project_Code",
                table: "RC_Project",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_RC_QuickStartAdvice_QuickStartCatalogueId",
                table: "RC_QuickStartAdvice",
                column: "QuickStartCatalogueId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_QuickStartCatalogue_DoctorCode",
                table: "RC_QuickStartCatalogue",
                column: "DoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_RC_QuickStartMedicine_QuickStartAdviceId",
                table: "RC_QuickStartMedicine",
                column: "QuickStartAdviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RC_Toxic_MedicineId",
                table: "RC_Toxic",
                column: "MedicineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oper_OperationApply");

            migrationBuilder.DropTable(
                name: "RC_DoctorsAdviceAudit");

            migrationBuilder.DropTable(
                name: "RC_DoctorSummary");

            migrationBuilder.DropTable(
                name: "RC_DrugStockQuery");

            migrationBuilder.DropTable(
                name: "RC_InviteDoctors");

            migrationBuilder.DropTable(
                name: "RC_LisItem");

            migrationBuilder.DropTable(
                name: "RC_MedDetailResult");

            migrationBuilder.DropTable(
                name: "RC_MySequence");

            migrationBuilder.DropTable(
                name: "RC_NovelCoronavirusRna");

            migrationBuilder.DropTable(
                name: "RC_PackageGroup");

            migrationBuilder.DropTable(
                name: "RC_PackageProject");

            migrationBuilder.DropTable(
                name: "RC_PacsItem");

            migrationBuilder.DropTable(
                name: "RC_Prescribe");

            migrationBuilder.DropTable(
                name: "RC_Prescription");

            migrationBuilder.DropTable(
                name: "RC_PrintInfo");

            migrationBuilder.DropTable(
                name: "RC_ProjectExamProp");

            migrationBuilder.DropTable(
                name: "RC_ProjectLabProp");

            migrationBuilder.DropTable(
                name: "RC_ProjectMedicineProp");

            migrationBuilder.DropTable(
                name: "RC_ProjectTreatrop");

            migrationBuilder.DropTable(
                name: "RC_QuickStartMedicine");

            migrationBuilder.DropTable(
                name: "RC_SettingPara");

            migrationBuilder.DropTable(
                name: "RC_Toxic");

            migrationBuilder.DropTable(
                name: "RC_Treat");

            migrationBuilder.DropTable(
                name: "RC_DoctorsAdvice");

            migrationBuilder.DropTable(
                name: "RC_GroupConsultations");

            migrationBuilder.DropTable(
                name: "RC_Lis");

            migrationBuilder.DropTable(
                name: "RC_Package");

            migrationBuilder.DropTable(
                name: "RC_Pacs");

            migrationBuilder.DropTable(
                name: "RC_Project");

            migrationBuilder.DropTable(
                name: "RC_QuickStartAdvice");

            migrationBuilder.DropTable(
                name: "RC_QuickStartCatalogue");
        }
    }
}
