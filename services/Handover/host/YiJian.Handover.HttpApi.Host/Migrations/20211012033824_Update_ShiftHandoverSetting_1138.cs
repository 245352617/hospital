using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_ShiftHandoverSetting_1138 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "Handover_ShiftHandoverSetting",
                type: "nvarchar(20)",
                nullable: false,
                comment: "开始时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "开始时间");

            migrationBuilder.AlterColumn<string>(
                name: "EndTime",
                table: "Handover_ShiftHandoverSetting",
                type: "nvarchar(20)",
                nullable: false,
                comment: "结束时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "结束时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Handover_ShiftHandoverSetting",
                type: "datetime2",
                nullable: false,
                comment: "开始时间",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldComment: "开始时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Handover_ShiftHandoverSetting",
                type: "datetime2",
                nullable: false,
                comment: "结束时间",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldComment: "结束时间");
        }
    }
}
