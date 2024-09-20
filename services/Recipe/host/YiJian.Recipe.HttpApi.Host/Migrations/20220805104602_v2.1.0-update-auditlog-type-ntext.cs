using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210updateauditlogtypentext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Request",
                table: "RC_AuditLog",
                type: "ntext",
                nullable: true,
                comment: "请求参数",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true,
                oldComment: "请求参数");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Request",
                table: "RC_AuditLog",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "请求参数",
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true,
                oldComment: "请求参数");
        }
    }
}
