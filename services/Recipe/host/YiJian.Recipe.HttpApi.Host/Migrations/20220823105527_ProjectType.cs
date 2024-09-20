using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class ProjectType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "RC_ProjectTreatrop",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "诊疗处置类别名称");

            migrationBuilder.AddColumn<string>(
                name: "ProjectType",
                table: "RC_ProjectTreatrop",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "诊疗处置类别代码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "RC_ProjectTreatrop");

            migrationBuilder.DropColumn(
                name: "ProjectType",
                table: "RC_ProjectTreatrop");
        }
    }
}
