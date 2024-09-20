using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_MedicineUsage_20220415 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_MedicineUsage",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "五笔码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "五笔码");

            migrationBuilder.AlterColumn<string>(
                name: "UsageName",
                table: "Dict_MedicineUsage",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_MedicineUsage",
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
                table: "Dict_MedicineUsage",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "五笔码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "五笔码");

            migrationBuilder.AlterColumn<string>(
                name: "UsageName",
                table: "Dict_MedicineUsage",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_MedicineUsage",
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
