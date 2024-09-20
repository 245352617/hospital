using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class add_ProjectType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "NursingTreat",
                type: "nvarchar(max)",
                nullable: true,
                comment: "项目类型名称");

            migrationBuilder.AddColumn<string>(
                name: "ProjectType",
                table: "NursingTreat",
                type: "nvarchar(max)",
                nullable: true,
                comment: "项目类型");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "NursingTreat");

            migrationBuilder.DropColumn(
                name: "ProjectType",
                table: "NursingTreat");
        }
    }
}
