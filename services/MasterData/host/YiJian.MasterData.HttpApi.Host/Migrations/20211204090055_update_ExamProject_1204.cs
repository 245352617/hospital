using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_ExamProject_1204 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeptCode",
                table: "Dict_LabProjects",
                newName: "ExecDeptCode");

            migrationBuilder.RenameColumn(
                name: "Dept",
                table: "Dict_LabProjects",
                newName: "ExecDept");

            migrationBuilder.RenameColumn(
                name: "DeptCode",
                table: "Dict_ExamProjects",
                newName: "ExecDeptCode");

            migrationBuilder.RenameColumn(
                name: "Dept",
                table: "Dict_ExamProjects",
                newName: "ExecDept");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExecDeptCode",
                table: "Dict_LabProjects",
                newName: "DeptCode");

            migrationBuilder.RenameColumn(
                name: "ExecDept",
                table: "Dict_LabProjects",
                newName: "Dept");

            migrationBuilder.RenameColumn(
                name: "ExecDeptCode",
                table: "Dict_ExamProjects",
                newName: "DeptCode");

            migrationBuilder.RenameColumn(
                name: "ExecDept",
                table: "Dict_ExamProjects",
                newName: "Dept");
        }
    }
}
