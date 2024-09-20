using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Treat_1118 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FrequencyCode",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "默认频次代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "默认频次代码");

            migrationBuilder.AlterColumn<string>(
                name: "FeeTypeSub",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "收费小类代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "收费小类代码");

            migrationBuilder.AlterColumn<string>(
                name: "FeeTypeMain",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "收费大类代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "收费大类代码");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptCode",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行科室代码");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDept",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "执行科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行科室");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FrequencyCode",
                table: "Dict_Treats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "默认频次代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "默认频次代码");

            migrationBuilder.AlterColumn<string>(
                name: "FeeTypeSub",
                table: "Dict_Treats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "收费小类代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "收费小类代码");

            migrationBuilder.AlterColumn<string>(
                name: "FeeTypeMain",
                table: "Dict_Treats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "收费大类代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "收费大类代码");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptCode",
                table: "Dict_Treats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行科室代码");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDept",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "执行科室");
        }
    }
}
