using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class movePrescribeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescribeTypeCode",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "PrescribeTypeName",
                table: "NursingPrescribe");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeTypeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱类型编码");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeTypeName",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱类型：临嘱、长嘱、出院带药等");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescribeTypeCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "PrescribeTypeName",
                table: "NursingRecipe");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeTypeCode",
                table: "NursingPrescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱类型编码");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeTypeName",
                table: "NursingPrescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱类型：临嘱、长嘱、出院带药等");
        }
    }
}
