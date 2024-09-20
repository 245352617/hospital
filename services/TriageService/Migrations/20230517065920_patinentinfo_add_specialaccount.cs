using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class patinentinfo_add_specialaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecialAccountTypeCode",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "特约记账类型编码");

            migrationBuilder.AddColumn<string>(
                name: "SpecialAccountTypeName",
                table: "Triage_PatientInfo",
                maxLength: 50,
                nullable: true,
                comment: "特约记账类型名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialAccountTypeCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "SpecialAccountTypeName",
                table: "Triage_PatientInfo");
        }
    }
}
