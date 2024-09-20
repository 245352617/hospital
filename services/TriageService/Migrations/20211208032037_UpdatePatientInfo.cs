using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class UpdatePatientInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsBasicInfoReadOnly",
                table: "Triage_PatientInfo",
                nullable: false,
                comment: "患者基本信息是否不可编辑",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "患者基本信息是否不可编译");

            migrationBuilder.AddColumn<bool>(
                name: "IsCovidExamFromOuterSystem",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: false,
                comment: "新冠问卷是否从外部获取");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCovidExamFromOuterSystem",
                table: "Triage_PatientInfo");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBasicInfoReadOnly",
                table: "Triage_PatientInfo",
                type: "bit",
                nullable: false,
                comment: "患者基本信息是否不可编译",
                oldClrType: typeof(bool),
                oldComment: "患者基本信息是否不可编辑");
        }
    }
}
