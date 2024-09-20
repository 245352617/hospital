using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Treat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "规格",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
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
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "Dict_Treats",
                type: "nvarchar(20)",
                maxLength: 20,
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
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Dict_Treats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "编码");
        }
    }
}
