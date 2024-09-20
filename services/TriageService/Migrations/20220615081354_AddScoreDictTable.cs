using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddScoreDictTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_ScoreDict",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Category = table.Column<string>(maxLength: 50, nullable: true, comment: "评分类型"),
                    DisplayText = table.Column<string>(maxLength: 200, nullable: true, comment: "显示名称"),
                    Grade = table.Column<int>(nullable: false, comment: "分数"),
                    ParentId = table.Column<Guid>(nullable: true, comment: "父级Id"),
                    Level = table.Column<int>(nullable: false, comment: "评分标题级别"),
                    Sort = table.Column<int>(nullable: false, comment: "排序号"),
                    Remark = table.Column<string>(nullable: true, comment: "备注"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ScoreDict", x => x.Id);
                },
                comment: "评分字典");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ScoreDict");
        }
    }
}
