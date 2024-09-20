using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class RemoveTablesAuditProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Triage_MergeRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Triage_MergeRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间"),
                    DeleteUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "删除时间"),
                    ExtensionField1 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    HospitalCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "医院名称"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否删除"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "修改时间"),
                    ModUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改人"),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "备注"),
                    Sort = table.Column<int>(type: "int", nullable: true, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_MergeRecord", x => x.Id);
                },
                comment: "院前分诊患者档案合并记录表");
        }
    }
}
