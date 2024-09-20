using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class Add_MedicalInsuranceCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicalInsuranceCode",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保编码");

            migrationBuilder.AddColumn<string>(
                name: "MedicalInsuranceCode",
                table: "RC_Prescribe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalInsuranceCode",
                table: "RC_QuickStartMedicine");

            migrationBuilder.DropColumn(
                name: "MedicalInsuranceCode",
                table: "RC_Prescribe");
        }
    }
}
