using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Medicine_1227 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEmergency",
                table: "Dict_Medicines",
                newName: "IsFirstAid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFirstAid",
                table: "Dict_Medicines",
                newName: "IsEmergency");
        }
    }
}
