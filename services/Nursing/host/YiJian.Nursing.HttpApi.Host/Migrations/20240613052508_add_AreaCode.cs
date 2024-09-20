using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Nursing.Migrations
{
    public partial class add_AreaCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaCode",
                table: "NursingRecipe",
                type: "nvarchar(max)",
                nullable: true,
                comment: "区域编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaCode",
                table: "NursingRecipe");
        }
    }
}
