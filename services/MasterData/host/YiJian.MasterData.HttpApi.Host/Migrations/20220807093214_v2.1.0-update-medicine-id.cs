using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210updatemedicineid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Dict_Medicine",
                type: "int",
                nullable: true,
                comment: "覆蓋ABP的ID，设置为HIS带过来的Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Dict_Medicine",
                type: "int",
                nullable: false);

            migrationBuilder.DropPrimaryKey(
                table: "Dict_Medicine",
                name: "PK_Dict_Medicine"
            );
            migrationBuilder.AddPrimaryKey(
                table: "Dict_Medicine",
                name: "PK_Dict_Medicine",
                column: "Id"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Dict_Medicine");
            
            migrationBuilder.DropPrimaryKey(
                table: "Dict_Medicine",
                name: "PK_Dict_Medicine"
            );
            migrationBuilder.AddPrimaryKey(
                table: "Dict_Medicine",
                name: "PK_Dict_Medicine",
                column: "Id"
            );
        }
    }
}
