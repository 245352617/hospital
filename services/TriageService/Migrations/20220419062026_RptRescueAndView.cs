using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class RptRescueAndView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rpt_RescueAndView",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AreaCode = table.Column<string>(nullable: true, comment: "区域"),
                    AreaName = table.Column<string>(nullable: true, comment: "区域"),
                    ItemName = table.Column<string>(maxLength: 60, nullable: true, comment: "项目"),
                    TriageDate = table.Column<DateTime>(nullable: false, comment: "分诊日期"),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Count = table.Column<int>(nullable: false, comment: "系统统计人数"),
                    CountChanged = table.Column<int>(nullable: true, comment: "手动统计人数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rpt_RescueAndView", x => x.Id);
                },
                comment: "抢救区、留观区统计");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rpt_RescueAndView");
        }
    }
}
