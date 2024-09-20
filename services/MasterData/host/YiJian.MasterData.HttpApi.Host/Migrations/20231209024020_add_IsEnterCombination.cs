using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class add_IsEnterCombination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnterCombination",
                table: "Dict_MedicineUsage",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否回车组合");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnterCombination",
                table: "Dict_MedicineUsage");
        }
    }
}
