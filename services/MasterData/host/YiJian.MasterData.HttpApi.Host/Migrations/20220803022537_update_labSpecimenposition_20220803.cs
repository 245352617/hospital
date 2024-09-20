using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_labSpecimenposition_20220803 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecimenName",
                table: "Dict_LabSpecimenPosition",
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
                table: "Dict_LabSpecimenPosition",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "标本编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "标本编码");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "单位");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Dict_ExamProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "单位");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_ExamProject",
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
                name: "SpecimenName",
                table: "Dict_LabSpecimenPosition",
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
                table: "Dict_LabSpecimenPosition",
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

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "单位");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_LabProject",
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

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Dict_ExamProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "单位");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_ExamProject",
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
