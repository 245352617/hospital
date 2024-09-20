using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_DoctorPatients_1119 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Handover_NursePatients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "性别",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "性别");

            migrationBuilder.AddColumn<string>(
                name: "SexName",
                table: "Handover_NursePatients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "性别名称");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPatient",
                table: "Handover_NurseHandovers",
                type: "int",
                nullable: false,
                comment: "所有患者数量",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Handover_DoctorPatients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "性别",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "性别");

            migrationBuilder.AddColumn<string>(
                name: "SexName",
                table: "Handover_DoctorPatients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "性别名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SexName",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "SexName",
                table: "Handover_DoctorPatients");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Handover_NursePatients",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "性别",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "性别");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPatient",
                table: "Handover_NurseHandovers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "所有患者数量");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Handover_DoctorPatients",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "性别",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "性别");
        }
    }
}
