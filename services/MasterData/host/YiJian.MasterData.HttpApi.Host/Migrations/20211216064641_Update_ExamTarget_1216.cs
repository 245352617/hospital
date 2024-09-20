using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamTarget_1216 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Dict_ExamTargets",
                newName: "TargetUnit");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dict_ExamTargets",
                newName: "TargetName");

            migrationBuilder.RenameColumn(
                name: "IndexNo",
                table: "Dict_ExamTargets",
                newName: "Sort");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Dict_ExamTargets",
                newName: "TargetCode");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Dict_ExamTargets",
                newName: "Qty");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamTargets_Code",
                table: "Dict_ExamTargets",
                newName: "IX_Dict_ExamTargets_TargetCode");

            migrationBuilder.AlterColumn<int>(
                name: "InsureType",
                table: "Dict_ExamTargets",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "医保类型",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "医保类型");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetUnit",
                table: "Dict_ExamTargets",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "TargetName",
                table: "Dict_ExamTargets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TargetCode",
                table: "Dict_ExamTargets",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "Dict_ExamTargets",
                newName: "IndexNo");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "Dict_ExamTargets",
                newName: "Amount");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamTargets_TargetCode",
                table: "Dict_ExamTargets",
                newName: "IX_Dict_ExamTargets_Code");

            migrationBuilder.AlterColumn<string>(
                name: "InsureType",
                table: "Dict_ExamTargets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医保类型",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldComment: "医保类型");
        }
    }
}
