using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class modifyEmrMergeTemplateWhiteList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
