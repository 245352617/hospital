using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace YiJian.Nursing.Migrations
{
    public partial class add_IsPrint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckStatus",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "DosageQty",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "DosageUnit",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "FinishNurse",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "FinishNurseCode",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "IfCalcIn",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "IsInfusion",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "NurseTime",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "SortNum",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "ExecTime",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ExecutorCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ExecutorName",
                table: "NursingRecipe");

            migrationBuilder.AlterColumn<int>(
                name: "SystolicPressure",
                table: "NursingTemperatureRecord",
                type: "int",
                nullable: true,
                comment: "血压BP舒张压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "血压BP收缩压(mmHg)");

            migrationBuilder.AlterColumn<int>(
                name: "DiastolicPressure",
                table: "NursingTemperatureRecord",
                type: "int",
                nullable: true,
                comment: "血压BP收缩压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "血压BP舒张压(mmHg)");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrint",
                table: "NursingRecipeExec",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否已经打印瓶贴");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrint",
                table: "NursingRecipeExec");

            migrationBuilder.AlterColumn<int>(
                name: "SystolicPressure",
                table: "NursingTemperatureRecord",
                type: "int",
                nullable: true,
                comment: "血压BP收缩压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "血压BP舒张压(mmHg)");

            migrationBuilder.AlterColumn<int>(
                name: "DiastolicPressure",
                table: "NursingTemperatureRecord",
                type: "int",
                nullable: true,
                comment: "血压BP舒张压(mmHg)",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "血压BP收缩压(mmHg)");

            migrationBuilder.AddColumn<string>(
                name: "CheckStatus",
                table: "NursingRecipeExec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "核对结果");

            migrationBuilder.AddColumn<string>(
                name: "DosageQty",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "每次剂量");

            migrationBuilder.AddColumn<string>(
                name: "DosageUnit",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "每次剂量单位");

            migrationBuilder.AddColumn<string>(
                name: "FinishNurse",
                table: "NursingRecipeExec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "完成护士名称");

            migrationBuilder.AddColumn<string>(
                name: "FinishNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "完成护士");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                comment: "执行完成时间");

            migrationBuilder.AddColumn<int>(
                name: "IfCalcIn",
                table: "NursingRecipeExec",
                type: "int",
                nullable: true,
                comment: "是否计算进入量");

            migrationBuilder.AddColumn<bool>(
                name: "IsInfusion",
                table: "NursingRecipeExec",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否输液");

            migrationBuilder.AddColumn<DateTime>(
                name: "NurseTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                comment: "护理时间");

            migrationBuilder.AddColumn<int>(
                name: "SortNum",
                table: "NursingRecipeExec",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "排序编号");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExecTime",
                table: "NursingRecipe",
                type: "datetime2",
                nullable: true,
                comment: "执行时间");

            migrationBuilder.AddColumn<string>(
                name: "ExecutorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行者编码");

            migrationBuilder.AddColumn<string>(
                name: "ExecutorName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行者名称");
        }
    }
}
