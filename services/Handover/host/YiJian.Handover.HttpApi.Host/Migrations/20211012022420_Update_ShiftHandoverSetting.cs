using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_ShiftHandoverSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationName",
                table: "Handover_ShiftHandoverSetting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificationName",
                table: "Handover_ShiftHandoverSetting",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationName",
                table: "Handover_ShiftHandoverSetting");

            migrationBuilder.DropColumn(
                name: "ModificationName",
                table: "Handover_ShiftHandoverSetting");
        }
    }
}
