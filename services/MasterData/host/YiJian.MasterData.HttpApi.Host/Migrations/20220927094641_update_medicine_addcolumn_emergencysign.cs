using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_medicine_addcolumn_emergencysign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmergencySign",
                table: "Dict_Medicine",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "（急诊处方标志）1.急诊 0.普通");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmergencySign",
                table: "Dict_Medicine");
        }
    }
}
