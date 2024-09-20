using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class add3tableslispacstreat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NursingPrescribes",
                table: "NursingPrescribes");

            migrationBuilder.RenameTable(
                name: "NursingPrescribes",
                newName: "NursingPrescribe");

            migrationBuilder.AlterTable(
                name: "NursingPrescribe",
                comment: "药物处方",
                oldComment: "药物");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NursingPrescribe",
                table: "NursingPrescribe",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NursingLis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱ID"),
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
                comment: "检查");

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
                    ReportTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "出报告时间"),
                    ReportDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认报告医生编码"),
                    ReportDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认报告医生"),
                    HasReport = table.Column<bool>(type: "bit", nullable: false, comment: "报告标识"),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false, comment: "是否紧急"),
                    IsBedSide = table.Column<bool>(type: "bit", nullable: false, comment: "是否在床旁"),
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
                comment: "检验");

            migrationBuilder.CreateTable(
                name: "NursingTreat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱ID"),
                    Qty = table.Column<int>(type: "int", nullable: false, comment: "数量"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "其它价格"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "规格"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认频次代码"),
                    FeeTypeMainCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费大类代码"),
                    FeeTypeSubCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "收费小类代码"),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingLis");

            migrationBuilder.DropTable(
                name: "NursingPacs");

            migrationBuilder.DropTable(
                name: "NursingTreat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NursingPrescribe",
                table: "NursingPrescribe");

            migrationBuilder.RenameTable(
                name: "NursingPrescribe",
                newName: "NursingPrescribes");

            migrationBuilder.AlterTable(
                name: "NursingPrescribes",
                comment: "药物",
                oldComment: "药物处方");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NursingPrescribes",
                table: "NursingPrescribes",
                column: "Id");
        }
    }
}
