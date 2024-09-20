using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210addcolumnsitemtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "Dict_Separation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Dict_Separation");
        }
    }
}
