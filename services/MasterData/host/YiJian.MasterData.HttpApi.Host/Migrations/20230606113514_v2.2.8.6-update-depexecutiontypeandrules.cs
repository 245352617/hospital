using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class v2286updatedepexecutiontypeandrules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepExecutionRules",
                table: "Dict_Treat",
                type: "nvarchar(max)",
                nullable: true,
                comment: "科室跟踪执行规则 depExecutionType=1：固定科室,depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、默认:departmentCode");

            migrationBuilder.AddColumn<int>(
                name: "DepExecutionType",
                table: "Dict_Treat",
                type: "int",
                nullable: true,
                comment: "科室跟踪执行类别 0.不跟踪执行(默认开单科室),1.按固定科室执行(取depExecutionRules字段),2.按病人科室执行(默认开单科室),3.按病人病区执行（默认开单科室）,9.按规则执行（医生选择开单科室、默认为开单科室）");

            migrationBuilder.AlterColumn<string>(
                name: "DepExecutionRules",
                table: "Dict_LabProject",
                type: "nvarchar(max)",
                nullable: true,
                comment: "科室跟踪执行规则 depExecutionType=1：固定科室,depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、默认:departmentCode",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "科室跟踪执行规则 depExecutionType=1：固定科室,depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、默认:departmentCode");

            migrationBuilder.AlterColumn<int>(
                name: "DepExecutionType",
                table: "Dict_ExamProject",
                type: "int",
                nullable: true,
                comment: "科室跟踪执行类别 0.不跟踪执行(默认开单科室),1.按固定科室执行(取depExecutionRules字段),2.按病人科室执行(默认开单科室),3.按病人病区执行（默认开单科室）,9.按规则执行（医生选择开单科室、默认为开单科室）",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DepExecutionRules",
                table: "Dict_ExamProject",
                type: "nvarchar(max)",
                nullable: true,
                comment: "科室跟踪执行规则 depExecutionType=1：固定科室,depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、默认:departmentCode",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepExecutionRules",
                table: "Dict_Treat");

            migrationBuilder.DropColumn(
                name: "DepExecutionType",
                table: "Dict_Treat");

            migrationBuilder.AlterColumn<int>(
                name: "DepExecutionRules",
                table: "Dict_LabProject",
                type: "int",
                nullable: true,
                comment: "科室跟踪执行规则 depExecutionType=1：固定科室,depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、默认:departmentCode",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "科室跟踪执行规则 depExecutionType=1：固定科室,depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、默认:departmentCode");

            migrationBuilder.AlterColumn<int>(
                name: "DepExecutionType",
                table: "Dict_ExamProject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "科室跟踪执行类别 0.不跟踪执行(默认开单科室),1.按固定科室执行(取depExecutionRules字段),2.按病人科室执行(默认开单科室),3.按病人病区执行（默认开单科室）,9.按规则执行（医生选择开单科室、默认为开单科室）");

            migrationBuilder.AlterColumn<int>(
                name: "DepExecutionRules",
                table: "Dict_ExamProject",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "科室跟踪执行规则 depExecutionType=1：固定科室,depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、默认:departmentCode");
        }
    }
}
