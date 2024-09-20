using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_LabProject_1112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabProjects_CatalogCode",
                table: "Dict_LabProjects",
                column: "CatalogCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamProjects_CatalogCode",
                table: "Dict_ExamProjects",
                column: "CatalogCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dict_LabProjects_CatalogCode",
                table: "Dict_LabProjects");

            migrationBuilder.DropIndex(
                name: "IX_Dict_ExamProjects_CatalogCode",
                table: "Dict_ExamProjects");
        }
    }
}
