using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_MedicineFrequency_20220402 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThirdPartyId",
                table: "Dict_MedicineFrequency",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "第三方id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThirdPartyId",
                table: "Dict_MedicineFrequency");
        }
    }
}
