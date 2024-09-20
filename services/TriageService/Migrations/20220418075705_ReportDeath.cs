using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class ReportDeath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rpt_DeathCount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 60, nullable: true, comment: "项目"),
                    TriageDate = table.Column<DateTime>(nullable: false, comment: "分诊日期"),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    DeathCount = table.Column<int>(nullable: false, comment: "系统统计人数"),
                    DeathCountChanged = table.Column<int>(nullable: true, comment: "手动修改人数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rpt_DeathCount", x => x.Id);
                },
                comment: "死亡人数统计");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rpt_DeathCount");
        }
    }
}
