 using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_MedicineUsage_1126 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Dict_MedicineUsages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "全称");

            migrationBuilder.AddColumn<bool>(
                name: "IsSingle",
                table: "Dict_MedicineUsages",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否单次");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Dict_MedicineUsages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "备注");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropColumn(
                name: "IsSingle",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Dict_MedicineUsages");
        }
    }
}
