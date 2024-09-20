using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dict_ExamNotes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "注意事项名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "注意事项名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dict_ExamNotes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "注意事项名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "注意事项名称");
        }
    }
}
