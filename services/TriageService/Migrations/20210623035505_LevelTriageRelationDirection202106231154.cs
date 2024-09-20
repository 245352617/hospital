using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class LevelTriageRelationDirection202106231154 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LevelTriageDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊去向级别代码",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true,
                oldComment: "分诊去向级别代码");

            migrationBuilder.AddColumn<string>(
                name: "TriageLevelCode",
                table: "Dict_LevelTriageRelationDirection",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊级别代码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriageLevelCode",
                table: "Dict_LevelTriageRelationDirection");

            migrationBuilder.AlterColumn<string>(
                name: "LevelTriageDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                type: "varchar(20)",
                nullable: true,
                comment: "分诊去向级别代码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊去向级别代码");
        }
    }
}
