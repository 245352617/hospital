using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Diagnose",
                table: "RpNursingDocument",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "诊断",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "诊断");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Diagnose",
                table: "RpNursingDocument",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "诊断",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true,
                oldComment: "诊断");
        }
    }
}
