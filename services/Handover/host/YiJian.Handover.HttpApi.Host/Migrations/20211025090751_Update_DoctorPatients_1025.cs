using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_DoctorPatients_1025 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TriageLevel",
                table: "Handover_DoctorPatients",
                newName: "TriageLevelName");

            migrationBuilder.RenameColumn(
                name: "Diagnose",
                table: "Handover_DoctorPatients",
                newName: "DiagnoseName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TriageLevelName",
                table: "Handover_DoctorPatients",
                newName: "TriageLevel");

            migrationBuilder.RenameColumn(
                name: "DiagnoseName",
                table: "Handover_DoctorPatients",
                newName: "Diagnose");
        }
    }
}
