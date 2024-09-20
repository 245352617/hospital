using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Longgang4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SocietyRelationCode",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "与联系人关系编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "与联系人关系编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SocietyRelationCode",
                table: "Triage_PatientInfo",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "与联系人关系编码",
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "与联系人关系编码");
        }
    }
}
