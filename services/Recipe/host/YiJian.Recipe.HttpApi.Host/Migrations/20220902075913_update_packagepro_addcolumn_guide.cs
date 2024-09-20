using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class update_packagepro_addcolumn_guide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuideCode",
                table: "RC_ProjectLabProp",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "指引Id 关联 ExamNote表code");

            migrationBuilder.AddColumn<string>(
                name: "GuideName",
                table: "RC_ProjectLabProp",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "指引名称 关联 ExamNote表name");

            migrationBuilder.AddColumn<string>(
                name: "GuideCode",
                table: "RC_ProjectExamProp",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "指引Id 关联 ExamNote表code");

            migrationBuilder.AddColumn<string>(
                name: "GuideName",
                table: "RC_ProjectExamProp",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "指引名称 关联 ExamNote表name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuideCode",
                table: "RC_ProjectLabProp");

            migrationBuilder.DropColumn(
                name: "GuideName",
                table: "RC_ProjectLabProp");

            migrationBuilder.DropColumn(
                name: "GuideCode",
                table: "RC_ProjectExamProp");

            migrationBuilder.DropColumn(
                name: "GuideName",
                table: "RC_ProjectExamProp");
        }
    }
}
