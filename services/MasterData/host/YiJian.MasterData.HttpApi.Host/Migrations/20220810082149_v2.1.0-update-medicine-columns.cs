using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210updatemedicinecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "拼音码");
             
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "拼音码");
 
        }
    }
}
