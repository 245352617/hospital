using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class uddate_treat_addcolumn_isadditionalutems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdditionalItems",
                table: "RC_Treat",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否药品附加项");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdditionalItems",
                table: "RC_Treat");
        }
    }
}
