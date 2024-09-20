using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210toxicprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSyncToReciped",
                table: "Dict_Medicine",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否已经全量同步过Recipes库");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSyncToReciped",
                table: "Dict_Medicine");
        }
    }
}
