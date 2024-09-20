using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class initdb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "T",
                table: "RpNursingRecord",
                type: "decimal(18,2)",
                nullable: true,
                comment: "体温（单位℃）",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "体温（单位℃）");

            migrationBuilder.AlterColumn<decimal>(
                name: "SPO2",
                table: "RpNursingRecord",
                type: "decimal(18,2)",
                nullable: true,
                comment: "血氧饱和度SPO2 %",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "血氧饱和度SPO2 %");

            migrationBuilder.AlterColumn<int>(
                name: "R",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "呼吸(次/min)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "呼吸(次/min)");

            migrationBuilder.AlterColumn<int>(
                name: "P",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "脉搏P(次/min)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "脉搏P(次/min)");

            migrationBuilder.AlterColumn<int>(
                name: "HR",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "心率(次/min)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "心率(次/min)");

            migrationBuilder.AlterColumn<int>(
                name: "BP2",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "血压BP舒张压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "血压BP舒张压(mmHg)");

            migrationBuilder.AlterColumn<int>(
                name: "BP",
                table: "RpNursingRecord",
                type: "int",
                nullable: true,
                comment: "血压BP收缩压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "血压BP收缩压(mmHg)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "T",
                table: "RpNursingRecord",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "体温（单位℃）",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "体温（单位℃）");

            migrationBuilder.AlterColumn<decimal>(
                name: "SPO2",
                table: "RpNursingRecord",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "血氧饱和度SPO2 %",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "血氧饱和度SPO2 %");

            migrationBuilder.AlterColumn<int>(
                name: "R",
                table: "RpNursingRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "呼吸(次/min)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "呼吸(次/min)");

            migrationBuilder.AlterColumn<int>(
                name: "P",
                table: "RpNursingRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "脉搏P(次/min)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "脉搏P(次/min)");

            migrationBuilder.AlterColumn<int>(
                name: "HR",
                table: "RpNursingRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "心率(次/min)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "心率(次/min)");

            migrationBuilder.AlterColumn<int>(
                name: "BP2",
                table: "RpNursingRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "血压BP舒张压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "血压BP舒张压(mmHg)");

            migrationBuilder.AlterColumn<int>(
                name: "BP",
                table: "RpNursingRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "血压BP收缩压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "血压BP收缩压(mmHg)");
        }
    }
}
