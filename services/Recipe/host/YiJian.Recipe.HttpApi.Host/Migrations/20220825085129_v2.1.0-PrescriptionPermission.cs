using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210PrescriptionPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AntibioticPermission",
                table: "RC_ProjectMedicineProp",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "抗生素权限");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionPermission",
                table: "RC_ProjectMedicineProp",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "处方权");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AntibioticPermission",
                table: "RC_ProjectMedicineProp");

            migrationBuilder.DropColumn(
                name: "PrescriptionPermission",
                table: "RC_ProjectMedicineProp");
        }
    }
}
