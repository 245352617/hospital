using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_projectandtarget_addcolumn_catalogandprojectcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatalogAndProjectCode",
                table: "Dict_LabTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "分类编码和当前项目的编码组合");

            migrationBuilder.AddColumn<string>(
                name: "CatalogAndProjectCode",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "分类编码和当前项目的编码组合");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatalogAndProjectCode",
                table: "Dict_LabTarget");

            migrationBuilder.DropColumn(
                name: "CatalogAndProjectCode",
                table: "Dict_LabProject");
        }
    }
}
