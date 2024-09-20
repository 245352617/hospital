using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_examnote_updatecolumn_length : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Dict_ExamNote",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单");

            migrationBuilder.AlterColumn<string>(
                name: "DescTemplate",
                table: "Dict_ExamNote",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "检查申请描述模板",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "检查申请描述模板");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Dict_ExamNote",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单");

            migrationBuilder.AlterColumn<string>(
                name: "DescTemplate",
                table: "Dict_ExamNote",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "检查申请描述模板",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "检查申请描述模板");
        }
    }
}
