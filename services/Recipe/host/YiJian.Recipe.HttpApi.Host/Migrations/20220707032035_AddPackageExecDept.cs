using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class AddPackageExecDept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExecDeptCode",
                table: "RC_PackageProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室编码");

            migrationBuilder.AddColumn<string>(
                name: "ExecDeptName",
                table: "RC_PackageProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "执行科室");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecDeptCode",
                table: "RC_PackageProject");

            migrationBuilder.DropColumn(
                name: "ExecDeptName",
                table: "RC_PackageProject");
        }
    }
}
