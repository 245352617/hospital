using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_Medicine_120416 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultFrequency",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "DefaultUsage",
                table: "Dict_Medicines");

            migrationBuilder.AddColumn<string>(
                name: "FrequencyCode",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "默认频次编码");

            migrationBuilder.AddColumn<string>(
                name: "FrequencyName",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "默认频次名称");

            migrationBuilder.AddColumn<string>(
                name: "UsageCode",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "默认用法编码");

            migrationBuilder.AddColumn<string>(
                name: "UsageName",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "默认用法名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrequencyCode",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "FrequencyName",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "UsageCode",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "UsageName",
                table: "Dict_Medicines");

            migrationBuilder.AddColumn<string>(
                name: "DefaultFrequency",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "默认频次");

            migrationBuilder.AddColumn<string>(
                name: "DefaultUsage",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "默认用法");
        }
    }
}
