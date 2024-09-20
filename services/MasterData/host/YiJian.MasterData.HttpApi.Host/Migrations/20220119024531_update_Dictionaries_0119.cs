using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_Dictionaries_0119 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Dict_Dictionaries",
                type: "bit",
                nullable: false,
                comment: "使用状态",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "Dict_Dictionaries",
                type: "int",
                nullable: false,
                comment: "排序",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Dict_Dictionaries",
                type: "nvarchar(max)",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Py",
                table: "Dict_Dictionaries",
                type: "nvarchar(max)",
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DictionariesTypeName",
                table: "Dict_Dictionaries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "字典类型名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DictionariesTypeCode",
                table: "Dict_Dictionaries",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "字典类型编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "DictionariesName",
                table: "Dict_Dictionaries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "字典名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "DictionariesCode",
                table: "Dict_Dictionaries",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "字典编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaltChecked",
                table: "Dict_Dictionaries",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "默认选中");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaltChecked",
                table: "Dict_Dictionaries");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Dict_Dictionaries",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "使用状态");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "Dict_Dictionaries",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Dict_Dictionaries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "Py",
                table: "Dict_Dictionaries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "DictionariesTypeName",
                table: "Dict_Dictionaries",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "字典类型名称");

            migrationBuilder.AlterColumn<string>(
                name: "DictionariesTypeCode",
                table: "Dict_Dictionaries",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "字典类型编码");

            migrationBuilder.AlterColumn<string>(
                name: "DictionariesName",
                table: "Dict_Dictionaries",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "字典名称");

            migrationBuilder.AlterColumn<string>(
                name: "DictionariesCode",
                table: "Dict_Dictionaries",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "字典编码");
        }
    }
}
