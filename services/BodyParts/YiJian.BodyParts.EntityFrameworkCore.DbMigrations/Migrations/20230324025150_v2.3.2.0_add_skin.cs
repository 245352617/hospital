using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.BodyParts.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class v2320_add_skin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CanulaRecord",
                table: "IcuNursingSkin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanulaRecord",
                table: "IcuNursingSkin");
        }
    }
}
