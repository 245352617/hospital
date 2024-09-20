using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class update_pacs_add_firstcatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstCatalogCode",
                table: "RC_Pacs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "一级检查类别编码");

            migrationBuilder.AddColumn<string>(
                name: "FirstCatalogName",
                table: "RC_Pacs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "一级检查目录名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstCatalogCode",
                table: "RC_Pacs");

            migrationBuilder.DropColumn(
                name: "FirstCatalogName",
                table: "RC_Pacs");
        }
    }
}
