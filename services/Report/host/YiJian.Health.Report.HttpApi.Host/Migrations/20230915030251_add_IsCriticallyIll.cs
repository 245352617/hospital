using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Health.Report.Migrations
{
    public partial class add_IsCriticallyIll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCriticallyIll",
                table: "RpNursingDocument",
                type: "bit",
                nullable: true,
                comment: "病危");

            migrationBuilder.AddColumn<bool>(
                name: "IsSeriouslyIll",
                table: "RpNursingDocument",
                type: "bit",
                nullable: true,
                comment: "病重");

            migrationBuilder.AddColumn<DateTime>(
                name: "SeriouslyTime",
                table: "RpNursingDocument",
                type: "datetime2",
                nullable: true,
                comment: "病危病重时间");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "RpIntake",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "内容");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCriticallyIll",
                table: "RpNursingDocument");

            migrationBuilder.DropColumn(
                name: "IsSeriouslyIll",
                table: "RpNursingDocument");

            migrationBuilder.DropColumn(
                name: "SeriouslyTime",
                table: "RpNursingDocument");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "RpIntake",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "内容");
        }
    }
}
