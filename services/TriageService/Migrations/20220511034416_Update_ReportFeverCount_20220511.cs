using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Update_ReportFeverCount_20220511 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportFeverCount",
                table: "ReportFeverCount");

            migrationBuilder.RenameTable(
                name: "ReportFeverCount",
                newName: "Rpt_FeverCount");

            migrationBuilder.AlterTable(
                name: "Rpt_FeverCount",
                comment: "发热人数统计");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TriageDate",
                table: "Rpt_FeverCount",
                nullable: false,
                comment: "分诊日期",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "Rpt_FeverCount",
                nullable: true,
                comment: "排序",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FeverCountChanged",
                table: "Rpt_FeverCount",
                nullable: true,
                comment: "手动修改人数",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FeverCount",
                table: "Rpt_FeverCount",
                nullable: false,
                comment: "系统统计人数",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DeptName",
                table: "Rpt_FeverCount",
                maxLength: 60,
                nullable: true,
                comment: "科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rpt_FeverCount",
                table: "Rpt_FeverCount",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rpt_FeverCount",
                table: "Rpt_FeverCount");

            migrationBuilder.RenameTable(
                name: "Rpt_FeverCount",
                newName: "ReportFeverCount");

            migrationBuilder.AlterTable(
                name: "ReportFeverCount",
                oldComment: "发热人数统计");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TriageDate",
                table: "ReportFeverCount",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "分诊日期");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "ReportFeverCount",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "排序");

            migrationBuilder.AlterColumn<int>(
                name: "FeverCountChanged",
                table: "ReportFeverCount",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "手动修改人数");

            migrationBuilder.AlterColumn<int>(
                name: "FeverCount",
                table: "ReportFeverCount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "系统统计人数");

            migrationBuilder.AlterColumn<string>(
                name: "DeptName",
                table: "ReportFeverCount",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "科室名称");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportFeverCount",
                table: "ReportFeverCount",
                column: "Id");
        }
    }
}
