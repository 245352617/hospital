using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class Add_Item_GuideCatelogNameAndExamTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuideCatelogName",
                table: "RC_ProjectLabProp",
                type: "nvarchar(max)",
                nullable: true,
                comment: "指引单大类");

            migrationBuilder.AddColumn<string>(
                name: "ExamTitle",
                table: "RC_ProjectExamProp",
                type: "nvarchar(max)",
                nullable: true,
                comment: "检查单单名标题");

            migrationBuilder.AddColumn<string>(
                name: "ExamTitle",
                table: "RC_Pacs",
                type: "nvarchar(max)",
                nullable: true,
                comment: "检查单单名标题");

            migrationBuilder.AddColumn<string>(
                name: "GuideCatelogName",
                table: "RC_Lis",
                type: "nvarchar(max)",
                nullable: true,
                comment: "指引单大类");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuideCatelogName",
                table: "RC_ProjectLabProp");

            migrationBuilder.DropColumn(
                name: "ExamTitle",
                table: "RC_ProjectExamProp");

            migrationBuilder.DropColumn(
                name: "ExamTitle",
                table: "RC_Pacs");

            migrationBuilder.DropColumn(
                name: "GuideCatelogName",
                table: "RC_Lis");
        }
    }
}
