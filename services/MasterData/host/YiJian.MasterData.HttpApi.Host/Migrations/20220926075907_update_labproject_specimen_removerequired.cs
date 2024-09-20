using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_labproject_specimen_removerequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecimenName",
                table: "Dict_LabProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "标本名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "标本名称");

            migrationBuilder.AlterColumn<string>(
                name: "SpecimenCode",
                table: "Dict_LabProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "标本编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "标本编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecimenName",
                table: "Dict_LabProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "标本名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "标本名称");

            migrationBuilder.AlterColumn<string>(
                name: "SpecimenCode",
                table: "Dict_LabProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "标本编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "标本编码");
        }
    }
}
