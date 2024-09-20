using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations
{
    public partial class update_novelcoronavirusrna_applytime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplyTime",
                table: "RC_NovelCoronavirusRna",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "申请时间",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "申请时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplyTime",
                table: "RC_NovelCoronavirusRna",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "申请时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "申请时间");
        }
    }
}
