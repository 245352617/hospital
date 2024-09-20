using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v2280addmergeTemplateWhiteList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrMergeTemplateWhiteList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "模板ID"),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "模板名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrMergeTemplateWhiteList", x => x.Id);
                },
                comment: "合并病历的白名单");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrMergeTemplateWhiteList");
        }
    }
}
