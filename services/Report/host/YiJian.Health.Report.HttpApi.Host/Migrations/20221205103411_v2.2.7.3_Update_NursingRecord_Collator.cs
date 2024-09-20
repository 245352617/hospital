using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2273_Update_NursingRecord_Collator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Collator",
                table: "RpNursingRecord",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "核对人名称");

            migrationBuilder.AddColumn<string>(
                name: "CollatorCode",
                table: "RpNursingRecord",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "核对人code");

            migrationBuilder.AddColumn<string>(
                name: "CollatorImage",
                table: "RpNursingRecord",
                type: "nvarchar(max)",
                nullable: true,
                comment: "核对人签名图片");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collator",
                table: "RpNursingRecord");

            migrationBuilder.DropColumn(
                name: "CollatorCode",
                table: "RpNursingRecord");

            migrationBuilder.DropColumn(
                name: "CollatorImage",
                table: "RpNursingRecord");
        }
    }
}
