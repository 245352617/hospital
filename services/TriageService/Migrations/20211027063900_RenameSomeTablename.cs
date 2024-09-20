using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class RenameSomeTablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TriageConfigTypeDescription",
                table: "TriageConfigTypeDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TriageConfig",
                table: "TriageConfig");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Config_TriageDevice",
                table: "Config_TriageDevice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Config_TableSetting",
                table: "Config_TableSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Config_FastTrackSetting",
                table: "Config_FastTrackSetting");

            migrationBuilder.RenameTable(
                name: "TriageConfigTypeDescription",
                newName: "Dict_TriageConfigTypeDescription");

            migrationBuilder.RenameTable(
                name: "TriageConfig",
                newName: "Dict_TriageConfig");

            migrationBuilder.RenameTable(
                name: "Config_TriageDevice",
                newName: "Dict_TriageDevice");

            migrationBuilder.RenameTable(
                name: "Config_TableSetting",
                newName: "Dict_TableSetting");

            migrationBuilder.RenameTable(
                name: "Config_FastTrackSetting",
                newName: "Dict_FastTrackSetting");

            migrationBuilder.AlterTable(
                name: "Dict_TriageConfigTypeDescription",
                comment: "分诊字典类型");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Triage_PatientInfo",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "分诊时间");

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigTypeName",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 20,
                nullable: true,
                comment: "分诊设置类型名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigTypeCode",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 20,
                nullable: true,
                comment: "分诊设置类型代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "Dict_TriageConfigTypeDescription",
                nullable: true,
                comment: "排序",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 256,
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 50,
                nullable: true,
                comment: "修改人",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "Dict_TriageConfigTypeDescription",
                nullable: true,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Dict_TriageConfigTypeDescription",
                nullable: false,
                defaultValue: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalName",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 250,
                nullable: true,
                comment: "医院名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalCode",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 250,
                nullable: true,
                comment: "医院编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField5",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段5",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField4",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段4",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField3",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段3",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField2",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段2",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField1",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段1",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "Dict_TriageConfigTypeDescription",
                nullable: true,
                comment: "删除时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteUser",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 50,
                nullable: true,
                comment: "删除人",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Dict_TriageConfigTypeDescription",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "AddUser",
                table: "Dict_TriageConfigTypeDescription",
                maxLength: 50,
                nullable: true,
                comment: "添加人",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_TriageConfigTypeDescription",
                table: "Dict_TriageConfigTypeDescription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_TriageConfig",
                table: "Dict_TriageConfig",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_TriageDevice",
                table: "Dict_TriageDevice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_TableSetting",
                table: "Dict_TableSetting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_FastTrackSetting",
                table: "Dict_FastTrackSetting",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_TriageDevice",
                table: "Dict_TriageDevice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_TriageConfigTypeDescription",
                table: "Dict_TriageConfigTypeDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_TriageConfig",
                table: "Dict_TriageConfig");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_TableSetting",
                table: "Dict_TableSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_FastTrackSetting",
                table: "Dict_FastTrackSetting");

            migrationBuilder.RenameTable(
                name: "Dict_TriageDevice",
                newName: "Config_TriageDevice");

            migrationBuilder.RenameTable(
                name: "Dict_TriageConfigTypeDescription",
                newName: "TriageConfigTypeDescription");

            migrationBuilder.RenameTable(
                name: "Dict_TriageConfig",
                newName: "TriageConfig");

            migrationBuilder.RenameTable(
                name: "Dict_TableSetting",
                newName: "Config_TableSetting");

            migrationBuilder.RenameTable(
                name: "Dict_FastTrackSetting",
                newName: "Config_FastTrackSetting");

            migrationBuilder.AlterTable(
                name: "TriageConfigTypeDescription",
                oldComment: "分诊字典类型");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Triage_PatientInfo",
                type: "datetime2",
                nullable: false,
                comment: "分诊时间",
                oldClrType: typeof(DateTime),
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigTypeName",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "分诊设置类型名称");

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigTypeCode",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "分诊设置类型代码");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "TriageConfigTypeDescription",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "排序");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "修改人");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "TriageConfigTypeDescription",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TriageConfigTypeDescription",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false,
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<string>(
                name: "HospitalName",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "医院名称");

            migrationBuilder.AlterColumn<string>(
                name: "HospitalCode",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "医院编码");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField5",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段5");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField4",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段4");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField3",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段3");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField2",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段2");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField1",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "TriageConfigTypeDescription",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "删除时间");

            migrationBuilder.AlterColumn<string>(
                name: "DeleteUser",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "删除人");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "TriageConfigTypeDescription",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<string>(
                name: "AddUser",
                table: "TriageConfigTypeDescription",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "添加人");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Config_TriageDevice",
                table: "Config_TriageDevice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TriageConfigTypeDescription",
                table: "TriageConfigTypeDescription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TriageConfig",
                table: "TriageConfig",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Config_TableSetting",
                table: "Config_TableSetting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Config_FastTrackSetting",
                table: "Config_FastTrackSetting",
                column: "Id");
        }
    }
}
