using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class parsePY : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PY",
                table: "Dict_AllItem",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "拼音首字母");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PY",
                table: "Dict_AllItem");
        }
    }
}
