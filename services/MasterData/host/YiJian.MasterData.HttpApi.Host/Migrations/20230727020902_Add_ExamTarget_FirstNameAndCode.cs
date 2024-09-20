using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class Add_ExamTarget_FirstNameAndCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstNodeCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "一级目录编码");

            migrationBuilder.AddColumn<string>(
                name: "FirstNodeName",
                table: "Dict_ExamTarget",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "一级目录名称");

            migrationBuilder.AddColumn<string>(
                name: "ExamTitle",
                table: "Dict_ExamProject",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                comment: "检查单单名标题");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstNodeCode",
                table: "Dict_ExamTarget");

            migrationBuilder.DropColumn(
                name: "FirstNodeName",
                table: "Dict_ExamTarget");

            migrationBuilder.DropColumn(
                name: "ExamTitle",
                table: "Dict_ExamProject");
        }
    }
}
