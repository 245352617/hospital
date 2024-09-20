using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class initdb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NurseCode",
                table: "RpNursingRecord",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "操作护士");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NurseCode",
                table: "RpNursingRecord");
        }
    }
}
