using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class Add_ExamTree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_ExamTree",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Id"),
                    ProjectName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "名称"),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "编码"),
                    CreatTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "父级id"),
                    FullPath = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "当前节点全路径"),
                    NodeName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "当前节点全路径"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ExamTree", x => x.Id);
                },
                comment: "检查树形结构表格");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamTree_Id",
                table: "Dict_ExamTree",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ExamTree");
        }
    }
}
