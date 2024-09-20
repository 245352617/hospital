using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamNote_1216 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dept",
                table: "Dict_ExamNotes");

            migrationBuilder.DropColumn(
                name: "DeptCode",
                table: "Dict_ExamNotes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dict_ExamNotes",
                newName: "NoteName");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Dict_ExamNotes",
                newName: "NoteCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamNotes_Code",
                table: "Dict_ExamNotes",
                newName: "IX_Dict_ExamNotes_NoteCode");

            migrationBuilder.AddColumn<string>(
                name: "ExecDeptCode",
                table: "Dict_ExamNotes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室编码");

            migrationBuilder.AddColumn<string>(
                name: "ExecDeptName",
                table: "Dict_ExamNotes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecDeptCode",
                table: "Dict_ExamNotes");

            migrationBuilder.DropColumn(
                name: "ExecDeptName",
                table: "Dict_ExamNotes");

            migrationBuilder.RenameColumn(
                name: "NoteName",
                table: "Dict_ExamNotes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NoteCode",
                table: "Dict_ExamNotes",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamNotes_NoteCode",
                table: "Dict_ExamNotes",
                newName: "IX_Dict_ExamNotes_Code");

            migrationBuilder.AddColumn<string>(
                name: "Dept",
                table: "Dict_ExamNotes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "科室");

            migrationBuilder.AddColumn<string>(
                name: "DeptCode",
                table: "Dict_ExamNotes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "科室编码");
        }
    }
}
