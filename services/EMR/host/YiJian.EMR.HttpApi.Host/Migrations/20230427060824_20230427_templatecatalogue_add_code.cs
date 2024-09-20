using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.EMR.Migrations
{
    public partial class _20230427_templatecatalogue_add_code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "EmrTemplateCatalogue",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "目录名称编码");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateName",
                table: "EmrMergeTemplateWhiteList",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "模板名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "模板名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "EmrTemplateCatalogue");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateName",
                table: "EmrMergeTemplateWhiteList",
                type: "nvarchar(max)",
                nullable: true,
                comment: "模板名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "模板名称");
        }
    }
}
