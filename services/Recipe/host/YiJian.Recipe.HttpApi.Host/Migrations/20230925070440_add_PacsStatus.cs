using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class add_PacsStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PacsStatus",
                table: "RC_Pacs",
                type: "nvarchar(max)",
                nullable: true,
                comment: "检查状态");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PacsStatus",
                table: "RC_Pacs");
        }
    }
}
