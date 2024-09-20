using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class UpdateSomeFiledToBooleanForAdmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsSoreThroatAndCough",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "是否咽痛咳嗽",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否咽痛咳嗽");

            migrationBuilder.AlterColumn<string>(
                name: "IsMediumAndHighRisk",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "是否去过中高风险区",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否去过中高风险区");

            migrationBuilder.AlterColumn<string>(
                name: "IsHot",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "是否发热",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否发热");

            migrationBuilder.AlterColumn<string>(
                name: "IsFocusIsolated",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "最近14天内您是否在集中隔离医学观察场所留观",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "最近14天内您是否在集中隔离医学观察场所留观");

            migrationBuilder.AlterColumn<string>(
                name: "IsContactNewCoronavirus",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "2周内是否接触过确诊新冠阳性患者",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "2周内是否接触过确诊新冠阳性患者");

            migrationBuilder.AlterColumn<string>(
                name: "IsContactHotPatient",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "2周内是否接触过中高风险区发热患者",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "2周内是否接触过中高风险区发热患者");

            migrationBuilder.AlterColumn<string>(
                name: "IsBeenAbroad",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "2周内是否有境外旅居史",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "2周内是否有境外旅居史");

            migrationBuilder.AlterColumn<string>(
                name: "IsAggregation",
                table: "Triage_AdmissionInfo",
                type: "varchar(10)",
                nullable: true,
                comment: "是否聚集性发病",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否聚集性发病");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsSoreThroatAndCough",
                table: "Triage_AdmissionInfo",
                type: "bit",
                nullable: false,
                comment: "是否咽痛咳嗽",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "是否咽痛咳嗽");

            migrationBuilder.AlterColumn<bool>(
                name: "IsMediumAndHighRisk",
                table: "Triage_AdmissionInfo",
                type: "bit",
                nullable: false,
                comment: "是否去过中高风险区",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "是否去过中高风险区");

            migrationBuilder.AlterColumn<bool>(
                name: "IsHot",
                table: "Triage_AdmissionInfo",
                type: "bit",
                nullable: false,
                comment: "是否发热",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "是否发热");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFocusIsolated",
                table: "Triage_AdmissionInfo",
                type: "bit",
                nullable: false,
                comment: "最近14天内您是否在集中隔离医学观察场所留观",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "最近14天内您是否在集中隔离医学观察场所留观");

            migrationBuilder.AlterColumn<bool>(
                name: "IsContactNewCoronavirus",
                table: "Triage_AdmissionInfo",
                type: "bit",
                nullable: false,
                comment: "2周内是否接触过确诊新冠阳性患者",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "2周内是否接触过确诊新冠阳性患者");

            migrationBuilder.AlterColumn<bool>(
                name: "IsContactHotPatient",
                table: "Triage_AdmissionInfo",
                type: "bit",
                nullable: false,
                comment: "2周内是否接触过中高风险区发热患者",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "2周内是否接触过中高风险区发热患者");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBeenAbroad",
                table: "Triage_AdmissionInfo",
                type: "bit",
                nullable: false,
                comment: "2周内是否有境外旅居史",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "2周内是否有境外旅居史");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAggregation",
                table: "Triage_AdmissionInfo",
                type: "bit",
                nullable: false,
                comment: "是否聚集性发病",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true,
                oldComment: "是否聚集性发病");
        }
    }
}
