using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Treat_20220506 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectMerge",
                table: "Dict_Treat",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "项目归类--龙岗字典所需");

            migrationBuilder.AddColumn<string>(
                name: "ProjectMerge",
                table: "Dict_LabTarget",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "项目归类--龙岗字典所需");

            migrationBuilder.AddColumn<string>(
                name: "ProjectType",
                table: "Dict_LabTarget",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "项目类型--龙岗字典所需");

            migrationBuilder.AddColumn<string>(
                name: "ProjectMerge",
                table: "Dict_ExamTarget",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "项目归类--龙岗字典所需");

            migrationBuilder.AddColumn<string>(
                name: "ProjectType",
                table: "Dict_ExamTarget",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "项目类型--龙岗字典所需");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectMerge",
                table: "Dict_Treat");

            migrationBuilder.DropColumn(
                name: "ProjectMerge",
                table: "Dict_LabTarget");

            migrationBuilder.DropColumn(
                name: "ProjectType",
                table: "Dict_LabTarget");

            migrationBuilder.DropColumn(
                name: "ProjectMerge",
                table: "Dict_ExamTarget");

            migrationBuilder.DropColumn(
                name: "ProjectType",
                table: "Dict_ExamTarget");
        }
    }
}
