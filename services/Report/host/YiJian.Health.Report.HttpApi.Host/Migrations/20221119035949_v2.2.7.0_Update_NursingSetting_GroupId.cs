using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2270_Update_NursingSetting_GroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "RpNursingSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "配置组Id");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "RpNursingSetting",
                type: "nvarchar(max)",
                nullable: true,
                comment: "配置组名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "RpNursingSetting");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "RpNursingSetting");
        }
    }
}
