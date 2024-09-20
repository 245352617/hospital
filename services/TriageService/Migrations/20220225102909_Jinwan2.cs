using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Jinwan2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GuardianPhone",
                table: "Triage_PatientInfo",
                maxLength: 20,
                nullable: true,
                comment: "监护人/联系人电话",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactsAddress",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "监护人/联系人地址");

            migrationBuilder.AddColumn<string>(
                name: "SocietyRelationCode",
                table: "Triage_PatientInfo",
                maxLength: 10,
                nullable: true,
                comment: "与联系人关系编码");

            migrationBuilder.AddColumn<string>(
                name: "SocietyRelationName",
                table: "Triage_PatientInfo",
                maxLength: 10,
                nullable: true,
                comment: "与联系人关系名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactsAddress",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "SocietyRelationCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "SocietyRelationName",
                table: "Triage_PatientInfo");

            migrationBuilder.AlterColumn<string>(
                name: "GuardianPhone",
                table: "Triage_PatientInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "监护人/联系人电话");
        }
    }
}
