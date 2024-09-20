using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class update_paitentInfo_addfield_extendfield1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtendField1",
                table: "Triage_PatientInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNum",
                table: "Triage_PatientInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtendField1",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "InvoiceNum",
                table: "Triage_PatientInfo");
        }
    }
}
