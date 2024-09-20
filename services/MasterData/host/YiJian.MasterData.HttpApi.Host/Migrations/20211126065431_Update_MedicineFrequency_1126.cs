using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_MedicineFrequency_1126 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Dict_MedicineFrequencies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "频次全称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Dict_MedicineFrequencies");
        }
    }
}
