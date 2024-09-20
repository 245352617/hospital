using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class ConsequenceInfo20210628 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangeDept",
                table: "Triage_ConsequenceInfo",
                type: "varchar(50)",
                nullable: true,
                comment: "科室变更");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeDept",
                table: "Triage_ConsequenceInfo");
        }
    }
}
