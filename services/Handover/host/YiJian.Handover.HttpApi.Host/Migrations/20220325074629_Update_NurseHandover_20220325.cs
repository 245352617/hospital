using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_NurseHandover_20220325 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DutyNurseName",
                table: "Handover_DoctorPatients");

            migrationBuilder.AddColumn<string>(
                name: "DutyNurseName",
                table: "Handover_NurseHandovers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DutyNurseName",
                table: "Handover_NurseHandovers");

            migrationBuilder.AddColumn<string>(
                name: "DutyNurseName",
                table: "Handover_DoctorPatients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
