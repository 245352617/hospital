using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addcolumnantibioticLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AntibioticLevel",
                table: "RC_ProjectMedicineProp",
                type: "int",
                nullable: true,
                comment: "抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyExecDayTimes",
                table: "RC_PackageProject",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "执行时间",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AntibioticLevel",
                table: "RC_ProjectMedicineProp");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyExecDayTimes",
                table: "RC_PackageProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行时间",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "执行时间");
        }
    }
}
