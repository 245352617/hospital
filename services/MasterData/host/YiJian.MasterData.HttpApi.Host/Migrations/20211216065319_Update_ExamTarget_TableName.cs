using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamTarget_TableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ExamTargets",
                table: "Dict_ExamTargets");

            migrationBuilder.RenameTable(
                name: "Dict_ExamTargets",
                newName: "Dict_ExamTarget");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamTargets_TargetCode",
                table: "Dict_ExamTarget",
                newName: "IX_Dict_ExamTarget_TargetCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ExamTarget",
                table: "Dict_ExamTarget",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ExamTarget",
                table: "Dict_ExamTarget");

            migrationBuilder.RenameTable(
                name: "Dict_ExamTarget",
                newName: "Dict_ExamTargets");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamTarget_TargetCode",
                table: "Dict_ExamTargets",
                newName: "IX_Dict_ExamTargets_TargetCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ExamTargets",
                table: "Dict_ExamTargets",
                column: "Id");
        }
    }
}
