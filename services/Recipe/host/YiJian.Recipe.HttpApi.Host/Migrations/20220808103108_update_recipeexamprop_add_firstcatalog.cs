using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class update_recipeexamprop_add_firstcatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstCatalogCode",
                table: "RC_ProjectExamProp",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "一级目录编码");

            migrationBuilder.AddColumn<string>(
                name: "FirstCatalogName",
                table: "RC_ProjectExamProp",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "一级分类名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstCatalogCode",
                table: "RC_ProjectExamProp");

            migrationBuilder.DropColumn(
                name: "FirstCatalogName",
                table: "RC_ProjectExamProp");
        }
    }
}
