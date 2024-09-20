using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addcolumnscommiitSerno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommitSerialNo",
                table: "RC_DoctorsAdvice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "提交序列号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommitSerialNo",
                table: "RC_DoctorsAdvice");
        }
    }
}
