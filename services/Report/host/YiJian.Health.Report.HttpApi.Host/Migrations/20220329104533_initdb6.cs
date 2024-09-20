using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class initdb6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "RpIntake",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "内容");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "RpIntake",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "RpIntake");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "RpIntake",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "内容");
        }
    }
}
