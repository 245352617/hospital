using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_DoctorHandover_1120 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HandoverDate",
                table: "Handover_DoctorHandovers",
                type: "datetime2",
                nullable: false,
                comment: "交班日期",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "交班日期");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HandoverDate",
                table: "Handover_DoctorHandovers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "交班日期",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "交班日期");
        }
    }
}
