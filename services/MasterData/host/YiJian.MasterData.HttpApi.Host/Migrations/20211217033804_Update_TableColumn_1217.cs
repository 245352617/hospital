using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_TableColumn_1217 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsureType",
                table: "Dict_LabTarget");

            migrationBuilder.DropColumn(
                name: "ExamPartCode",
                table: "Dict_ExamProject");

            migrationBuilder.RenameColumn(
                name: "TreatUnit",
                table: "Dict_Treats",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "PositionName",
                table: "Dict_LabSpecimenPosition",
                newName: "SpecimenPartName");

            migrationBuilder.RenameColumn(
                name: "PositionCode",
                table: "Dict_LabSpecimenPosition",
                newName: "SpecimenPartCode");

            migrationBuilder.RenameColumn(
                name: "InsureType",
                table: "Dict_ExamTarget",
                newName: "InsuranceType");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceType",
                table: "Dict_LabTarget",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "医保类型");

            migrationBuilder.AddColumn<string>(
                name: "PartCode",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "检查部位编码");

            migrationBuilder.AddColumn<string>(
                name: "PartName",
                table: "Dict_ExamProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "检查部位名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuranceType",
                table: "Dict_LabTarget");

            migrationBuilder.DropColumn(
                name: "PartCode",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "PartName",
                table: "Dict_ExamProject");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Dict_Treats",
                newName: "TreatUnit");

            migrationBuilder.RenameColumn(
                name: "SpecimenPartName",
                table: "Dict_LabSpecimenPosition",
                newName: "PositionName");

            migrationBuilder.RenameColumn(
                name: "SpecimenPartCode",
                table: "Dict_LabSpecimenPosition",
                newName: "PositionCode");

            migrationBuilder.RenameColumn(
                name: "InsuranceType",
                table: "Dict_ExamTarget",
                newName: "InsureType");

            migrationBuilder.AddColumn<int>(
                name: "InsureType",
                table: "Dict_LabTarget",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医保类型");

            migrationBuilder.AddColumn<string>(
                name: "ExamPartCode",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "检查部位");
        }
    }
}
