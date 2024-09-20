using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_examnote_updatecolumn_namelength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "Dict_ExamNote",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "执行科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行科室名称");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Dict_ExamNote",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单");

            migrationBuilder.AlterColumn<string>(
                name: "DescTemplate",
                table: "Dict_ExamNote",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                comment: "检查申请描述模板",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "检查申请描述模板");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "Dict_ExamNote",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "执行科室名称");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Dict_ExamNote",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldComment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单");

            migrationBuilder.AlterColumn<string>(
                name: "DescTemplate",
                table: "Dict_ExamNote",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "检查申请描述模板",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldComment: "检查申请描述模板");
        }
    }
}
