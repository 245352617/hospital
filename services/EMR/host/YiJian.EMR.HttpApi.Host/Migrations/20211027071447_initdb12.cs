using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmrPatientEmrXmlHistory",
                table: "EmrPatientEmrXmlHistory");

            migrationBuilder.RenameTable(
                name: "EmrPatientEmrXmlHistory",
                newName: "EmrXmlHistory");

            migrationBuilder.RenameIndex(
                name: "IX_EmrPatientEmrXmlHistory_XmlId",
                table: "EmrXmlHistory",
                newName: "IX_EmrXmlHistory_XmlId");

            migrationBuilder.RenameIndex(
                name: "IX_EmrPatientEmrXmlHistory_XmlCategory",
                table: "EmrXmlHistory",
                newName: "IX_EmrXmlHistory_XmlCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmrXmlHistory",
                table: "EmrXmlHistory",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmrXmlHistory",
                table: "EmrXmlHistory");

            migrationBuilder.RenameTable(
                name: "EmrXmlHistory",
                newName: "EmrPatientEmrXmlHistory");

            migrationBuilder.RenameIndex(
                name: "IX_EmrXmlHistory_XmlId",
                table: "EmrPatientEmrXmlHistory",
                newName: "IX_EmrPatientEmrXmlHistory_XmlId");

            migrationBuilder.RenameIndex(
                name: "IX_EmrXmlHistory_XmlCategory",
                table: "EmrPatientEmrXmlHistory",
                newName: "IX_EmrPatientEmrXmlHistory_XmlCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmrPatientEmrXmlHistory",
                table: "EmrPatientEmrXmlHistory",
                column: "Id");
        }
    }
}
