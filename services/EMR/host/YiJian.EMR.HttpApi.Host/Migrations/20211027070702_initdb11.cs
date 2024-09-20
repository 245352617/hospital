using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "EmrXmlTemplate",
                comment: "xml病历模板");

            migrationBuilder.AlterTable(
                name: "EmrTemplateCatalogue",
                comment: "模板目录结构");

            migrationBuilder.AlterTable(
                name: "EmrCatalogue",
                comment: "电子病历库目录树");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "EmrPatientEmrXml",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "EmrPatientEmrXml",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "EmrPatientEmrXml",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "EmrPatientEmrXml",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "EmrPatientEmrXml",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "EmrPatientEmrXml",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmrPatientEmrXml",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "EmrPatientEmrXml",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "EmrPatientEmrXml",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmrPatientEmrXmlHistory",
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
                    table.PrimaryKey("PK_EmrPatientEmrXmlHistory", x => x.Id);
                },
                comment: "电子病历留痕实体");

            migrationBuilder.CreateIndex(
                name: "IX_EmrPatientEmrXmlHistory_XmlCategory",
                table: "EmrPatientEmrXmlHistory",
                column: "XmlCategory");

            migrationBuilder.CreateIndex(
                name: "IX_EmrPatientEmrXmlHistory_XmlId",
                table: "EmrPatientEmrXmlHistory",
                column: "XmlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrPatientEmrXmlHistory");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "EmrPatientEmrXml");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "EmrPatientEmrXml");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "EmrPatientEmrXml");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "EmrPatientEmrXml");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "EmrPatientEmrXml");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "EmrPatientEmrXml");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmrPatientEmrXml");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "EmrPatientEmrXml");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "EmrPatientEmrXml");

            migrationBuilder.AlterTable(
                name: "EmrXmlTemplate",
                oldComment: "xml病历模板");

            migrationBuilder.AlterTable(
                name: "EmrTemplateCatalogue",
                oldComment: "模板目录结构");

            migrationBuilder.AlterTable(
                name: "EmrCatalogue",
                oldComment: "电子病历库目录树");
        }
    }
}
