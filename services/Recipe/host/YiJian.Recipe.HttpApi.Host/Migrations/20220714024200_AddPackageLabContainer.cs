using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class AddPackageLabContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContainerCode",
                table: "RC_PackageProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "容器编码");

            migrationBuilder.AddColumn<string>(
                name: "ContainerName",
                table: "RC_PackageProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "容器名称");

            migrationBuilder.AddColumn<string>(
                name: "SpecimenCode",
                table: "RC_PackageProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "标本编码");

            migrationBuilder.AddColumn<string>(
                name: "SpecimenName",
                table: "RC_PackageProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "标本");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerCode",
                table: "RC_PackageProject");

            migrationBuilder.DropColumn(
                name: "ContainerName",
                table: "RC_PackageProject");

            migrationBuilder.DropColumn(
                name: "SpecimenCode",
                table: "RC_PackageProject");

            migrationBuilder.DropColumn(
                name: "SpecimenName",
                table: "RC_PackageProject");
        }
    }
}
