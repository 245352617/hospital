using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Update_CallInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityTypeName",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "患者身份");

            migrationBuilder.AddColumn<string>(
                name: "SexName",
                table: "CallCallInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "患者性别");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityTypeName",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "SexName",
                table: "CallCallInfo");
        }
    }
}
