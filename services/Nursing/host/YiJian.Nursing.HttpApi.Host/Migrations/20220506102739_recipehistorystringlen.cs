using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class recipehistorystringlen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "NursingRecipeHistory",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "OperatorName",
                table: "NursingRecipeHistory",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "操作人名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "操作人名称");

            migrationBuilder.AlterColumn<string>(
                name: "OperatorCode",
                table: "NursingRecipeHistory",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "操作人编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "操作人编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "NursingRecipeHistory",
                type: "nvarchar(max)",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "OperatorName",
                table: "NursingRecipeHistory",
                type: "nvarchar(max)",
                nullable: true,
                comment: "操作人名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "操作人名称");

            migrationBuilder.AlterColumn<string>(
                name: "OperatorCode",
                table: "NursingRecipeHistory",
                type: "nvarchar(max)",
                nullable: true,
                comment: "操作人编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "操作人编码");
        }
    }
}
