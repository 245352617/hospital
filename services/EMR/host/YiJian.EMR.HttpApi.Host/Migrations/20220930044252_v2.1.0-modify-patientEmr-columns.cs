using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v210modifypatientEmrcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "EmrPatientEmr",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "患者名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "患者名称");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorName",
                table: "EmrPatientEmr",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "医生名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldComment: "医生名称");

            migrationBuilder.AddColumn<string>(
                name: "DeptCode",
                table: "EmrPatientEmr",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "EmrPatientEmr",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptCode",
                table: "EmrPatientEmr");

            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "EmrPatientEmr");

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "EmrPatientEmr",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "患者名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "患者名称");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorName",
                table: "EmrPatientEmr",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                comment: "医生名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "医生名称");
        }
    }
}
