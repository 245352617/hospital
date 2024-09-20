using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddReportSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_ReportSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReportName = table.Column<string>(nullable: true, comment: "报表名称"),
                    ReportTypeCode = table.Column<string>(type: "varchar(20)", nullable: true, comment: "报表分类"),
                    IsEnabled = table.Column<int>(nullable: false, comment: "是否启用"),
                    ReportSql = table.Column<string>(nullable: true, comment: "查询语句"),
                    ReportHead = table.Column<string>(nullable: true, comment: "报表表头"),
                    ReportQueryItem = table.Column<string>(nullable: true, comment: "报表查询条件"),
                    ReportSortFiled = table.Column<string>(nullable: true, comment: "报表排序字段"),
                    OrderType = table.Column<int>(nullable: false, comment: "排序类型； 0：升序； 1：降序；")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ReportSetting", x => x.Id);
                },
                comment: "分诊报表设置表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ReportSetting");
        }
    }
}
