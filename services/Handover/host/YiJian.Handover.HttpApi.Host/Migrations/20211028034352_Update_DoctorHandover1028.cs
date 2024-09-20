using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_DoctorHandover1028 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Handover_DoctorHandovers_HandoverDate",
                table: "Handover_DoctorHandovers",
                column: "HandoverDate");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_DoctorHandovers_HandoverDoctorCode",
                table: "Handover_DoctorHandovers",
                column: "HandoverDoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_DoctorHandovers_HandoverTime",
                table: "Handover_DoctorHandovers",
                column: "HandoverTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Handover_DoctorHandovers_HandoverDate",
                table: "Handover_DoctorHandovers");

            migrationBuilder.DropIndex(
                name: "IX_Handover_DoctorHandovers_HandoverDoctorCode",
                table: "Handover_DoctorHandovers");

            migrationBuilder.DropIndex(
                name: "IX_Handover_DoctorHandovers_HandoverTime",
                table: "Handover_DoctorHandovers");
        }
    }
}
