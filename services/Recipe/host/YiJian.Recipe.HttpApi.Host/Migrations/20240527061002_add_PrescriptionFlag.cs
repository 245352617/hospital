using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class add_PrescriptionFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrescriptionFlag",
                table: "RC_DoctorsAdvice",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "分方标记");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriptionFlag",
                table: "RC_DoctorsAdvice");
        }
    }
}
