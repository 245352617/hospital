using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Health.Report.Migrations
{
    public partial class v2292_update_IntakeSetting_add_default : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultColor",
                table: "RpIntakeSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "颜色");

            migrationBuilder.AddColumn<string>(
                name: "DefaultInputMode",
                table: "RpIntakeSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "方式");

            migrationBuilder.AddColumn<string>(
                name: "DefaultTraits",
                table: "RpIntakeSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "性状");

            migrationBuilder.AddColumn<string>(
                name: "DefaultUnit",
                table: "RpIntakeSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "单位");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultColor",
                table: "RpIntakeSetting");

            migrationBuilder.DropColumn(
                name: "DefaultInputMode",
                table: "RpIntakeSetting");

            migrationBuilder.DropColumn(
                name: "DefaultTraits",
                table: "RpIntakeSetting");

            migrationBuilder.DropColumn(
                name: "DefaultUnit",
                table: "RpIntakeSetting");
        }
    }
}
