using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_LabCatalog_TableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabTargets",
                table: "Dict_LabTargets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabSpecimens",
                table: "Dict_LabSpecimens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabSpecimenPositions",
                table: "Dict_LabSpecimenPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabProjects",
                table: "Dict_LabProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabContainers",
                table: "Dict_LabContainers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabCatalogs",
                table: "Dict_LabCatalogs");

            migrationBuilder.RenameTable(
                name: "Dict_LabTargets",
                newName: "Dict_LabTarget");

            migrationBuilder.RenameTable(
                name: "Dict_LabSpecimens",
                newName: "Dict_LabSpecimen");

            migrationBuilder.RenameTable(
                name: "Dict_LabSpecimenPositions",
                newName: "Dict_LabSpecimenPosition");

            migrationBuilder.RenameTable(
                name: "Dict_LabProjects",
                newName: "Dict_LabProject");

            migrationBuilder.RenameTable(
                name: "Dict_LabContainers",
                newName: "Dict_LabContainer");

            migrationBuilder.RenameTable(
                name: "Dict_LabCatalogs",
                newName: "Dict_LabCatalog");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabTargets_TargetCode",
                table: "Dict_LabTarget",
                newName: "IX_Dict_LabTarget_TargetCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabSpecimens_SpecimenCode",
                table: "Dict_LabSpecimen",
                newName: "IX_Dict_LabSpecimen_SpecimenCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabSpecimenPositions_SpecimenCode",
                table: "Dict_LabSpecimenPosition",
                newName: "IX_Dict_LabSpecimenPosition_SpecimenCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabProjects_ProjectCode",
                table: "Dict_LabProject",
                newName: "IX_Dict_LabProject_ProjectCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabContainers_ContainerCode",
                table: "Dict_LabContainer",
                newName: "IX_Dict_LabContainer_ContainerCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabCatalogs_CatalogCode",
                table: "Dict_LabCatalog",
                newName: "IX_Dict_LabCatalog_CatalogCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabTarget",
                table: "Dict_LabTarget",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabSpecimen",
                table: "Dict_LabSpecimen",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabSpecimenPosition",
                table: "Dict_LabSpecimenPosition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabProject",
                table: "Dict_LabProject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabContainer",
                table: "Dict_LabContainer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabCatalog",
                table: "Dict_LabCatalog",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabTarget",
                table: "Dict_LabTarget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabSpecimenPosition",
                table: "Dict_LabSpecimenPosition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabSpecimen",
                table: "Dict_LabSpecimen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabProject",
                table: "Dict_LabProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabContainer",
                table: "Dict_LabContainer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_LabCatalog",
                table: "Dict_LabCatalog");

            migrationBuilder.RenameTable(
                name: "Dict_LabTarget",
                newName: "Dict_LabTargets");

            migrationBuilder.RenameTable(
                name: "Dict_LabSpecimenPosition",
                newName: "Dict_LabSpecimenPositions");

            migrationBuilder.RenameTable(
                name: "Dict_LabSpecimen",
                newName: "Dict_LabSpecimens");

            migrationBuilder.RenameTable(
                name: "Dict_LabProject",
                newName: "Dict_LabProjects");

            migrationBuilder.RenameTable(
                name: "Dict_LabContainer",
                newName: "Dict_LabContainers");

            migrationBuilder.RenameTable(
                name: "Dict_LabCatalog",
                newName: "Dict_LabCatalogs");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabTarget_TargetCode",
                table: "Dict_LabTargets",
                newName: "IX_Dict_LabTargets_TargetCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabSpecimenPosition_SpecimenCode",
                table: "Dict_LabSpecimenPositions",
                newName: "IX_Dict_LabSpecimenPositions_SpecimenCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabSpecimen_SpecimenCode",
                table: "Dict_LabSpecimens",
                newName: "IX_Dict_LabSpecimens_SpecimenCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabProject_ProjectCode",
                table: "Dict_LabProjects",
                newName: "IX_Dict_LabProjects_ProjectCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabContainer_ContainerCode",
                table: "Dict_LabContainers",
                newName: "IX_Dict_LabContainers_ContainerCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabCatalog_CatalogCode",
                table: "Dict_LabCatalogs",
                newName: "IX_Dict_LabCatalogs_CatalogCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabTargets",
                table: "Dict_LabTargets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabSpecimenPositions",
                table: "Dict_LabSpecimenPositions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabSpecimens",
                table: "Dict_LabSpecimens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabProjects",
                table: "Dict_LabProjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabContainers",
                table: "Dict_LabContainers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_LabCatalogs",
                table: "Dict_LabCatalogs",
                column: "Id");
        }
    }
}
