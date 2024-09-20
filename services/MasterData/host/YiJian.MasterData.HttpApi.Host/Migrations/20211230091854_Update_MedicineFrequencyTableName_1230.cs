using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_MedicineFrequencyTableName_1230 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_MedicineFrequencie",
                table: "Dict_MedicineFrequencie");

            migrationBuilder.RenameTable(
                name: "Dict_MedicineFrequencie",
                newName: "Dict_MedicineFrequency");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_MedicineFrequencie_FrequencyCode",
                table: "Dict_MedicineFrequency",
                newName: "IX_Dict_MedicineFrequency_FrequencyCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_MedicineFrequency",
                table: "Dict_MedicineFrequency",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_MedicineFrequency",
                table: "Dict_MedicineFrequency");

            migrationBuilder.RenameTable(
                name: "Dict_MedicineFrequency",
                newName: "Dict_MedicineFrequencie");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_MedicineFrequency_FrequencyCode",
                table: "Dict_MedicineFrequencie",
                newName: "IX_Dict_MedicineFrequencie_FrequencyCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_MedicineFrequencie",
                table: "Dict_MedicineFrequencie",
                column: "Id");
        }
    }
}
