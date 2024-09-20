using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_AllItem_1203 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TypeName",
                table: "Dict_AllItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "类型名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true,
                oldComment: "类型名称");

            migrationBuilder.AlterColumn<string>(
                name: "TypeCode",
                table: "Dict_AllItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "类型编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true,
                oldComment: "类型编码");

            migrationBuilder.AddColumn<string>(
                name: "ChargeCode",
                table: "Dict_AllItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "收费分类编码");

            migrationBuilder.AddColumn<string>(
                name: "ChargeName",
                table: "Dict_AllItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "收费分类名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeCode",
                table: "Dict_AllItems");

            migrationBuilder.DropColumn(
                name: "ChargeName",
                table: "Dict_AllItems");

            migrationBuilder.AlterColumn<string>(
                name: "TypeName",
                table: "Dict_AllItems",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "类型名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "类型名称");

            migrationBuilder.AlterColumn<string>(
                name: "TypeCode",
                table: "Dict_AllItems",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "类型编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "类型编码");
        }
    }
}
