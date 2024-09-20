using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class addAntibioticPermissionPrescriptionPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AntibioticPermission",
                table: "NursingPrescribe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "抗生素权限");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionPermission",
                table: "NursingPrescribe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "处方权");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AntibioticPermission",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "PrescriptionPermission",
                table: "NursingPrescribe");
        }
    }
}
