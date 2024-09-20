using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Medicine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "单位");

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "Dict_Treats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "规格",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "规格");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dict_Treats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "编码");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Medicines",
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
                table: "Dict_Treats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Dict_Treats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "单位");

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "规格",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "规格");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "编码");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Medicines",
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
