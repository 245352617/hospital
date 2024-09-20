using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addtableauditlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RC_AuditLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "方法名称"),
                    HttpMethods = table.Column<int>(type: "int", nullable: false, comment: "方法类型  GET = 0, POST = 1, PUT = 2,DELETE = 3,HEAD = 4,OPTIONS = 5, PATCH = 6"),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "请求的HTTP URL"),
                    Request = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "请求参数"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_AuditLog", x => x.Id);
                },
                comment: "HIS接口审计日志");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_AuditLog");
        }
    }
}
