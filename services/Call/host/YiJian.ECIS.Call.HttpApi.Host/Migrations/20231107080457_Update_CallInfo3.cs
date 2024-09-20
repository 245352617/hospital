using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Update_CallInfo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPause",
                table: "CallCallInfo",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否暂停");

            migrationBuilder.AddColumn<bool>(
                name: "IsReport",
                table: "CallCallInfo",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否是查看报告");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPause",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "IsReport",
                table: "CallCallInfo");
        }
    }
}
