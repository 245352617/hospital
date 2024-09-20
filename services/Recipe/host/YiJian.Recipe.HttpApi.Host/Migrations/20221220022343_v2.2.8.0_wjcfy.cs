using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v2280_wjcfy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCriticalPrescription",
                table: "RC_QuickStartMedicine",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCriticalPrescription",
                table: "RC_Prescribe",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否危急处方 1=是 ，0=否");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCriticalPrescription",
                table: "RC_QuickStartMedicine");

            migrationBuilder.DropColumn(
                name: "IsCriticalPrescription",
                table: "RC_Prescribe");
        }
    }
}
