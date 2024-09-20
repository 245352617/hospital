using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class add_ReserveDosage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReserveDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "备用量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReserveDosage",
                table: "NursingRecipeExec");
        }
    }
}
