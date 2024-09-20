using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddIsEnabledForJudgment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsEnabled",
                table: "Dict_JudgmentType",
                nullable: false,
                defaultValue: 0,
                comment: "是否启用; 0：不启用； 1：启用");

            migrationBuilder.AddColumn<int>(
                name: "IsEnabled",
                table: "Dict_JudgmentMaster",
                nullable: false,
                defaultValue: 0,
                comment: "是否启用; 0：不启用； 1：启用");

            migrationBuilder.AlterColumn<int>(
                name: "IsGreenRoad",
                table: "Dict_JudgmentItem",
                nullable: false,
                comment: "是否属于绿色通道； 0：不属于 1：属于",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否属于绿色通道 0：不属于 1：属于");

            migrationBuilder.AddColumn<int>(
                name: "IsEnabled",
                table: "Dict_JudgmentItem",
                nullable: false,
                defaultValue: 0,
                comment: "是否启用; 0：不启用； 1：启用");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Dict_JudgmentType");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Dict_JudgmentMaster");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Dict_JudgmentItem");

            migrationBuilder.AlterColumn<bool>(
                name: "IsGreenRoad",
                table: "Dict_JudgmentItem",
                type: "bit",
                nullable: false,
                comment: "是否属于绿色通道 0：不属于 1：属于",
                oldClrType: typeof(int),
                oldComment: "是否属于绿色通道； 0：不属于 1：属于");
        }
    }
}
