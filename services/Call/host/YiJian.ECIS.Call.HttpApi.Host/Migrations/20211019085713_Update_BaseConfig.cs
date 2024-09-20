using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Update_BaseConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateNoMinute",
                table: "CallBaseConfig",
                newName: "TomorrowUpdateNoMinute");

            migrationBuilder.RenameColumn(
                name: "UpdateNoHour",
                table: "CallBaseConfig",
                newName: "TomorrowUpdateNoHour");

            migrationBuilder.RenameColumn(
                name: "CallMode",
                table: "CallBaseConfig",
                newName: "TomorrowCallMode");

            migrationBuilder.AddColumn<string>(
                name: "FriendlyReminder",
                table: "CallBaseConfig",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "温馨提醒（大屏叫号端）");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendlyReminder",
                table: "CallBaseConfig");

            migrationBuilder.RenameColumn(
                name: "TomorrowUpdateNoMinute",
                table: "CallBaseConfig",
                newName: "UpdateNoMinute");

            migrationBuilder.RenameColumn(
                name: "TomorrowUpdateNoHour",
                table: "CallBaseConfig",
                newName: "UpdateNoHour");

            migrationBuilder.RenameColumn(
                name: "TomorrowCallMode",
                table: "CallBaseConfig",
                newName: "CallMode");
        }
    }
}
