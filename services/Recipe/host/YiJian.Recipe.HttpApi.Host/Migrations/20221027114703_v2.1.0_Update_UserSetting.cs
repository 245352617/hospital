using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations
{
    public partial class v210_Update_UserSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "RC_UserSetting",
                type: "int",
                nullable: false,
                comment: "配置类型",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "配置组");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "RC_UserSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "配置编码");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RC_UserSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "RC_UserSetting",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupCode",
                table: "RC_UserSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "配置组编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "RC_UserSetting");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RC_UserSetting");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "RC_UserSetting");

            migrationBuilder.DropColumn(
                name: "GroupCode",
                table: "RC_UserSetting");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "RC_UserSetting",
                type: "int",
                nullable: false,
                comment: "配置组",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "配置类型");
        }
    }
}
