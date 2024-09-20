using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class add_AdditionalItemsType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdditionalItemsType",
                table: "NursingTreat",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "附加类型");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalItemsType",
                table: "NursingTreat");
        }
    }
}
