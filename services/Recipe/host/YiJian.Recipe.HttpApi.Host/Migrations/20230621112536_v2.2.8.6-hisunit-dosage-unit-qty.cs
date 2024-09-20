using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class v2286hisunitdosageunitqty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CommitHisDosageQty",
                table: "RC_QuickStartMedicine",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "急诊换算之后-每次剂量-标准单位的数量");

            migrationBuilder.AddColumn<decimal>(
                name: "HisDosageQty",
                table: "RC_QuickStartMedicine",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "每次剂量-标准单位的数量");

            migrationBuilder.AddColumn<string>(
                name: "HisDosageUnit",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "每次剂量-标准单位");

            migrationBuilder.AddColumn<string>(
                name: "HisUnit",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "剂量单位-视图里面的那个Unit单位");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommitHisDosageQty",
                table: "RC_QuickStartMedicine");

            migrationBuilder.DropColumn(
                name: "HisDosageQty",
                table: "RC_QuickStartMedicine");

            migrationBuilder.DropColumn(
                name: "HisDosageUnit",
                table: "RC_QuickStartMedicine");

            migrationBuilder.DropColumn(
                name: "HisUnit",
                table: "RC_QuickStartMedicine");
        }
    }
}
