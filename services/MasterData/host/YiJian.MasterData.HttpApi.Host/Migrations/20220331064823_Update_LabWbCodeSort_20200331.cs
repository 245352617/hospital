using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_LabWbCodeSort_20200331 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_LabTarget",
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
                name: "WbCode",
                table: "Dict_LabSpecimen",
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
                name: "WbCode",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "拼音码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_LabTarget",
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
                name: "WbCode",
                table: "Dict_LabSpecimen",
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
                name: "WbCode",
                table: "Dict_LabProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_LabProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "拼音码");
        }
    }
}
