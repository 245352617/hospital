using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_NurseHandover_112017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HandoverNurseHandovers",
                table: "HandoverNurseHandovers");

            migrationBuilder.RenameTable(
                name: "HandoverNurseHandovers",
                newName: "Handover_NurseHandovers");

            migrationBuilder.RenameIndex(
                name: "IX_HandoverNurseHandovers_PI_ID",
                table: "Handover_NurseHandovers",
                newName: "IX_Handover_NurseHandovers_PI_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Handover_NurseHandovers",
                table: "Handover_NurseHandovers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Handover_NurseHandovers",
                table: "Handover_NurseHandovers");

            migrationBuilder.RenameTable(
                name: "Handover_NurseHandovers",
                newName: "HandoverNurseHandovers");

            migrationBuilder.RenameIndex(
                name: "IX_Handover_NurseHandovers_PI_ID",
                table: "HandoverNurseHandovers",
                newName: "IX_HandoverNurseHandovers_PI_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HandoverNurseHandovers",
                table: "HandoverNurseHandovers",
                column: "Id");
        }
    }
}
