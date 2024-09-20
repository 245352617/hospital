using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210modifyseparationcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Dict_Separation");

            migrationBuilder.AlterTable(
                name: "Dict_Separation",
                comment: "分方途径分类实体",
                oldComment: "分方配置");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Dict_Separation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "分方单分类编码，0=注射单，1=输液单，2=雾化单...");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Dict_Separation");

            migrationBuilder.AlterTable(
                name: "Dict_Separation",
                comment: "分方配置",
                oldComment: "分方途径分类实体");

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "Dict_Separation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
