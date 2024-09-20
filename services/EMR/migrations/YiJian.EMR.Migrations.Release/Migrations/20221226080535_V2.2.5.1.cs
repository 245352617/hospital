using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations.Migrations
{
    public partial class V2251 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrAppSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "配置名称"),
                    Data = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "配置值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrAppSetting", x => x.Id);
                },
                comment: "应用配置");

            migrationBuilder.CreateTable(
                name: "EmrCatalogue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "目录名称"),
                    IsFile = table.Column<bool>(type: "bit", nullable: false, comment: "是否是文件（文件夹=false,文件=true）"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "父级Id，根级=0"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序权重"),
                    Lv = table.Column<int>(type: "int", nullable: false, comment: "目录结构层级Level"),
                    Classify = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "电子文书分类(0=电子病历,1=文书)"),
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
                    table.PrimaryKey("PK_EmrCatalogue", x => x.Id);
                },
                comment: "电子病历库目录树");

            migrationBuilder.CreateTable(
                name: "EmrCategoryProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "属性值"),
                    Label = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "属性标签"),
                    Lv = table.Column<int>(type: "int", nullable: false, comment: "属性层级"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "级联父节点Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrCategoryProperty", x => x.Id);
                },
                comment: "电子病历属性");

            migrationBuilder.CreateTable(
                name: "EmrDataElement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "名称标题"),
                    IsElement = table.Column<bool>(type: "bit", nullable: false, comment: "是否是元素"),
                    Parent = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "父级级联Id"),
                    Lv = table.Column<int>(type: "int", nullable: false, comment: "层级，默认=0"),
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
                    table.PrimaryKey("PK_EmrDataElement", x => x.Id);
                },
                comment: "数据元集合根");

            migrationBuilder.CreateTable(
                name: "EmrDataElementDropdown",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataElementItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "元素项Id"),
                    IsAllowMultiple = table.Column<bool>(type: "bit", nullable: false, comment: "允许多选"),
                    IsSortByTime = table.Column<bool>(type: "bit", nullable: false, comment: "数值勾选按照时间排序"),
                    IsDynamicallyLoad = table.Column<bool>(type: "bit", nullable: false, comment: "动态加载下拉列表"),
                    DataSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "来源"),
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
                    table.PrimaryKey("PK_EmrDataElementDropdown", x => x.Id);
                },
                comment: "输入域类型下拉项目");

            migrationBuilder.CreateTable(
                name: "EmrDataElementDropdownItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataElementDropdownId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "输入域类型下拉项目Id"),
                    Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "文本"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "数值"),
                    ListText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "指定的列表文本"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "分组名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrDataElementDropdownItem", x => x.Id);
                },
                comment: "元素下拉项");

            migrationBuilder.CreateTable(
                name: "EmrDataElementItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    No = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "编号"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "名称"),
                    Watermark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "背景文本"),
                    Tips = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "提示文本"),
                    BeginMargin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "起始边框"),
                    EndMargin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "结尾边框"),
                    FixedWidth = table.Column<int>(type: "int", nullable: false, comment: "固定宽度"),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false, comment: "只读状态"),
                    DataSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "数据源"),
                    BindPath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "绑定路径"),
                    CascadeExpression = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "级联表达式"),
                    NumericalExpression = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "数值表达式"),
                    CanModify = table.Column<bool>(type: "bit", nullable: false, comment: "用户可以直接修改内容"),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false, comment: "允许被删除"),
                    InputType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "输入域类型"),
                    NeedVerification = table.Column<bool>(type: "bit", nullable: false, comment: "是否开启校验"),
                    VerificationExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "校验表达式"),
                    DataElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "数据元Id"),
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
                    table.PrimaryKey("PK_EmrDataElementItem", x => x.Id);
                },
                comment: "数据元素");

            migrationBuilder.CreateTable(
                name: "EmrDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "科室名称"),
                    DeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "科室名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrDepartment", x => x.Id);
                },
                comment: "科室历史记录");

            migrationBuilder.CreateTable(
                name: "EmrEmrBaseInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, defaultValue: "H7110", comment: "机构ID,固定：H7110"),
                    DeptCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "书写科室:一级科室代码"),
                    DoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "书写医生:书写医生工号"),
                    PatientId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "病人ID:His内部识别id"),
                    VisitNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "就诊号"),
                    RegisterNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "挂号识别号"),
                    ChiefComplaint = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "主诉"),
                    HistoryPresentIllness = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "现病史"),
                    AllergySign = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "药物过敏史"),
                    MedicalHistory = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "既往史"),
                    BodyExam = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "体格体查"),
                    PreliminaryDiagnosis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "初步诊断"),
                    HandlingOpinions = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "处理意见"),
                    OutpatientSurgery = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "门诊手术"),
                    AuxiliaryExamination = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "辅助检查"),
                    Channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "SZKJ", comment: "渠道来源,尚哲：SZKJ"),
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
                    table.PrimaryKey("PK_EmrEmrBaseInfo", x => x.Id);
                },
                comment: "电子病例采集的基础信息");

            migrationBuilder.CreateTable(
                name: "EmrInpatientWard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WardName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "病区名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrInpatientWard", x => x.Id);
                },
                comment: "病区");

            migrationBuilder.CreateTable(
                name: "EmrPatientEmr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "患者编号"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "患者名称"),
                    DoctorCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "医生编号"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生名称"),
                    DeptCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmrTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "病历名称"),
                    CategoryLv1 = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "一级分类"),
                    CategoryLv2 = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "二级分类"),
                    AdmissionTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "入院时间"),
                    DischargeTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "出院时间"),
                    Classify = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "电子文书分类（0=电子病历，1=文书）"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false, comment: "是否已归档，默认false=未归档"),
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
                    table.PrimaryKey("PK_EmrPatientEmr", x => x.Id);
                },
                comment: "患者电子病历/文书...");

            migrationBuilder.CreateTable(
                name: "EmrPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionCode = table.Column<int>(type: "int", nullable: false, comment: "权限代码"),
                    Module = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "模块"),
                    PermissionTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "权限"),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "描述"),
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
                    table.PrimaryKey("PK_EmrPermission", x => x.Id);
                },
                comment: "EMR权限管理模型");

            migrationBuilder.CreateTable(
                name: "EmrPhraseCatalogue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "目录标题"),
                    TemplateType = table.Column<int>(type: "int", nullable: false, comment: "模板类型，0=通用(全院)，1=科室，2=个人"),
                    Belonger = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "归属人 如果 TemplateType=2 归属者为医生Id doctorId, 如果 TemplateType=1 归属者为科室id deptid , 如果 TemplateType=0 归属者为hospital"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号码"),
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
                    table.PrimaryKey("PK_EmrPhraseCatalogue", x => x.Id);
                },
                comment: "常用语目录");

            migrationBuilder.CreateTable(
                name: "EmrTemplateCatalogue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "目录名称"),
                    IsFile = table.Column<bool>(type: "bit", nullable: false, comment: "是否是文件（文件夹=false,文件=true）"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "父级Id，根级=0"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序权重"),
                    TemplateType = table.Column<int>(type: "int", nullable: false, comment: "模板类型"),
                    DeptCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "科室编码"),
                    InpatientWardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "病区id"),
                    DoctorCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, defaultValue: "", comment: "医生编码"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "", comment: "医生"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否启用"),
                    Lv = table.Column<int>(type: "int", nullable: false, comment: "目录结构层级Level"),
                    CatalogueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "最初引入病历库的Id"),
                    CatalogueTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "最初引入病历库的名称"),
                    Classify = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "电子文书分类(0=电子病历,1=文书)"),
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
                    table.PrimaryKey("PK_EmrTemplateCatalogue", x => x.Id);
                },
                comment: "模板目录结构");

            migrationBuilder.CreateTable(
                name: "EmrUniversalCharacter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "分类"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrUniversalCharacter", x => x.Id);
                },
                comment: "通用字符");

            migrationBuilder.CreateTable(
                name: "EmrXmlHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XmlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmrXml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    XmlCategory = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_EmrXmlHistory", x => x.Id);
                },
                comment: "电子病历留痕实体");

            migrationBuilder.CreateTable(
                name: "EmrXmlTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateXml = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "xml模板"),
                    CatalogueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "目录结构树模板Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrXmlTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrXmlTemplate_EmrCatalogue_CatalogueId",
                        column: x => x.CatalogueId,
                        principalTable: "EmrCatalogue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "xml病历模板");

            migrationBuilder.CreateTable(
                name: "EmrDataBindContext",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, defaultValue: "", comment: "就诊号"),
                    RegisterSerialNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, defaultValue: "", comment: "流水号"),
                    OrgCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "", comment: "机构ID"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者唯一Id"),
                    PatientId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "患者Id"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "患者名称"),
                    WriterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "录入人Id"),
                    WriterName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "录入人名称"),
                    Classify = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "电子文书分类（0=电子病历，1=文书）"),
                    PatientEmrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者电子病历Id"),
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
                    table.PrimaryKey("PK_EmrDataBindContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrDataBindContext_EmrPatientEmr_PatientEmrId",
                        column: x => x.PatientEmrId,
                        principalTable: "EmrPatientEmr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "电子病历、文书绑定的数据上下文");

            migrationBuilder.CreateTable(
                name: "EmrPatientEmrXml",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmrXml = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "电子病历Xml文档"),
                    PatientEmrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者电子病历Id"),
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
                    table.PrimaryKey("PK_EmrPatientEmrXml", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrPatientEmrXml_EmrPatientEmr_PatientEmrId",
                        column: x => x.PatientEmrId,
                        principalTable: "EmrPatientEmr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "患者的电子病历xml文档");

            migrationBuilder.CreateTable(
                name: "EmrOperatingAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室编码"),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    DoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医生编码"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医生"),
                    PermissionId = table.Column<int>(type: "int", nullable: false, comment: "权限Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrOperatingAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrOperatingAccount_EmrPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "EmrPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "操作账号信息");

            migrationBuilder.CreateTable(
                name: "EmrPhrase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "标题"),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "内容"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号码"),
                    CatalogueId = table.Column<int>(type: "int", nullable: false, comment: "目录Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrPhrase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrPhrase_EmrPhraseCatalogue_CatalogueId",
                        column: x => x.CatalogueId,
                        principalTable: "EmrPhraseCatalogue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "病历常用语");

            migrationBuilder.CreateTable(
                name: "EmrMyXmlTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateXml = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "xml模板"),
                    TemplateCatalogueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "目录结构树模板Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrMyXmlTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrMyXmlTemplate_EmrTemplateCatalogue_TemplateCatalogueId",
                        column: x => x.TemplateCatalogueId,
                        principalTable: "EmrTemplateCatalogue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "被管理起来的XML电子病例模板(通用模板，科室模板，个人模板)");

            migrationBuilder.CreateTable(
                name: "EmrUniversalCharacterNode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Character = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "字符"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    UniversalCharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrUniversalCharacterNode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrUniversalCharacterNode_EmrUniversalCharacter_UniversalCharacterId",
                        column: x => x.UniversalCharacterId,
                        principalTable: "EmrUniversalCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmrDataBindMap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "数据分类"),
                    Path = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "绑定的数据名称"),
                    Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "绑定的数据"),
                    DataBindContextId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "数据上下文Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrDataBindMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrDataBindMap_EmrDataBindContext_DataBindContextId",
                        column: x => x.DataBindContextId,
                        principalTable: "EmrDataBindContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "数据绑定字典");

            migrationBuilder.CreateIndex(
                name: "IX_EmrCatalogue_Title",
                table: "EmrCatalogue",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataBindContext_PatientEmrId",
                table: "EmrDataBindContext",
                column: "PatientEmrId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataBindContext_PI_ID",
                table: "EmrDataBindContext",
                column: "PI_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataBindMap_DataBindContextId",
                table: "EmrDataBindMap",
                column: "DataBindContextId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataElementDropdown_DataElementItemId",
                table: "EmrDataElementDropdown",
                column: "DataElementItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataElementDropdownItem_DataElementDropdownId",
                table: "EmrDataElementDropdownItem",
                column: "DataElementDropdownId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataElementItem_DataElementId",
                table: "EmrDataElementItem",
                column: "DataElementId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrMyXmlTemplate_TemplateCatalogueId",
                table: "EmrMyXmlTemplate",
                column: "TemplateCatalogueId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrOperatingAccount_PermissionId",
                table: "EmrOperatingAccount",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrPatientEmr_DoctorCode",
                table: "EmrPatientEmr",
                column: "DoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmrPatientEmrXml_PatientEmrId",
                table: "EmrPatientEmrXml",
                column: "PatientEmrId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrPhrase_CatalogueId",
                table: "EmrPhrase",
                column: "CatalogueId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrTemplateCatalogue_DeptCode",
                table: "EmrTemplateCatalogue",
                column: "DeptCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmrTemplateCatalogue_DoctorCode",
                table: "EmrTemplateCatalogue",
                column: "DoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmrTemplateCatalogue_Lv",
                table: "EmrTemplateCatalogue",
                column: "Lv");

            migrationBuilder.CreateIndex(
                name: "IX_EmrTemplateCatalogue_Title",
                table: "EmrTemplateCatalogue",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_EmrUniversalCharacterNode_UniversalCharacterId",
                table: "EmrUniversalCharacterNode",
                column: "UniversalCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrXmlHistory_XmlCategory",
                table: "EmrXmlHistory",
                column: "XmlCategory");

            migrationBuilder.CreateIndex(
                name: "IX_EmrXmlHistory_XmlId",
                table: "EmrXmlHistory",
                column: "XmlId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrXmlTemplate_CatalogueId",
                table: "EmrXmlTemplate",
                column: "CatalogueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrAppSetting");

            migrationBuilder.DropTable(
                name: "EmrCategoryProperty");

            migrationBuilder.DropTable(
                name: "EmrDataBindMap");

            migrationBuilder.DropTable(
                name: "EmrDataElement");

            migrationBuilder.DropTable(
                name: "EmrDataElementDropdown");

            migrationBuilder.DropTable(
                name: "EmrDataElementDropdownItem");

            migrationBuilder.DropTable(
                name: "EmrDataElementItem");

            migrationBuilder.DropTable(
                name: "EmrDepartment");

            migrationBuilder.DropTable(
                name: "EmrEmrBaseInfo");

            migrationBuilder.DropTable(
                name: "EmrInpatientWard");

            migrationBuilder.DropTable(
                name: "EmrMyXmlTemplate");

            migrationBuilder.DropTable(
                name: "EmrOperatingAccount");

            migrationBuilder.DropTable(
                name: "EmrPatientEmrXml");

            migrationBuilder.DropTable(
                name: "EmrPhrase");

            migrationBuilder.DropTable(
                name: "EmrUniversalCharacterNode");

            migrationBuilder.DropTable(
                name: "EmrXmlHistory");

            migrationBuilder.DropTable(
                name: "EmrXmlTemplate");

            migrationBuilder.DropTable(
                name: "EmrDataBindContext");

            migrationBuilder.DropTable(
                name: "EmrTemplateCatalogue");

            migrationBuilder.DropTable(
                name: "EmrPermission");

            migrationBuilder.DropTable(
                name: "EmrPhraseCatalogue");

            migrationBuilder.DropTable(
                name: "EmrUniversalCharacter");

            migrationBuilder.DropTable(
                name: "EmrCatalogue");

            migrationBuilder.DropTable(
                name: "EmrPatientEmr");
        }
    }
}
