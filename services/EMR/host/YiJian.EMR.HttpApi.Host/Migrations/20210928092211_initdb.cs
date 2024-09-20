using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrCatalogue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "目录名称"),
                    IsFile = table.Column<bool>(type: "bit", nullable: false, comment: "是否是文件（文件夹=false,文件=true）"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "父级Id，根级=0"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序权重"),
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
                });

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
                },
                comment: "被管理起来的XML电子病例模板(通用模板，科室模板，个人模板)");

            migrationBuilder.CreateTable(
                name: "EmrPatientEmr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "患者编号"),
                    PatientName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "患者名称"),
                    DoctorCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "医生编号"),
                    DoctorName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "医生名称"),
                    EmrTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "病历名称"),
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
                comment: "患者电子病历");

            migrationBuilder.CreateTable(
                name: "EmrPatientEmrXml",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmrXml = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "电子病历Xml文档"),
                    PatientEmrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者电子病历Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrPatientEmrXml", x => x.Id);
                },
                comment: "患者的电子病历xml文档");

            migrationBuilder.CreateTable(
                name: "EmrTemplateCatalogue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "目录名称"),
                    IsFile = table.Column<bool>(type: "bit", nullable: false, comment: "是否是文件（文件夹=false,文件=true）"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "父级Id，根级=0"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序权重"),
                    TemplateType = table.Column<int>(type: "int", nullable: false, comment: "模板类型"),
                    DeptCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "科室编码"),
                    InpatientWardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "病区id"),
                    DoctorCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, defaultValue: "", comment: "医生编码"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "", comment: "医生"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否启用"),
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
                });

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
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmrCatalogue_Title",
                table: "EmrCatalogue",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_EmrMyXmlTemplate_TemplateCatalogueId",
                table: "EmrMyXmlTemplate",
                column: "TemplateCatalogueId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrPatientEmr_DoctorCode",
                table: "EmrPatientEmr",
                column: "DoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmrPatientEmrXml_PatientEmrId",
                table: "EmrPatientEmrXml",
                column: "PatientEmrId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrTemplateCatalogue_DeptCode",
                table: "EmrTemplateCatalogue",
                column: "DeptCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmrTemplateCatalogue_DoctorCode",
                table: "EmrTemplateCatalogue",
                column: "DoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmrTemplateCatalogue_Title",
                table: "EmrTemplateCatalogue",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_EmrXmlTemplate_CatalogueId",
                table: "EmrXmlTemplate",
                column: "CatalogueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrInpatientWard");

            migrationBuilder.DropTable(
                name: "EmrMyXmlTemplate");

            migrationBuilder.DropTable(
                name: "EmrPatientEmr");

            migrationBuilder.DropTable(
                name: "EmrPatientEmrXml");

            migrationBuilder.DropTable(
                name: "EmrTemplateCatalogue");

            migrationBuilder.DropTable(
                name: "EmrXmlTemplate");

            migrationBuilder.DropTable(
                name: "EmrCatalogue");
        }
    }
}
