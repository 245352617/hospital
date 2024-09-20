using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addispush : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPush",
                table: "RC_OwnMedicine",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "标记是否已经推送过,默认是否");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPush",
                table: "RC_OwnMedicine");
        }
    }
}
