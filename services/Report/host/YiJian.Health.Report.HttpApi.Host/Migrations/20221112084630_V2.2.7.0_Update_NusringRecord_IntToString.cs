using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class V2270_Update_NusringRecord_IntToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "RpReportData");

            migrationBuilder.AddColumn<string>(
                name: "PrescriptionNo",
                table: "RpReportData",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "处方号");

            migrationBuilder.AlterColumn<string>(
                name: "T",
                table: "RpNursingRecord",
                type: "nvarchar(max)",
                nullable: true,
                comment: "体温（单位℃）",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "体温（单位℃）");

            migrationBuilder.AlterColumn<string>(
                name: "SPO2",
                table: "RpNursingRecord",
                type: "nvarchar(max)",
                nullable: true,
                comment: "血氧饱和度SPO2 %",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "血氧饱和度SPO2 %");

            migrationBuilder.AlterColumn<string>(
                name: "R",
                table: "RpNursingRecord",
                type: "nvarchar(max)",
                nullable: true,
                comment: "呼吸(次/min)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "呼吸(次/min)");

            migrationBuilder.AlterColumn<string>(
                name: "P",
                table: "RpNursingRecord",
                type: "nvarchar(max)",
                nullable: true,
                comment: "脉搏P(次/min)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "脉搏P(次/min)");

            migrationBuilder.AlterColumn<string>(
                name: "HR",
                table: "RpNursingRecord",
                type: "nvarchar(max)",
                nullable: true,
                comment: "心率(次/min)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "心率(次/min)");

            migrationBuilder.AlterColumn<string>(
                name: "BP2",
                table: "RpNursingRecord",
                type: "nvarchar(max)",
                nullable: true,
                comment: "血压BP舒张压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "血压BP舒张压(mmHg)");

            migrationBuilder.AlterColumn<string>(
                name: "BP",
                table: "RpNursingRecord",
                type: "nvarchar(max)",
                nullable: true,
                comment: "血压BP收缩压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "血压BP收缩压(mmHg)");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "RpMmol",
                type: "nvarchar(max)",
                nullable: true,
                comment: "数值",
                oldClrType: typeof(float),
                oldType: "real",
                oldComment: "数值");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriptionNo",
                table: "RpReportData");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "RpReportData",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "Url");

            migrationBuilder.AlterColumn<decimal>(
                name: "T",
                table: "RpNursingRecord",
                type: "decimal(18,2)",
                nullable: true,
                comment: "体温（单位℃）",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "体温（单位℃）");

            migrationBuilder.AlterColumn<decimal>(
                name: "SPO2",
                table: "RpNursingRecord",
                type: "decimal(18,2)",
                nullable: true,
                comment: "血氧饱和度SPO2 %",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "血氧饱和度SPO2 %");

            migrationBuilder.AlterColumn<int>(
                name: "R",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "呼吸(次/min)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "呼吸(次/min)");

            migrationBuilder.AlterColumn<int>(
                name: "P",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "脉搏P(次/min)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "脉搏P(次/min)");

            migrationBuilder.AlterColumn<int>(
                name: "HR",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "心率(次/min)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "心率(次/min)");

            migrationBuilder.AlterColumn<int>(
                name: "BP2",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "血压BP舒张压(mmHg)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "血压BP舒张压(mmHg)");

            migrationBuilder.AlterColumn<int>(
                name: "BP",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "血压BP收缩压(mmHg)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "血压BP收缩压(mmHg)");

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "RpMmol",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                comment: "数值",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "数值");
        }
    }
}
