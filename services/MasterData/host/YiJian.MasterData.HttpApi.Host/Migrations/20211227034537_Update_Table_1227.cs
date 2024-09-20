using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Table_1227 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Sys_Sequences");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Sys_Sequences");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Sys_Sequences");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_Treats");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_Treats");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_Treats");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_MedicineFrequencies");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_MedicineFrequencies");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_MedicineFrequencies");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_LabTarget");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_LabTarget");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_LabTarget");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_LabSpecimenPosition");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_LabSpecimenPosition");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_LabSpecimenPosition");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_LabSpecimen");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_LabSpecimen");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_LabSpecimen");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_LabContainer");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_LabContainer");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_LabContainer");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_LabCatalog");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_LabCatalog");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_LabCatalog");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_ExamTarget");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_ExamTarget");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_ExamTarget");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_ExamPart");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_ExamPart");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_ExamPart");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_ExamNote");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_ExamNote");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_ExamNote");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_ExamCatalog");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_ExamCatalog");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_ExamCatalog");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_AllItems");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_AllItems");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "Dict_AllItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Sys_Sequences",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Sys_Sequences",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Sys_Sequences",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_Treats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_Treats",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_MedicineUsages",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_MedicineUsages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_MedicineUsages",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_Medicines",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_Medicines",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_MedicineFrequencies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_MedicineFrequencies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_MedicineFrequencies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_LabTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_LabTarget",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_LabTarget",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_LabSpecimenPosition",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_LabSpecimenPosition",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_LabSpecimenPosition",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_LabSpecimen",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_LabSpecimen",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_LabSpecimen",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_LabProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_LabProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_LabProject",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_LabContainer",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_LabContainer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_LabContainer",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_LabCatalog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_LabCatalog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_LabCatalog",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_ExamTarget",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_ExamTarget",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_ExamProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_ExamProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_ExamProject",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_ExamPart",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_ExamPart",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_ExamPart",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_ExamNote",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_ExamNote",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_ExamNote",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_ExamCatalog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_ExamCatalog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_ExamCatalog",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_AllItems",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_AllItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "Dict_AllItems",
                type: "bit",
                nullable: true);
        }
    }
}
