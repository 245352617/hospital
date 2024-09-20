using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddReportSettingQueryOptionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportQueryItem",
                table: "Dict_ReportSetting");

            migrationBuilder.CreateTable(
                name: "Dict_ReportSettingQueryOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReportSettingId = table.Column<Guid>(nullable: false),
                    QueryName = table.Column<string>(nullable: true, comment: "查询名称"),
                    QueryFiled = table.Column<string>(nullable: true, comment: "查询字段"),
                    QueryType = table.Column<string>(nullable: true, comment: "查询类型"),
                    DataSourceSql = table.Column<string>(nullable: true, comment: "数据源：SQL"),
                    DisplayName = table.Column<string>(nullable: true, comment: "显示名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ReportSettingQueryOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dict_ReportSettingQueryOption_Dict_ReportSetting_ReportSettingId",
                        column: x => x.ReportSettingId,
                        principalTable: "Dict_ReportSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "分诊报表查询选项");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ReportSettingQueryOption_ReportSettingId",
                table: "Dict_ReportSettingQueryOption",
                column: "ReportSettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ReportSettingQueryOption");

            migrationBuilder.AddColumn<string>(
                name: "ReportQueryItem",
                table: "Dict_ReportSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "报表查询条件");
        }
    }
}
