using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class AddIsShowFieldToCallInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "CallCallInfo",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否显示");

            migrationBuilder.AddColumn<string>(
                name: "ToHospitalWay",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "来院方式");

            migrationBuilder.AddColumn<string>(
                name: "TriageDirection",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "分诊去向编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "ToHospitalWay",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "TriageDirection",
                table: "CallCallInfo");
        }
    }
}
