using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Update_CallInfo4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPause",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "VisitStatus",
                table: "CallCallInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPause",
                table: "CallCallInfo",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否暂停");

            migrationBuilder.AddColumn<int>(
                name: "VisitStatus",
                table: "CallCallInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "就诊状态");
        }
    }
}
