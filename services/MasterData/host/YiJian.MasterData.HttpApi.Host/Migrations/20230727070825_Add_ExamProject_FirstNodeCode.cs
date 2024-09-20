using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class Add_ExamProject_FirstNodeCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstNodeCode",
                table: "Dict_ExamProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "一级目录编码");

            migrationBuilder.AddColumn<string>(
                name: "FirstNodeName",
                table: "Dict_ExamProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "一级目录名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstNodeCode",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "FirstNodeName",
                table: "Dict_ExamProject");
        }
    }
}
