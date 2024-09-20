using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class AddPackagePartName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartCode",
                table: "RC_PackageProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "检查部位编码");

            migrationBuilder.AddColumn<string>(
                name: "PartName",
                table: "RC_PackageProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "检查部位");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartCode",
                table: "RC_PackageProject");

            migrationBuilder.DropColumn(
                name: "PartName",
                table: "RC_PackageProject");
        }
    }
}
