using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210medDetailResultaddcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeptCode",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "科室编码");

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "科室名称 ");

            migrationBuilder.AddColumn<string>(
                name: "DoctorCode",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医生编码");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医生姓名");

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "病人ID");

            migrationBuilder.AddColumn<string>(
                name: "VisSerialNo",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "就诊流水号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptCode",
                table: "RC_MedDetailResult");

            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "RC_MedDetailResult");

            migrationBuilder.DropColumn(
                name: "DoctorCode",
                table: "RC_MedDetailResult");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "RC_MedDetailResult");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "RC_MedDetailResult");

            migrationBuilder.DropColumn(
                name: "VisSerialNo",
                table: "RC_MedDetailResult");
        }
    }
}
