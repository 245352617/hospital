using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_Medicine_1204 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BasicUnitPrice",
                table: "Dict_Medicines",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "BasicUnit",
                table: "Dict_Medicines",
                newName: "Unit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Dict_Medicines",
                newName: "BasicUnit");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Dict_Medicines",
                newName: "BasicUnitPrice");
        }
    }
}
