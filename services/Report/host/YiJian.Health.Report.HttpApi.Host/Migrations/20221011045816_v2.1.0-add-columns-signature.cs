using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v210addcolumnssignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nurse",
                table: "RpNursingRecord",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "护士签名",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true,
                oldComment: "护士签名");

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "RpNursingRecord",
                type: "nvarchar(max)",
                nullable: true,
                comment: "签名图片");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "RpNursingRecord");

            migrationBuilder.AlterColumn<string>(
                name: "Nurse",
                table: "RpNursingRecord",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "护士签名",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "护士签名");
        }
    }
}
