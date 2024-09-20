using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class recipeexecupdatecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanExcuteHours",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "StopDateTime",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "StopDoctorCode",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "StopDoctorName",
                table: "NursingPrescribes");

            migrationBuilder.RenameColumn(
                name: "Dosage",
                table: "NursingRecipeExec",
                newName: "DosageQty");

            migrationBuilder.AddColumn<string>(
                name: "UsageName",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "用法名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsageName",
                table: "NursingRecipeExec");

            migrationBuilder.RenameColumn(
                name: "DosageQty",
                table: "NursingRecipeExec",
                newName: "Dosage");

            migrationBuilder.AddColumn<string>(
                name: "PlanExcuteHours",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "预计执行时长");

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "用法");

            migrationBuilder.AddColumn<DateTime>(
                name: "StopDateTime",
                table: "NursingPrescribes",
                type: "datetime2",
                nullable: true,
                comment: "停嘱时间");

            migrationBuilder.AddColumn<string>(
                name: "StopDoctorCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "停嘱医生代码");

            migrationBuilder.AddColumn<string>(
                name: "StopDoctorName",
                table: "NursingPrescribes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "停嘱医生名称");
        }
    }
}
