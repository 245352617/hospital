using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class ReportSetting20210706 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddUser",
                table: "Dict_ReportSetting",
                maxLength: 50,
                nullable: true,
                comment: "添加人");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Dict_ReportSetting",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "创建时间");

            migrationBuilder.AddColumn<string>(
                name: "DeleteUser",
                table: "Dict_ReportSetting",
                maxLength: 50,
                nullable: true,
                comment: "删除人");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Dict_ReportSetting",
                nullable: true,
                comment: "删除时间");

            migrationBuilder.AddColumn<string>(
                name: "ExtensionField1",
                table: "Dict_ReportSetting",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段1");

            migrationBuilder.AddColumn<string>(
                name: "ExtensionField2",
                table: "Dict_ReportSetting",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段2");

            migrationBuilder.AddColumn<string>(
                name: "ExtensionField3",
                table: "Dict_ReportSetting",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段3");

            migrationBuilder.AddColumn<string>(
                name: "ExtensionField4",
                table: "Dict_ReportSetting",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段4");

            migrationBuilder.AddColumn<string>(
                name: "ExtensionField5",
                table: "Dict_ReportSetting",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段5");

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "Dict_ReportSetting",
                maxLength: 250,
                nullable: true,
                comment: "医院编码");

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "Dict_ReportSetting",
                maxLength: 250,
                nullable: true,
                comment: "医院名称");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Dict_ReportSetting",
                nullable: false,
                defaultValue: false,
                comment: "是否删除");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Dict_ReportSetting",
                nullable: true,
                comment: "修改时间");

            migrationBuilder.AddColumn<string>(
                name: "ModUser",
                table: "Dict_ReportSetting",
                maxLength: 50,
                nullable: true,
                comment: "修改人");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Dict_ReportSetting",
                maxLength: 256,
                nullable: true,
                comment: "备注");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Dict_ReportSetting",
                nullable: true,
                comment: "排序");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddUser",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "DeleteUser",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "ExtensionField1",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "ExtensionField2",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "ExtensionField3",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "ExtensionField4",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "ExtensionField5",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "ModUser",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Dict_ReportSetting");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Dict_ReportSetting");
        }
    }
}
