using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamNote_TableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ExamNotes",
                table: "Dict_ExamNotes");

            migrationBuilder.RenameTable(
                name: "Dict_ExamNotes",
                newName: "Dict_ExamNote");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamNotes_NoteCode",
                table: "Dict_ExamNote",
                newName: "IX_Dict_ExamNote_NoteCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ExamNote",
                table: "Dict_ExamNote",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ExamNote",
                table: "Dict_ExamNote");

            migrationBuilder.RenameTable(
                name: "Dict_ExamNote",
                newName: "Dict_ExamNotes");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamNote_NoteCode",
                table: "Dict_ExamNotes",
                newName: "IX_Dict_ExamNotes_NoteCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ExamNotes",
                table: "Dict_ExamNotes",
                column: "Id");
        }
    }
}
