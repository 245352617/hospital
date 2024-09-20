using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210_Update_DictionariesMultitype_Sort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Dict_DictionariesMultitype",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "排序");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Dict_DictionariesMultitype");
        }
    }
}
