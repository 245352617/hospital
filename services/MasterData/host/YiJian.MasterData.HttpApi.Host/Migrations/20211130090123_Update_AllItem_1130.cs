using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_AllItem_1130 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Charge",
                table: "Dict_AllItems",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Dict_AllItems",
                newName: "Charge");
        }
    }
}
