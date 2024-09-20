using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class ReportHot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rpt_HotMorningAndNight",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, comment: "日期"),
                    DeptCode = table.Column<string>(maxLength: 60, nullable: true, comment: "科室编码"),
                    DeptName = table.Column<string>(maxLength: 60, nullable: true, comment: "科室名称"),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    MorningCount = table.Column<int>(nullable: false, comment: "早八统计人数"),
                    NightCount = table.Column<int>(nullable: false, comment: "晚八统计人数"),
                    MorningCountChanged = table.Column<int>(nullable: true, comment: "早八统计人数（修改）"),
                    NightCountChanged = table.Column<int>(nullable: true, comment: "晚八统计人数（修改）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rpt_HotMorningAndNight", x => x.Id);
                },
                comment: "早八晚八发热统计");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rpt_HotMorningAndNight");
        }
    }
}
