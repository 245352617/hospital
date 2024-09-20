using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210separationaddprintsettingname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PrintSettingId",
                table: "Dict_Separation",
                type: "uniqueidentifier",
                nullable: true,
                comment: "打印模板Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrintSettingName",
                table: "Dict_Separation",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "分方单名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrintSettingName",
                table: "Dict_Separation");

            migrationBuilder.AlterColumn<Guid>(
                name: "PrintSettingId",
                table: "Dict_Separation",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "打印模板Id");
        }
    }
}
