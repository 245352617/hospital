using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations.Migrations
{
    public partial class V2251 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_AllItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "分类编码"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "分类名称"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    PY = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "拼音首字母"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "单位"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "价格"),
                    IndexNo = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    TypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "类型编码"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "类型名称"),
                    ChargeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费分类编码"),
                    ChargeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "收费分类名称"),
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
                    table.PrimaryKey("PK_Dict_AllItem", x => x.Id);
                },
                comment: "诊疗检查检验药品项目合集");

            migrationBuilder.CreateTable(
                name: "Dict_Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "编码"),
                    RegisterCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_Dict_Department", x => x.Id);
                },
                comment: "科室表");

            migrationBuilder.CreateTable(
                name: "Dict_Dictionaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DictionariesCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "字典编码"),
                    DictionariesName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "字典名称"),
                    DictionariesTypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "字典类型编码"),
                    DictionariesTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "字典类型名称"),
                    Status = table.Column<bool>(type: "bit", nullable: false, comment: "使用状态"),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "备注"),
                    Py = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "拼音码"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    IsDefaltChecked = table.Column<bool>(type: "bit", nullable: false, comment: "默认选中"),
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
                    table.PrimaryKey("PK_Dict_Dictionaries", x => x.Id);
                },
                comment: "平台字典表");

            migrationBuilder.CreateTable(
                name: "Dict_DictionariesMultitype",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "字典分组编码"),
                    GroupName = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "字典分组名称"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "字典编码"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "字典名称"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "配置类型"),
                    Value = table.Column<string>(type: "nvarchar(500)", nullable: false, comment: "配置值"),
                    DataFrom = table.Column<int>(type: "int", nullable: false, comment: "数据来源，0：急诊添加，1：预检分诊同步"),
                    Status = table.Column<bool>(type: "bit", nullable: false, comment: "状态"),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "备注"),
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
                    table.PrimaryKey("PK_Dict_DictionariesMultitype", x => x.Id);
                },
                comment: "字典多类型表");

            migrationBuilder.CreateTable(
                name: "Dict_DictionariesType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DictionariesTypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "字典类型编码"),
                    DictionariesTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "字典类型名称"),
                    DataFrom = table.Column<int>(type: "int", nullable: false, comment: "数据来源，0：急诊添加，1：预检分诊同步"),
                    Status = table.Column<bool>(type: "bit", nullable: false, comment: "状态"),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "备注"),
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
                    table.PrimaryKey("PK_Dict_DictionariesType", x => x.Id);
                },
                comment: "字典类型编码");

            migrationBuilder.CreateTable(
                name: "Dict_Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医生代码"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生姓名"),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "机构编码"),
                    BranchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "机构名称"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "科室代码"),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "科室名称"),
                    Sex = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医生性别"),
                    DoctorTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生职称"),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "联系电话"),
                    Skill = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医生擅长"),
                    Introdution = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "医生简介"),
                    AnaesthesiaAuthority = table.Column<bool>(type: "bit", nullable: false, comment: "麻醉处方权限"),
                    DrugAuthority = table.Column<bool>(type: "bit", nullable: false, comment: "处方权限"),
                    SpiritAuthority = table.Column<bool>(type: "bit", nullable: false, comment: "精神处方权限"),
                    AntibioticAuthority = table.Column<bool>(type: "bit", nullable: false, comment: "抗生素处方权限"),
                    PracticeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医师执业代码"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    DoctorType = table.Column<int>(type: "int", nullable: false, comment: "人员类型	1.急诊医生  2.急诊护士 0.其他人员"),
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
                    table.PrimaryKey("PK_Dict_Doctor", x => x.Id);
                },
                comment: "医生表");

            migrationBuilder.CreateTable(
                name: "Dict_Entrust",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "嘱托编码"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "嘱托名称"),
                    PrescribeTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型编码"),
                    PrescribeTypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型：临嘱、长嘱、出院带药等"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次编码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "频次名称"),
                    RecieveQty = table.Column<int>(type: "int", nullable: false, comment: "领量(数量)"),
                    RecieveUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "领量单位"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "五笔码"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Entrust", x => x.Id);
                },
                comment: "嘱托配置");

            migrationBuilder.CreateTable(
                name: "Dict_ExamCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstNodeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "编码"),
                    FirstNodeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "名称"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "五笔"),
                    RoomCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行机房编码"),
                    RoomName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行机房名称"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_Dict_ExamCatalog", x => x.Id);
                },
                comment: "检查目录");

            migrationBuilder.CreateTable(
                name: "Dict_ExamNote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "注意事项编码"),
                    NoteName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "注意事项名称"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行科室编码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "执行科室名称"),
                    DisplayName = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单"),
                    DescTemplate = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "检查申请描述模板"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_Dict_ExamNote", x => x.Id);
                },
                comment: "检查申请注意事项");

            migrationBuilder.CreateTable(
                name: "Dict_ExamPart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "检查部位编码"),
                    PartName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "检查部位名称"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
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
                    table.PrimaryKey("PK_Dict_ExamPart", x => x.Id);
                },
                comment: "检查部位");

            migrationBuilder.CreateTable(
                name: "Dict_ExamProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    ProjectName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "目录编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "目录名称"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
                    PartCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位编码"),
                    PartName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位名称"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "价格"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    RoomCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行机房编码"),
                    RoomName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行机房名称"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "平台标识"),
                    AddCard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "附加卡片类型 12.TCT细胞学检查申请单 11.病理检验申请单 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用"),
                    GuideCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "指引ID 关联 ExamNote表code"),
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
                    table.PrimaryKey("PK_Dict_ExamProject", x => x.Id);
                },
                comment: "检查申请项目");

            migrationBuilder.CreateTable(
                name: "Dict_ExamTarget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TargetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TargetUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsuranceType = table.Column<int>(type: "int", nullable: false),
                    SpecialFlag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProjectType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "项目类型--龙岗字典所需"),
                    ProjectMerge = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "项目归类--龙岗字典所需"),
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
                    table.PrimaryKey("PK_Dict_ExamTarget", x => x.Id);
                },
                comment: "检查明细项");

            migrationBuilder.CreateTable(
                name: "Dict_LabCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "分类编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "分类名称"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行科室编码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行科室名称"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_Dict_LabCatalog", x => x.Id);
                },
                comment: "检验目录");

            migrationBuilder.CreateTable(
                name: "Dict_LabContainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContainerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "容器编码"),
                    ContainerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "容器名称"),
                    ContainerColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "容器颜色"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_Dict_LabContainer", x => x.Id);
                },
                comment: "容器编码");

            migrationBuilder.CreateTable(
                name: "Dict_LabProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "编码"),
                    ProjectName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检验目录编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "检验目录名称"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本编码"),
                    SpecimenName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "标本名称"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    SpecimenPartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "检验位置编码"),
                    SpecimenPartName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "检验位置名称"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "单位"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "价格"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "其他价格"),
                    ContainerCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "容器编码"),
                    ContainerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "容器名称"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "平台标识"),
                    AddCard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "附加卡片类型 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)14.新型冠状病毒RNA检测申请单13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单"),
                    GuideCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "指引ID 关联 ExamNote表code"),
                    CatalogAndProjectCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "分类编码和当前项目的编码组合"),
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
                    table.PrimaryKey("PK_Dict_LabProject", x => x.Id);
                },
                comment: "检验项目");

            migrationBuilder.CreateTable(
                name: "Dict_LabSpecimen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "标本编码"),
                    SpecimenName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "标本名称"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_Dict_LabSpecimen", x => x.Id);
                },
                comment: "检验标本");

            migrationBuilder.CreateTable(
                name: "Dict_LabSpecimenPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "标本编码"),
                    SpecimenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "标本名称"),
                    SpecimenPartCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "采集部位编码"),
                    SpecimenPartName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "采集部位名称"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_Dict_LabSpecimenPosition", x => x.Id);
                },
                comment: "检验标本采集部位");

            migrationBuilder.CreateTable(
                name: "Dict_LabTarget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "编码"),
                    TargetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    ProjectCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "项目编码"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
                    TargetUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "单位"),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "数量"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "价格"),
                    InsuranceType = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "医保目录:0=自费,1=甲类,2=乙类,3=其它"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ProjectType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "项目类型--龙岗字典所需"),
                    ProjectMerge = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "项目归类--龙岗字典所需"),
                    CatalogAndProjectCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "分类编码和当前项目的编码组合"),
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
                    table.PrimaryKey("PK_Dict_LabTarget", x => x.Id);
                },
                comment: "检验明细项");

            migrationBuilder.CreateTable(
                name: "Dict_Medicine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "药品编码"),
                    MedicineName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "药品名称"),
                    ScientificName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "学名"),
                    Alias = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "别名"),
                    AliasPyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名拼音"),
                    AliasWbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名五笔码"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
                    MedicineProperty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "药物属性：西药、中药、西药制剂、中药制剂"),
                    DefaultDosage = table.Column<double>(type: "float", maxLength: 20, nullable: false, comment: "默认剂量"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false, comment: "剂量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "剂量单位"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "基本单位"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "基本单位价格"),
                    BigPackPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "大包装价格"),
                    BigPackFactor = table.Column<int>(type: "int", nullable: false, comment: "大包装量(大包装系数)"),
                    BigPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "大包装单位"),
                    SmallPackPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "小包装单价"),
                    SmallPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "小包装单位"),
                    SmallPackFactor = table.Column<int>(type: "int", nullable: false, comment: "小包装量(小包装系数)"),
                    ChildrenPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "儿童价格"),
                    FixPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "批发价格"),
                    RetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "零售价格"),
                    InsuranceType = table.Column<int>(type: "int", nullable: false, comment: "医保类型：0自费,1甲类,2乙类，3丙类"),
                    InsuranceCode = table.Column<int>(type: "int", nullable: false, comment: "医保类型代码"),
                    InsuranceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医保类型名称"),
                    InsurancePayRate = table.Column<int>(type: "int", nullable: true, comment: "医保支付比例"),
                    FactoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "厂家名称"),
                    FactoryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "厂家代码"),
                    Weight = table.Column<double>(type: "float", nullable: true, comment: "重量"),
                    WeightUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "重量单位"),
                    Volume = table.Column<double>(type: "float", nullable: true, comment: "体积"),
                    VolumeUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "体积单位"),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "备注"),
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
                    LimitedNote = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "限制性用药描述"),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "规格"),
                    DosageForm = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "剂型"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行科室编码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行科室名称"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "默认用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认用法名称"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "默认频次编码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认频次名称"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    IsFirstAid = table.Column<bool>(type: "bit", nullable: true, comment: "急救药"),
                    PharmacyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "药房代码"),
                    PharmacyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "药房"),
                    AntibioticPermission = table.Column<int>(type: "int", nullable: false, comment: "抗生素权限"),
                    PrescriptionPermission = table.Column<int>(type: "int", nullable: false, comment: "处方权"),
                    BaseFlag = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "基药标准 01国基，02省基，03市基，04基药，05中草药，06非基药"),
                    MedicalInsuranceCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医保编码"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "平台标识"),
                    Unpack = table.Column<int>(type: "int", nullable: false, comment: "门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分"),
                    IsSyncToReciped = table.Column<bool>(type: "bit", nullable: false, comment: "是否已经全量同步过Recipes库"),
                    EmergencySign = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "（急诊处方标志）1.急诊 0.普通"),
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
                    table.PrimaryKey("PK_Dict_Medicine", x => x.Id);
                },
                comment: "药品字典");

            migrationBuilder.CreateTable(
                name: "Dict_MedicineFrequency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次编码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "频次名称"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "频次全称"),
                    Times = table.Column<int>(type: "int", nullable: false, comment: "频次系数"),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "频次单位"),
                    ExecDayTimes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开"),
                    Weeks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "频次周明细"),
                    Catalog = table.Column<int>(type: "int", nullable: false, comment: "频次分类 0：临时 1：长期 2：通用"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    Remark = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "备注"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ThirdPartyId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "第三方id"),
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
                    table.PrimaryKey("PK_Dict_MedicineFrequency", x => x.Id);
                },
                comment: "药品频次字典");

            migrationBuilder.CreateTable(
                name: "Dict_MedicineUsage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsageCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "编码"),
                    UsageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "全称"),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "备注"),
                    IsSingle = table.Column<bool>(type: "bit", nullable: false, comment: "是否单次"),
                    Catalog = table.Column<int>(type: "int", nullable: false, comment: "分类  1：输液  2：注射  3：治疗  4：服药  10其他"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔码"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    TreatCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "诊疗项目 描述：一个或多个项目，多个以,隔开"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    AddCard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "附加卡片类型10.注射单,皮试单  08.雾化申请单  09.输液卡"),
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
                    table.PrimaryKey("PK_Dict_MedicineUsage", x => x.Id);
                },
                comment: "药品用法字典");

            migrationBuilder.CreateTable(
                name: "Dict_Operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationCode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    OperationName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PyCode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AddUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleteUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField3 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField4 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField5 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Operation", x => x.Id);
                },
                comment: "手术字典表");

            migrationBuilder.CreateTable(
                name: "Dict_Pharmacy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PharmacyCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "药房编号"),
                    PharmacyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "药房名称"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "默认药房，1=是默认药房")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Pharmacy", x => x.Id);
                },
                comment: "药房配置");

            migrationBuilder.CreateTable(
                name: "Dict_Separation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false, comment: "分方单分类编码，0=注射单，1=输液单，2=雾化单..."),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "分方单名称"),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    PrintSettingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "打印模板Id"),
                    PrintSettingName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "分方单名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Separation", x => x.Id);
                },
                comment: "分方途径分类实体");

            migrationBuilder.CreateTable(
                name: "Dict_Treat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreatCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "编码"),
                    TreatName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    PyCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "五笔"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "单价"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "其它价格"),
                    Additional = table.Column<bool>(type: "bit", nullable: false, comment: "加收标志"),
                    CategoryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "诊疗处置类别代码"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "诊疗处置类别名称"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "规格"),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "单位"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认频次代码"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行科室代码"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "执行科室名称"),
                    FeeTypeMainCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费大类代码"),
                    FeeTypeSubCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费小类代码"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "平台标识"),
                    ProjectMerge = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "项目归类--龙岗字典所需"),
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
                    table.PrimaryKey("PK_Dict_Treat", x => x.Id);
                },
                comment: "诊疗项目字典");

            migrationBuilder.CreateTable(
                name: "Dict_TreatGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatalogCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "目录编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "目录名称"),
                    DictionaryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "字典编码"),
                    DictionaryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "字典名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_TreatGroup", x => x.Id);
                },
                comment: "诊疗分组");

            migrationBuilder.CreateTable(
                name: "Dict_ViewSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prop = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "属性"),
                    DefaultLabel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "默认标头"),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "标头"),
                    DefaultHeaderAlign = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认标头对齐"),
                    HeaderAlign = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "标头对齐"),
                    DefaultAlign = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认对齐"),
                    Align = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "对齐"),
                    DefaultWidth = table.Column<int>(type: "int", nullable: false, comment: "默认宽度"),
                    Width = table.Column<int>(type: "int", nullable: false, comment: "宽度"),
                    DefaultMinWidth = table.Column<int>(type: "int", nullable: false, comment: "默认最小宽度"),
                    MinWidth = table.Column<int>(type: "int", nullable: false, comment: "最小宽度"),
                    DefaultVisible = table.Column<bool>(type: "bit", nullable: false, comment: "默认显示"),
                    Visible = table.Column<bool>(type: "bit", nullable: false, comment: "是否显示"),
                    DefaultShowTooltip = table.Column<bool>(type: "bit", nullable: false, comment: "默认是否提示"),
                    ShowTooltip = table.Column<bool>(type: "bit", nullable: false, comment: "是否提示"),
                    DefaultIndex = table.Column<int>(type: "int", nullable: false, comment: "默认序号"),
                    Index = table.Column<int>(type: "int", nullable: false, comment: "序号"),
                    View = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "视图"),
                    Comment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "注释"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: false, comment: "父级ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ViewSetting", x => x.Id);
                },
                comment: "视图配置");

            migrationBuilder.CreateTable(
                name: "Dict_VitalSignExpression",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "评分项"),
                    StLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅰ级评分表达式"),
                    NdLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅱ级评分表达式"),
                    RdLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅲ级评分表达式"),
                    ThALevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅳa级评分表达式"),
                    ThBLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅳb级评分表达式"),
                    DefaultStLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "默认Ⅰ级评分表达式"),
                    DefaultNdLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "默认Ⅱ级评分表达式"),
                    DefaultRdLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "默认Ⅲ级评分表达式"),
                    DefaultThALevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "默认Ⅳa级评分表达式"),
                    DefaultThBLevelExpression = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, comment: "默认Ⅳb级评分表达式")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_VitalSignExpression", x => x.Id);
                },
                comment: "生命体征表达式");

            migrationBuilder.CreateTable(
                name: "Sys_ReceivedLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "路由键"),
                    Queue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "队列"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true, comment: "内容"),
                    Retries = table.Column<int>(type: "int", nullable: false, comment: "重试次数"),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "新增时间"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "终止时间"),
                    StatusName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "状态")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ReceivedLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "区域编码"),
                    RegionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "区域名称"),
                    RegionType = table.Column<int>(type: "int", nullable: false, comment: "区域类型"),
                    ParentCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "父级编码"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Sequence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    Value = table.Column<int>(type: "int", nullable: false, comment: "序列值"),
                    Format = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "格式"),
                    Length = table.Column<int>(type: "int", nullable: false, comment: "序列值长度"),
                    Date = table.Column<DateTime>(type: "date", nullable: false, comment: "日期"),
                    Memo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "备注"),
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
                    table.PrimaryKey("PK_Sys_Sequence", x => x.Id);
                },
                comment: "序列");

            migrationBuilder.CreateTable(
                name: "Dict_ConsultingRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "编码"),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "IP"),
                    CallScreenIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ConsultingRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dict_ConsultingRoom_Dict_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Dict_Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "诊室表");

            migrationBuilder.CreateTable(
                name: "Dict_Usage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法名称"),
                    SeparationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "分方配置Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Usage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dict_Usage_Dict_Separation_SeparationId",
                        column: x => x.SeparationId,
                        principalTable: "Dict_Separation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dict_AllItem_CategoryCode",
                table: "Dict_AllItem",
                column: "CategoryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ConsultingRoom_DepartmentId",
                table: "Dict_ConsultingRoom",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_DictionariesType_DictionariesTypeCode",
                table: "Dict_DictionariesType",
                column: "DictionariesTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamCatalog_CatalogCode",
                table: "Dict_ExamCatalog",
                column: "CatalogCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamNote_NoteCode",
                table: "Dict_ExamNote",
                column: "NoteCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamPart_PartCode",
                table: "Dict_ExamPart",
                column: "PartCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamProject_ProjectCode",
                table: "Dict_ExamProject",
                column: "ProjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamTarget_TargetCode",
                table: "Dict_ExamTarget",
                column: "TargetCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabCatalog_CatalogCode",
                table: "Dict_LabCatalog",
                column: "CatalogCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabContainer_ContainerCode",
                table: "Dict_LabContainer",
                column: "ContainerCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabProject_ProjectCode",
                table: "Dict_LabProject",
                column: "ProjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabSpecimen_SpecimenCode",
                table: "Dict_LabSpecimen",
                column: "SpecimenCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabSpecimenPosition_SpecimenCode",
                table: "Dict_LabSpecimenPosition",
                column: "SpecimenCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabTarget_TargetCode",
                table: "Dict_LabTarget",
                column: "TargetCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_Medicine_MedicineCode",
                table: "Dict_Medicine",
                column: "MedicineCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_MedicineFrequency_FrequencyCode",
                table: "Dict_MedicineFrequency",
                column: "FrequencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_MedicineUsage_UsageCode",
                table: "Dict_MedicineUsage",
                column: "UsageCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_Treat_TreatCode",
                table: "Dict_Treat",
                column: "TreatCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_Usage_SeparationId",
                table: "Dict_Usage",
                column: "SeparationId");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ViewSetting_Prop",
                table: "Dict_ViewSetting",
                column: "Prop");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_VitalSignExpression_ItemName",
                table: "Dict_VitalSignExpression",
                column: "ItemName");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Sequence_Code",
                table: "Sys_Sequence",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_AllItem");

            migrationBuilder.DropTable(
                name: "Dict_ConsultingRoom");

            migrationBuilder.DropTable(
                name: "Dict_Dictionaries");

            migrationBuilder.DropTable(
                name: "Dict_DictionariesMultitype");

            migrationBuilder.DropTable(
                name: "Dict_DictionariesType");

            migrationBuilder.DropTable(
                name: "Dict_Doctor");

            migrationBuilder.DropTable(
                name: "Dict_Entrust");

            migrationBuilder.DropTable(
                name: "Dict_ExamCatalog");

            migrationBuilder.DropTable(
                name: "Dict_ExamNote");

            migrationBuilder.DropTable(
                name: "Dict_ExamPart");

            migrationBuilder.DropTable(
                name: "Dict_ExamProject");

            migrationBuilder.DropTable(
                name: "Dict_ExamTarget");

            migrationBuilder.DropTable(
                name: "Dict_LabCatalog");

            migrationBuilder.DropTable(
                name: "Dict_LabContainer");

            migrationBuilder.DropTable(
                name: "Dict_LabProject");

            migrationBuilder.DropTable(
                name: "Dict_LabSpecimen");

            migrationBuilder.DropTable(
                name: "Dict_LabSpecimenPosition");

            migrationBuilder.DropTable(
                name: "Dict_LabTarget");

            migrationBuilder.DropTable(
                name: "Dict_Medicine");

            migrationBuilder.DropTable(
                name: "Dict_MedicineFrequency");

            migrationBuilder.DropTable(
                name: "Dict_MedicineUsage");

            migrationBuilder.DropTable(
                name: "Dict_Operation");

            migrationBuilder.DropTable(
                name: "Dict_Pharmacy");

            migrationBuilder.DropTable(
                name: "Dict_Treat");

            migrationBuilder.DropTable(
                name: "Dict_TreatGroup");

            migrationBuilder.DropTable(
                name: "Dict_Usage");

            migrationBuilder.DropTable(
                name: "Dict_ViewSetting");

            migrationBuilder.DropTable(
                name: "Dict_VitalSignExpression");

            migrationBuilder.DropTable(
                name: "Sys_ReceivedLog");

            migrationBuilder.DropTable(
                name: "Sys_Region");

            migrationBuilder.DropTable(
                name: "Sys_Sequence");

            migrationBuilder.DropTable(
                name: "Dict_Department");

            migrationBuilder.DropTable(
                name: "Dict_Separation");
        }
    }
}
