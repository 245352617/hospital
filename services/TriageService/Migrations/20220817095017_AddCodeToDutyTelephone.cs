using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddCodeToDutyTelephone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Dept",
                table: "Sms_DutyTelephone",
                maxLength: 50,
                nullable: true,
                comment: "科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "科室");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Sms_DutyTelephone",
                maxLength: 50,
                nullable: true,
                comment: "科室编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Sms_DutyTelephone");

            migrationBuilder.AlterColumn<string>(
                name: "Dept",
                table: "Sms_DutyTelephone",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "科室",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "科室名称");
        }
    }
}
