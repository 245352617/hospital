using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class update_firstcatalogcode_norequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirstCatalogName",
                table: "RC_ProjectExamProp",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "一级分类名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "一级分类名称");

            migrationBuilder.AlterColumn<string>(
                name: "FirstCatalogCode",
                table: "RC_ProjectExamProp",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "一级目录编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "一级目录编码");

            migrationBuilder.AlterColumn<string>(
                name: "FirstCatalogName",
                table: "RC_Pacs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "一级检查目录名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "一级检查目录名称");

            migrationBuilder.AlterColumn<string>(
                name: "FirstCatalogCode",
                table: "RC_Pacs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "一级检查类别编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "一级检查类别编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirstCatalogName",
                table: "RC_ProjectExamProp",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "一级分类名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "一级分类名称");

            migrationBuilder.AlterColumn<string>(
                name: "FirstCatalogCode",
                table: "RC_ProjectExamProp",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "一级目录编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "一级目录编码");

            migrationBuilder.AlterColumn<string>(
                name: "FirstCatalogName",
                table: "RC_Pacs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "一级检查目录名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "一级检查目录名称");

            migrationBuilder.AlterColumn<string>(
                name: "FirstCatalogCode",
                table: "RC_Pacs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "一级检查类别编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "一级检查类别编码");
        }
    }
}
