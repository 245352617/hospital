using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class updatehistoryfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckNurseCode",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "CheckNurseName",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "CheckStatus",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "CheckTime",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "FinishNurse",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "FinishNurseCode",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "NursingStatus",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "NursingRecipeExecHistory");

            migrationBuilder.AddColumn<int>(
                name: "CombineStatus",
                table: "NursingRecipeExecHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "操作后状态");

            migrationBuilder.AddColumn<string>(
                name: "NurseCode",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "护士编码");

            migrationBuilder.AddColumn<string>(
                name: "NurseName",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "护士名称");

            migrationBuilder.AddColumn<DateTime>(
                name: "OperationTime",
                table: "NursingRecipeExecHistory",
                type: "datetime2",
                nullable: true,
                comment: "实际操作时间");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeExecId",
                table: "NursingRecipeExecHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "医嘱执行Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CombineStatus",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "NurseCode",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "NurseName",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "OperationTime",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "RecipeExecId",
                table: "NursingRecipeExecHistory");

            migrationBuilder.AddColumn<string>(
                name: "CheckNurseCode",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "核对护士（核对人）");

            migrationBuilder.AddColumn<string>(
                name: "CheckNurseName",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "核对护士名称（核对人）");

            migrationBuilder.AddColumn<string>(
                name: "CheckStatus",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "核对结果");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckTime",
                table: "NursingRecipeExecHistory",
                type: "datetime2",
                nullable: true,
                comment: "实际核对时间");

            migrationBuilder.AddColumn<string>(
                name: "FinishNurse",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "结束执行人");

            migrationBuilder.AddColumn<string>(
                name: "FinishNurseCode",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "结束执行人");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishTime",
                table: "NursingRecipeExecHistory",
                type: "datetime2",
                nullable: true,
                comment: "实际结束时间");

            migrationBuilder.AddColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipeExecHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医嘱执行状态");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "NursingRecipeExecHistory",
                type: "datetime2",
                nullable: true,
                comment: "开始时间");
        }
    }
}
