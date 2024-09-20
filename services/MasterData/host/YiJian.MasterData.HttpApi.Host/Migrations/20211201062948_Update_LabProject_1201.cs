using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_LabProject_1201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAid",
                table: "Dict_LabProjects",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否急救");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFirstAid",
                table: "Dict_ExamProjects",
                type: "bit",
                nullable: false,
                comment: "是否急救",
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstAid",
                table: "Dict_LabProjects");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFirstAid",
                table: "Dict_ExamProjects",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否急救");
        }
    }
}
