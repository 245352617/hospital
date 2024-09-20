using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Medicine_20220510 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InsuranceName",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保类型名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuranceName",
                table: "Dict_Medicine");
        }
    }
}
