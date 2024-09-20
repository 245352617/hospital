using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamPart_1216 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IndexNo",
                table: "Dict_ExamParts",
                newName: "Sort");

            migrationBuilder.AlterColumn<string>(
                name: "PartCode",
                table: "Dict_ExamParts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "检查部位编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "检查部位编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "Dict_ExamParts",
                newName: "IndexNo");

            migrationBuilder.AlterColumn<string>(
                name: "PartCode",
                table: "Dict_ExamParts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "检查部位编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "检查部位编码");
        }
    }
}
