using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Classify",
                table: "EmrPatientEmr",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "电子文书分类（0=电子病历，1=文书）");

            migrationBuilder.AddColumn<int>(
                name: "Classify",
                table: "EmrCatalogue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "电子文书分类(0=电子病历,1=文书)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classify",
                table: "EmrPatientEmr");

            migrationBuilder.DropColumn(
                name: "Classify",
                table: "EmrCatalogue");
        }
    }
}
