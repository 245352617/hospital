using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class AddPackagePosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionCode",
                table: "RC_PackageProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "位置编码");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "RC_PackageProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "位置");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionCode",
                table: "RC_PackageProject");

            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "RC_PackageProject");
        }
    }
}
