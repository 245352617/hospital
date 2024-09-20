using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamProject_1116 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Container",
                table: "Dict_LabProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "容器名称");

            migrationBuilder.AddColumn<string>(
                name: "ContainerCode",
                table: "Dict_LabProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "容器编码");

            migrationBuilder.AddColumn<string>(
                name: "Container",
                table: "Dict_ExamProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "容器名称");

            migrationBuilder.AddColumn<string>(
                name: "ContainerCode",
                table: "Dict_ExamProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "容器编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Container",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "ContainerCode",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "Container",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "ContainerCode",
                table: "Dict_ExamProjects");
        }
    }
}
