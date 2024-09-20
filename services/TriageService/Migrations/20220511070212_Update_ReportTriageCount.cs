using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Update_ReportTriageCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rpt_TriageCount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeptName = table.Column<string>(maxLength: 60, nullable: true, comment: "科室名称"),
                    TriageDate = table.Column<DateTime>(nullable: false, comment: "分诊日期"),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    TriageCount = table.Column<int>(nullable: false, comment: "系统统计人数"),
                    TriageCountChanged = table.Column<int>(nullable: true, comment: "手动修改人数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rpt_TriageCount", x => x.Id);
                },
                comment: "分诊人数统计");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rpt_TriageCount");
        }
    }
}
