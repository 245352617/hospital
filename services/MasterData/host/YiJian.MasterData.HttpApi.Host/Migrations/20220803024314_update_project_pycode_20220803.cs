using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_project_pycode_20220803 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_LabProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_ExamProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "拼音码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "拼音码");
        }
    }
}
