using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_LabProject_1151 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionCode",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Dict_LabProject");

            migrationBuilder.AddColumn<string>(
                name: "SpecimenPartCode",
                table: "Dict_LabProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "检验部位编码");

            migrationBuilder.AddColumn<string>(
                name: "SpecimenPartName",
                table: "Dict_LabProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "检验部位名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecimenPartCode",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "SpecimenPartName",
                table: "Dict_LabProject");

            migrationBuilder.AddColumn<string>(
                name: "PositionCode",
                table: "Dict_LabProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "位置编码");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Dict_LabProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "位置名称");
        }
    }
}
