using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class update_column_isadditionalitems_additionalitemstype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdditionalItems",
                table: "RC_Treat");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalItemsType",
                table: "RC_Treat",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "附加类型");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalItemsType",
                table: "RC_Treat");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdditionalItems",
                table: "RC_Treat",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否药品附加项");
        }
    }
}
