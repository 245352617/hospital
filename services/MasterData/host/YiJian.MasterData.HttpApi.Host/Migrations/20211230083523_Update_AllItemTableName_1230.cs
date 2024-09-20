using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_AllItemTableName_1230 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_AllItems",
                table: "Dict_AllItems");

            migrationBuilder.RenameTable(
                name: "Dict_AllItems",
                newName: "Dict_AllItem");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_AllItems_CategoryCode",
                table: "Dict_AllItem",
                newName: "IX_Dict_AllItem_CategoryCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_AllItem",
                table: "Dict_AllItem",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_AllItem",
                table: "Dict_AllItem");

            migrationBuilder.RenameTable(
                name: "Dict_AllItem",
                newName: "Dict_AllItems");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_AllItem_CategoryCode",
                table: "Dict_AllItems",
                newName: "IX_Dict_AllItems_CategoryCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_AllItems",
                table: "Dict_AllItems",
                column: "Id");
        }
    }
}
