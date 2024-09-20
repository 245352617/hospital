using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Nursing.Migrations
{
    public partial class add_CanulaRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CanulaRecord",
                table: "Duct_NursingCanula",
                type: "nvarchar(max)",
                nullable: true,
                comment: "导管操作记录");

            migrationBuilder.AddColumn<string>(
                name: "CanulaRecord",
                table: "Duct_Canula",
                type: "nvarchar(max)",
                nullable: true,
                comment: "导管操作记录");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanulaRecord",
                table: "Duct_NursingCanula");

            migrationBuilder.DropColumn(
                name: "CanulaRecord",
                table: "Duct_Canula");
        }
    }
}
