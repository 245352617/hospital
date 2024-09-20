using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations
{
    public partial class v210_Update_UserSetting_UserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RC_UserSetting_UserId",
                table: "RC_UserSetting");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RC_UserSetting");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "RC_UserSetting",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                comment: "用户名称");

            migrationBuilder.CreateIndex(
                name: "IX_RC_UserSetting_UserName",
                table: "RC_UserSetting",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RC_UserSetting_UserName",
                table: "RC_UserSetting");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "RC_UserSetting");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "RC_UserSetting",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "用户Id");

            migrationBuilder.CreateIndex(
                name: "IX_RC_UserSetting_UserId",
                table: "RC_UserSetting",
                column: "UserId");
        }
    }
}
