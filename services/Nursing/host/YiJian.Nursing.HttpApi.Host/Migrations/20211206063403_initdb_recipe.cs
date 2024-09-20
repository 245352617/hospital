using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class initdb_recipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NursingRecipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    RecipeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    Diagnosis = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "临床诊断（冗余设计）"),
                    PrescribeTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型编码"),
                    PrescribeType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型：字典、临嘱、长嘱、出院带药"),
                    RecipeCategoryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱项目分类编码 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)"),
                    RecipeCategory = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)"),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "医嘱说明"),
                    IsBackTracking = table.Column<bool>(type: "bit", nullable: false, comment: "是否补录"),
                    PrescriptionNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "处方号"),
                    RecipeNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱号"),
                    RecipeSubNo = table.Column<int>(type: "int", nullable: false, comment: "医嘱子号：药物为1.2.3...其它项目取默认值"),
                    ApplyTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "开立时间"),
                    ApplyDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "申请医生编码"),
                    ApplyDoctor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "申请医生"),
                    ApplyDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "申请科室编码"),
                    ApplyDept = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "申请科室"),
                    TraineeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "管培生代码"),
                    Trainee = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "管培生"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "执行科室编码"),
                    ExecDept = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "执行科室"),
                    StatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "状态编码"),
                    StatusDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "状态描述"),
                    ExecStatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "执行状态码"),
                    ExecStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "执行状态"),
                    StopDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "停嘱医生代码"),
                    StopDoctor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "停嘱医生"),
                    StopDateTime = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: true, comment: "停嘱时间"),
                    PayTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "付费类型编码"),
                    PayType = table.Column<int>(type: "int", nullable: false, comment: "付费类型  0 自费项目 1 医保项目"),
                    PayTypeDescrition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "付费类型描述：自费项目 医保项目"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "单价"),
                    TotalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "总费用"),
                    RecipeFeeStatusCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "收费状态码"),
                    RecipeFeeStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "收费状态"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    Insurance = table.Column<int>(type: "int", nullable: false, comment: "医保目录：0-自费项目1-医保项目(甲、乙)"),
                    InsuranceDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医保目录描述"),
                    IsReceiptPrinted = table.Column<bool>(type: "bit", nullable: false, comment: "申请单打印标识"),
                    RePrintReceiptCount = table.Column<int>(type: "int", nullable: false, comment: "补打次数 默认0次"),
                    RePrintReceiptTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "补打时间"),
                    RePrintReceiptor = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "补打印人"),
                    IsRecipePrinted = table.Column<bool>(type: "bit", nullable: false, comment: "医嘱单打印标识"),
                    SignCert = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "签名证书"),
                    SignValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "签名值"),
                    TimestampValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "时间戳值"),
                    SignFlow = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: " Base64 编码格式的签章图片"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "病人标识"),
                    IsChronicDisease = table.Column<bool>(type: "bit", nullable: true, comment: "慢性病标识"),
                    BussinessCatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "业务类型编码"),
                    BussinessCatalog = table.Column<int>(type: "int", nullable: false, comment: "业务类型"),
                    BussinessCatalogDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "业务类型描述"),
                    HisOrderNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "HIS医嘱号"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NursingLis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicalSymptom = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "临床症状"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "标本编码"),
                    Specimen = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "标本"),
                    SpecimenPartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本采集部位编码"),
                    SpecimenPart = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本采集部位"),
                    SpecimenContainerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器代码"),
                    SpecimenContainer = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器"),
                    SpecimenContainerColor = table.Column<int>(type: "int", maxLength: 20, nullable: true, comment: "标本容器颜色 0 红帽 1 蓝帽 2 紫帽"),
                    SpecimenDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "标本说明"),
                    SpecimenCollectDT = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "标本采集时间"),
                    SpecimenReceivedDT = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "标本接收时间"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检验类别编码"),
                    Catalog = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "检验类别"),
                    ReportDT = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "出报告时间"),
                    ReportDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认报告医生编码"),
                    ReportDoctor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认报告医生"),
                    HasReport = table.Column<bool>(type: "bit", nullable: false, comment: "报告标识"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false, comment: "是否紧急"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingLis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingLis_NursingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "NursingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检验项");

            migrationBuilder.CreateTable(
                name: "NursingPacs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicalSymptom = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "临床症状"),
                    MedicalHistory = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "简要病史"),
                    ExamPartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "检查部位编码"),
                    ExamPart = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检查类别编码"),
                    Catalog = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "检查类别"),
                    CatalogDisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "分类类型名称 例如心电图申请单、超声申请单"),
                    ReportDT = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: true, comment: "出报告时间"),
                    ReportDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认报告医生编码"),
                    ReportDoctor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认报告医生"),
                    HasReport = table.Column<bool>(type: "bit", nullable: false, comment: "报告标识"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false, comment: "是否紧急"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingPacs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingPacs_NursingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "NursingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检查项");

            migrationBuilder.CreateTable(
                name: "NursingPrescribes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Property = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "药物属性：西药、中药、西药制剂、中药制剂"),
                    ToxicPropertyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "药理等级编码：毒、麻、精一、精二"),
                    ToxicProperty = table.Column<int>(type: "int", nullable: false, comment: "药理等级：毒、麻、精一、精二"),
                    ToxicPropertyDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药理等级描述：毒、麻、精一、精二"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法编码"),
                    Usage = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法"),
                    Speed = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "滴速"),
                    LongDays = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "开药天数"),
                    ActualDays = table.Column<int>(type: "int", maxLength: 20, nullable: true, comment: "实际天数"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "每次计量"),
                    QtyPerTime = table.Column<decimal>(type: "decimal(18,4)", maxLength: 20, precision: 18, scale: 4, nullable: true, comment: "每次用量"),
                    Amount = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "领量"),
                    Unit = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "领量单位"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次码"),
                    Frequency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次"),
                    FrequencyRatio = table.Column<int>(type: "int", nullable: true, comment: "频次执行系数"),
                    IsAmendedMark = table.Column<bool>(type: "bit", nullable: false, comment: "是否抢救后补：0-非抢救后补1-抢救后补"),
                    IsOutDrug = table.Column<bool>(type: "bit", nullable: false, comment: "是否自备药：0-非自备药1-自备药"),
                    PharmacyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "药房编码"),
                    Pharmacy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药房"),
                    ExecTime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "执行时间：例如13:00."),
                    Factory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "厂家"),
                    FactoryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "厂家代码"),
                    BatchNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "批次号"),
                    ExpirDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "失效期"),
                    Specification = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "包装规格"),
                    BigPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "包装单位"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂量单位"),
                    IsSkinTest = table.Column<bool>(type: "bit", maxLength: 20, nullable: false, comment: "是否皮试 0 不需要皮试 1 需要皮试"),
                    SkinTestResult = table.Column<bool>(type: "bit", maxLength: 20, nullable: true, comment: "皮试结果 0-阴性 1-阳性"),
                    MaterialPrice = table.Column<decimal>(type: "decimal(18,4)", maxLength: 20, precision: 18, scale: 4, nullable: true, comment: "耗材金额"),
                    IsBindingTreat = table.Column<bool>(type: "bit", nullable: true, comment: "用于判断关联耗材是否手动删除"),
                    IsAdaptationDisease = table.Column<bool>(type: "bit", nullable: true, comment: "是否医保适应症"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingPrescribes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingPrescribes_NursingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "NursingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "处方业务");

            migrationBuilder.CreateTable(
                name: "NursingTreats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "用量单位"),
                    Amount = table.Column<int>(type: "int", nullable: false, comment: "用量"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingTreats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingTreats_NursingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "NursingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "诊疗项");

            migrationBuilder.CreateTable(
                name: "NursingLisTargets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "项目标识-外键"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "小项标识"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "小项编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "小项名称"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "单价"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "数量"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位"),
                    TotalFee = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "总费用"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    Insurance = table.Column<int>(type: "int", nullable: false, comment: "医保目录：0-自费项目1-医保项目(甲、乙)"),
                    InsuranceDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医保目录描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingLisTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingLisTargets_NursingLis_LisId",
                        column: x => x.LisId,
                        principalTable: "NursingLis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检验小项");

            migrationBuilder.CreateTable(
                name: "NursingPacsTargets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "项目标识-外键"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "小项标识"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "小项编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "小项名称"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "单价"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "数量"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位"),
                    TotalFee = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "总费用"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    Insurance = table.Column<int>(type: "int", nullable: false, comment: "医保目录：0-自费项目1-医保项目(甲、乙)"),
                    InsuranceDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医保目录描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingPacsTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingPacsTargets_NursingPacs_PacsId",
                        column: x => x.PacsId,
                        principalTable: "NursingPacs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检查小项");

            migrationBuilder.CreateIndex(
                name: "IX_NursingLis_RecipeId",
                table: "NursingLis",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingLisTargets_LisId",
                table: "NursingLisTargets",
                column: "LisId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingPacs_RecipeId",
                table: "NursingPacs",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingPacsTargets_PacsId",
                table: "NursingPacsTargets",
                column: "PacsId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingPrescribes_RecipeId",
                table: "NursingPrescribes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipe_RecipeCode",
                table: "NursingRecipe",
                column: "RecipeCode");

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipe_RecipeName",
                table: "NursingRecipe",
                column: "RecipeName");

            migrationBuilder.CreateIndex(
                name: "IX_NursingTreats_RecipeId",
                table: "NursingTreats",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingLisTargets");

            migrationBuilder.DropTable(
                name: "NursingPacsTargets");

            migrationBuilder.DropTable(
                name: "NursingPrescribes");

            migrationBuilder.DropTable(
                name: "NursingTreats");

            migrationBuilder.DropTable(
                name: "NursingLis");

            migrationBuilder.DropTable(
                name: "NursingPacs");

            migrationBuilder.DropTable(
                name: "NursingRecipe");
        }
    }
}
