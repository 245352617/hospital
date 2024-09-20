using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class GroupInjuryInfo20210803 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Triage_GroupInjuryInfo",
                type: "nvarchar(500)",
                nullable: true,
                comment: "概要说明",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true,
                oldComment: "概要说明");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Triage_GroupInjuryInfo",
                type: "nvarchar(100)",
                nullable: true,
                comment: "概要说明",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true,
                oldComment: "概要说明");
        }
    }
}
