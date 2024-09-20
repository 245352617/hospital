using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class lisupdatecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecimenName",
                table: "NursingLis",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "标本名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "标本名称");

            migrationBuilder.AlterColumn<string>(
                name: "SpecimenCode",
                table: "NursingLis",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "标本编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "标本编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecimenName",
                table: "NursingLis",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "标本名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "标本名称");

            migrationBuilder.AlterColumn<string>(
                name: "SpecimenCode",
                table: "NursingLis",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "标本编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "标本编码");
        }
    }
}
