using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v2280_0216_add_log_feild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperationType",
                table: "RC_AuditLog",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: " 操作类型，0=请求，1=返回");

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "RC_AuditLog",
                type: "ntext",
                nullable: true,
                comment: "返回数据");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationType",
                table: "RC_AuditLog");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "RC_AuditLog");
        }
    }
}
