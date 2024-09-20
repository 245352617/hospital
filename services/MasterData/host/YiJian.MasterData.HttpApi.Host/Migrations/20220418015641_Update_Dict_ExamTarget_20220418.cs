using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Dict_ExamTarget_20220418 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_LabCatalog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_LabCatalog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_LabCatalog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_LabCatalog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
