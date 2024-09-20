using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_Medicine_120417 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExecDept",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行科室");

            migrationBuilder.AddColumn<string>(
                name: "ExecDeptCode",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecDeptCode",
                table: "Dict_Medicines");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDept",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行科室");
        }
    }
}
