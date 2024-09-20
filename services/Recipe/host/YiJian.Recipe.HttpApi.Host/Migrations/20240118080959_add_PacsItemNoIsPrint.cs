using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class add_PacsItemNoIsPrint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrint",
                table: "RC_PacsPathologyItemNo",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否已经打印");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrint",
                table: "RC_PacsPathologyItemNo");
        }
    }
}
