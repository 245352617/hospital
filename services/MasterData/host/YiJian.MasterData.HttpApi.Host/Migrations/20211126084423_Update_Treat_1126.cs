using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Treat_1126 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAid",
                table: "Dict_Treats",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否急救标识");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstAid",
                table: "Dict_Treats");
        }
    }
}
