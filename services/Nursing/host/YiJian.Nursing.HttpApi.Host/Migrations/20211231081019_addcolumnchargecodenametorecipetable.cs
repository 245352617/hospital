using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class addcolumnchargecodenametorecipetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChargeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "收费类型编码");

            migrationBuilder.AddColumn<string>(
                name: "ChargeName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "收费类型名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ChargeName",
                table: "NursingRecipe");
        }
    }
}
