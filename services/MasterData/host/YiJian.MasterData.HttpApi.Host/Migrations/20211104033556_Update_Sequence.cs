using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Sequence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sys_Sequences",
                table: "Sys_Sequences");

            migrationBuilder.RenameTable(
                name: "Sys_Sequences",
                newName: "Dict_Sequences");

            migrationBuilder.RenameIndex(
                name: "IX_Sys_Sequences_Code",
                table: "Dict_Sequences",
                newName: "IX_Dict_Sequences_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_Sequences",
                table: "Dict_Sequences",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_Sequences",
                table: "Dict_Sequences");

            migrationBuilder.RenameTable(
                name: "Dict_Sequences",
                newName: "Sys_Sequences");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_Sequences_Code",
                table: "Sys_Sequences",
                newName: "IX_Sys_Sequences_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sys_Sequences",
                table: "Sys_Sequences",
                column: "Id");
        }
    }
}
