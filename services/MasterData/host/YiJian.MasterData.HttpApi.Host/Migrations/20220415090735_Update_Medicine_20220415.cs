using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Medicine_20220415 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "规格",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "规格");

            migrationBuilder.AlterColumn<string>(
                name: "SmallPackUnit",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "小包装单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "小包装单位");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Dict_Medicine",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "剂量单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "剂量单位");

            migrationBuilder.AlterColumn<string>(
                name: "BigPackUnit",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "大包装单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "大包装单位");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "规格",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "规格");

            migrationBuilder.AlterColumn<string>(
                name: "SmallPackUnit",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "小包装单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "小包装单位");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "剂量单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "剂量单位");

            migrationBuilder.AlterColumn<string>(
                name: "BigPackUnit",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "大包装单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "大包装单位");
        }
    }
}
