using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Add_call_room_code_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConsultingRoomCode",
                table: "CallCallInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsultingRoomName",
                table: "CallCallInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultingRoomCode",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "ConsultingRoomName",
                table: "CallCallInfo");
        }
    }
}
