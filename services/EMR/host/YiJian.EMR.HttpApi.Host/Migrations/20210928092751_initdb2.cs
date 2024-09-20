using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_EmrMyXmlTemplate_EmrTemplateCatalogue_TemplateCatalogueId",
                table: "EmrMyXmlTemplate",
                column: "TemplateCatalogueId",
                principalTable: "EmrTemplateCatalogue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmrPatientEmrXml_EmrPatientEmr_PatientEmrId",
                table: "EmrPatientEmrXml",
                column: "PatientEmrId",
                principalTable: "EmrPatientEmr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmrMyXmlTemplate_EmrTemplateCatalogue_TemplateCatalogueId",
                table: "EmrMyXmlTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_EmrPatientEmrXml_EmrPatientEmr_PatientEmrId",
                table: "EmrPatientEmrXml");
        }
    }
}
