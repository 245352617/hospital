using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations
{
    public partial class v210_Add_UserSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RC_UserSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "用户Id"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "配置名称"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "配置组"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "配置组"),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "配置值"),
                    Remark = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "备注")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_UserSetting", x => x.Id);
                },
                comment: "用户个人设置");

            migrationBuilder.CreateIndex(
                name: "IX_RC_UserSetting_UserId",
                table: "RC_UserSetting",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_UserSetting");
        }
    }
}
