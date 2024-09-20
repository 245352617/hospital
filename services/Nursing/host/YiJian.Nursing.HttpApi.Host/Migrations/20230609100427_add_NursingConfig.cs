using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace YiJian.Nursing.Migrations
{
    public partial class add_NursingConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Duct_NursingEvent");

            migrationBuilder.CreateTable(
                name: "NursingNursingConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "键名"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "键值"),
                    Extra = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "额外信息"),
                    NurseCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "用户名（对指定用户生效）"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingNursingConfig", x => x.Id);
                },
                comment: "护士站通用配置表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingNursingConfig");

            migrationBuilder.CreateTable(
                name: "Duct_NursingEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditNurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "审核人"),
                    AuditNurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "审核人名称"),
                    AuditState = table.Column<int>(type: "int", nullable: true, comment: "审核状态（0-未审核，1，已审核，2-取消审核）"),
                    AuditTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "审核时间"),
                    Context = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "内容"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护士工号"),
                    NurseDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理日期"),
                    NurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护士名称"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理时间"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者id"),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "记录时间"),
                    SignatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "签名记录编号对应icu_signature的id"),
                    Sort = table.Column<int>(type: "int", nullable: true, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_NursingEvent", x => x.Id);
                },
                comment: "表:护理记录");
        }
    }
}
