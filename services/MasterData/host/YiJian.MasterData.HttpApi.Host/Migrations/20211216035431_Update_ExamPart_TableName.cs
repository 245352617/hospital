using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamPart_TableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ExamParts",
                table: "Dict_ExamParts");

            migrationBuilder.RenameTable(
                name: "Dict_ExamParts",
                newName: "Dict_ExamPart");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamParts_PartCode",
                table: "Dict_ExamPart",
                newName: "IX_Dict_ExamPart_PartCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ExamPart",
                table: "Dict_ExamPart",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ExamPart",
                table: "Dict_ExamPart");

            migrationBuilder.RenameTable(
                name: "Dict_ExamPart",
                newName: "Dict_ExamParts");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamPart_PartCode",
                table: "Dict_ExamParts",
                newName: "IX_Dict_ExamParts_PartCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ExamParts",
                table: "Dict_ExamParts",
                column: "Id");
        }
    }
}
