using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_treat_addcolumn_additional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Additional",
                table: "Dict_Treat",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "加收标志");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Additional",
                table: "Dict_Treat");
        }
    }
}
