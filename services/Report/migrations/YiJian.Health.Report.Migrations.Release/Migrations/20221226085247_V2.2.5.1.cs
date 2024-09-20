using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations.Migrations
{
    public partial class V2251 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RpDynamicData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Header = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "表头[Key]"),
                    Text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "文本数据"),
                    SheetIndex = table.Column<int>(type: "int", nullable: false, comment: "新建页索引"),
                    NursingRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理单记录列Id"),
                    NursingDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理单Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpDynamicData", x => x.Id);
                },
                comment: "动态数据字典集合");

            migrationBuilder.CreateTable(
                name: "RpHospitalInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "医院的名称"),
                    Logo = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "医院徽标"),
                    HospitalLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医院评级(级别，如：三级甲等)"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "医院的地址"),
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
                    table.PrimaryKey("PK_RpHospitalInfo", x => x.Id);
                },
                comment: "医院的基础信息");

            migrationBuilder.CreateTable(
                name: "RpNursingDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "单据标题"),
                    CardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NursingCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "护理单编码"),
                    PatientId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "患者Id"),
                    Patient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "患者名称"),
                    IDCard = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true, comment: "身份证"),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "性别"),
                    Age = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "入科当时的年龄"),
                    DayOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "出生日期"),
                    BedNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "床号"),
                    AdmissionTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "入院时间"),
                    DeptCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "科室编号"),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    Diagnose = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "诊断"),
                    EmergencyWay = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "入抢救室的方式"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "PI_ID"),
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
                    table.PrimaryKey("PK_RpNursingDocument", x => x.Id);
                },
                comment: "护理单");

            migrationBuilder.CreateTable(
                name: "RpNursingSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "表头分类"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序顺序"),
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
                    table.PrimaryKey("PK_RpNursingSetting", x => x.Id);
                },
                comment: "护理单配置");

            migrationBuilder.CreateTable(
                name: "RpPageSize",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "编码"),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "高"),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "宽")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpPageSize", x => x.Id);
                },
                comment: "纸张大小");

            migrationBuilder.CreateTable(
                name: "RpPrintCatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CataLogName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "目录名称"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "类型，0:打印中心，1：其他地方打印")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpPrintCatalog", x => x.Id);
                },
                comment: "打印目录");

            migrationBuilder.CreateTable(
                name: "RpPrintSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CataLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "目录Id"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "名称"),
                    ParamUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "传参Url"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    Status = table.Column<bool>(type: "bit", nullable: false, comment: "状态"),
                    TempType = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "模板类型"),
                    PrintMethod = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "打印方式"),
                    TempContent = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "模板内容"),
                    CreationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "创建人名称"),
                    PageSizeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "纸张编码"),
                    Layout = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "布局"),
                    IsPreview = table.Column<bool>(type: "bit", nullable: false, comment: "是否预览"),
                    ReportTypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "单据类型编码"),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "备注"),
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
                    table.PrimaryKey("PK_RpPrintSetting", x => x.Id);
                },
                comment: "打印设置");

            migrationBuilder.CreateTable(
                name: "RpReportData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者分诊id"),
                    TempId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "模板id"),
                    PrescriptionNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "处方号"),
                    DataContent = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "数据"),
                    OperationCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "操作人编码"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpReportData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RpCriticalIllness",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, comment: " 危重枚举(0 = 病危, 1=病重)"),
                    Remark = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "危重描述"),
                    Begintime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "危重开始记录时间"),
                    Endtime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "患者Id"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "患者名称"),
                    NursingDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理记录Id"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "PI_ID"),
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
                    table.PrimaryKey("PK_RpCriticalIllness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpCriticalIllness_RpNursingDocument_NursingDocumentId",
                        column: x => x.NursingDocumentId,
                        principalTable: "RpNursingDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "危重情况记录");

            migrationBuilder.CreateTable(
                name: "RpDynamicField",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Field1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "保留字段1"),
                    Field2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "保留字段2"),
                    Field3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "保留字段3"),
                    Field4 = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "保留字段4"),
                    Field5 = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "保留字段5"),
                    Field6 = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "保留字段6"),
                    Field7 = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "保留字段7"),
                    Field8 = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "保留字段8"),
                    Field9 = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "保留字段9"),
                    SheetIndex = table.Column<int>(type: "int", nullable: false, comment: "护理单记录索引"),
                    SheetName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "护理单记录名称"),
                    NursingDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理单Id(外键)"),
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
                    table.PrimaryKey("PK_RpDynamicField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpDynamicField_RpNursingDocument_NursingDocumentId",
                        column: x => x.NursingDocumentId,
                        principalTable: "RpNursingDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "动态字段名字描述");

            migrationBuilder.CreateTable(
                name: "RpNursingRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理记录时间"),
                    T = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "体温（单位℃）"),
                    P = table.Column<int>(type: "int", nullable: true, comment: "脉搏P(次/min)"),
                    HR = table.Column<int>(type: "int", nullable: true, comment: "心率(次/min)"),
                    R = table.Column<int>(type: "int", nullable: true, comment: "呼吸(次/min)"),
                    BP = table.Column<int>(type: "int", nullable: true, comment: "血压BP收缩压(mmHg)"),
                    BP2 = table.Column<int>(type: "int", nullable: true, comment: "血压BP舒张压(mmHg)"),
                    SPO2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "血氧饱和度SPO2 %"),
                    Consciousness = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "意识"),
                    Field1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "保留字段1"),
                    Field2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "保留字段2"),
                    Field3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "保留字段3"),
                    Field4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "保留字段4"),
                    Field5 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "保留字段5"),
                    Field6 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "保留字段6"),
                    Field7 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "保留字段7"),
                    Field8 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "保留字段8"),
                    Field9 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "保留字段9"),
                    Remark = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "特殊情况记录"),
                    NurseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "操作护士"),
                    Nurse = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "护士签名"),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "签名图片"),
                    SheetIndex = table.Column<int>(type: "int", nullable: false, comment: "新建页索引"),
                    NursingDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理单Id"),
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
                    table.PrimaryKey("PK_RpNursingRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpNursingRecord_RpNursingDocument_NursingDocumentId",
                        column: x => x.NursingDocumentId,
                        principalTable: "RpNursingDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "护理记录");

            migrationBuilder.CreateTable(
                name: "RpNursingSettingHeader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "表头名称"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序编号[序号]"),
                    NursingSettingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理单配置Id"),
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
                    table.PrimaryKey("PK_RpNursingSettingHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpNursingSettingHeader_RpNursingSetting_NursingSettingId",
                        column: x => x.NursingSettingId,
                        principalTable: "RpNursingSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "护理单表头配置");

            migrationBuilder.CreateTable(
                name: "RpCharacteristic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JsonData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NursingRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpCharacteristic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpCharacteristic_RpNursingRecord_NursingRecordId",
                        column: x => x.NursingRecordId,
                        principalTable: "RpNursingRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RpIntake",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IntakeType = table.Column<int>(type: "int", nullable: false, comment: "入量出量类型（0=入量，1=出量）"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "内容"),
                    Quantity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "量"),
                    UnitCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "单位编码"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "单位"),
                    TraitsCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "性状"),
                    Traits = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "性状"),
                    NursingRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpIntake", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpIntake_RpNursingRecord_NursingRecordId",
                        column: x => x.NursingRecordId,
                        principalTable: "RpNursingRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "入量出量");

            migrationBuilder.CreateTable(
                name: "RpMmol",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealTimeType = table.Column<int>(type: "int", nullable: false, comment: "餐前餐后(0=餐前，1=餐后)"),
                    Value = table.Column<float>(type: "real", nullable: false, comment: "数值"),
                    NursingRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理记录Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpMmol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpMmol_RpNursingRecord_NursingRecordId",
                        column: x => x.NursingRecordId,
                        principalTable: "RpNursingRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "指尖血糖 mmol/L");

            migrationBuilder.CreateTable(
                name: "RpPupil",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PupilType = table.Column<int>(type: "int", nullable: false, comment: "瞳孔评估(0=左眼，1=右眼)"),
                    Diameter = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "直径（mm）"),
                    LightReaction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "对光反应（灵敏（+）/迟钝（-）/固定（±））"),
                    OtherTrait = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "其他特征（眼疾/义眼/缺失/破裂/肿胀/包扎/其他...）"),
                    Other = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "其他"),
                    NursingRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpPupil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpPupil_RpNursingRecord_NursingRecordId",
                        column: x => x.NursingRecordId,
                        principalTable: "RpNursingRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "瞳孔评估");

            migrationBuilder.CreateTable(
                name: "RpNursingSettingItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputType = table.Column<int>(type: "int", nullable: false, comment: "表单域类型（0=普通文本域,1=单选按钮,2=复选框,3=下拉框）"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "配置的值"),
                    Watermark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "水印配置，文本域用"),
                    Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "文本描述（复选框、单选按钮、下拉框用）"),
                    HasTextblock = table.Column<bool>(type: "bit", nullable: false, comment: "是否有提示文本"),
                    TextblockLeft = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "左边提示文本"),
                    TextblockRight = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "右边提示文本"),
                    NursingSettingHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "护理单表头配置Id"),
                    Lv = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "层"),
                    IsCarryInputBox = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否带输入框"),
                    HasNext = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否有下一层"),
                    NursingSettingItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "自关联外键"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
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
                    table.PrimaryKey("PK_RpNursingSettingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpNursingSettingItem_RpNursingSettingHeader_NursingSettingHeaderId",
                        column: x => x.NursingSettingHeaderId,
                        principalTable: "RpNursingSettingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RpNursingSettingItem_RpNursingSettingItem_NursingSettingItemId",
                        column: x => x.NursingSettingItemId,
                        principalTable: "RpNursingSettingItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "护理单配置项");

            migrationBuilder.CreateIndex(
                name: "IX_RpCharacteristic_NursingRecordId",
                table: "RpCharacteristic",
                column: "NursingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_RpCriticalIllness_NursingDocumentId",
                table: "RpCriticalIllness",
                column: "NursingDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RpDynamicData_Header",
                table: "RpDynamicData",
                column: "Header");

            migrationBuilder.CreateIndex(
                name: "IX_RpDynamicData_NursingDocumentId",
                table: "RpDynamicData",
                column: "NursingDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RpDynamicData_SheetIndex",
                table: "RpDynamicData",
                column: "SheetIndex");

            migrationBuilder.CreateIndex(
                name: "IX_RpDynamicField_NursingDocumentId",
                table: "RpDynamicField",
                column: "NursingDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RpIntake_NursingRecordId",
                table: "RpIntake",
                column: "NursingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_RpMmol_NursingRecordId",
                table: "RpMmol",
                column: "NursingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_RpNursingDocument_AdmissionTime",
                table: "RpNursingDocument",
                column: "AdmissionTime");

            migrationBuilder.CreateIndex(
                name: "IX_RpNursingDocument_PatientId",
                table: "RpNursingDocument",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_RpNursingRecord_NursingDocumentId",
                table: "RpNursingRecord",
                column: "NursingDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RpNursingSettingHeader_NursingSettingId",
                table: "RpNursingSettingHeader",
                column: "NursingSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_RpNursingSettingItem_NursingSettingHeaderId",
                table: "RpNursingSettingItem",
                column: "NursingSettingHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RpNursingSettingItem_NursingSettingItemId",
                table: "RpNursingSettingItem",
                column: "NursingSettingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RpPupil_NursingRecordId",
                table: "RpPupil",
                column: "NursingRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpCharacteristic");

            migrationBuilder.DropTable(
                name: "RpCriticalIllness");

            migrationBuilder.DropTable(
                name: "RpDynamicData");

            migrationBuilder.DropTable(
                name: "RpDynamicField");

            migrationBuilder.DropTable(
                name: "RpHospitalInfo");

            migrationBuilder.DropTable(
                name: "RpIntake");

            migrationBuilder.DropTable(
                name: "RpMmol");

            migrationBuilder.DropTable(
                name: "RpNursingSettingItem");

            migrationBuilder.DropTable(
                name: "RpPageSize");

            migrationBuilder.DropTable(
                name: "RpPrintCatalog");

            migrationBuilder.DropTable(
                name: "RpPrintSetting");

            migrationBuilder.DropTable(
                name: "RpPupil");

            migrationBuilder.DropTable(
                name: "RpReportData");

            migrationBuilder.DropTable(
                name: "RpNursingSettingHeader");

            migrationBuilder.DropTable(
                name: "RpNursingRecord");

            migrationBuilder.DropTable(
                name: "RpNursingSetting");

            migrationBuilder.DropTable(
                name: "RpNursingDocument");
        }
    }
}
