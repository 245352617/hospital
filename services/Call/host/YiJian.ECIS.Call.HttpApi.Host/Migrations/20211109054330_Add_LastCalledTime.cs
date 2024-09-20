using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Add_LastCalledTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastCalledTime",
                table: "CallCallInfo",
                type: "datetime2",
                nullable: true,
                comment: "叫号时间");
            migrationBuilder.AlterColumn<DateTime>(
                name: "LogDate",
                table: "CallCallInfo",
                type: "datetime2",
                nullable: true,
                comment: "排队日期（工作日计算）",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true,
                oldComment: "排队日期（工作日计算）");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCalledTime",
                table: "CallCallInfo");
            migrationBuilder.AlterColumn<DateTime>(
                name: "LogDate",
                table: "CallCallInfo",
                type: "date",
                nullable: true,
                comment: "排队日期（工作日计算）",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "排队日期（工作日计算）");
        }
    }
}
