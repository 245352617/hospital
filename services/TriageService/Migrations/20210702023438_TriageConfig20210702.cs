using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class TriageConfig20210702 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "TriageConfig",
                comment: "分诊字典");

            migrationBuilder.AlterColumn<int>(
                name: "TriageConfigType",
                table: "TriageConfig",
                nullable: false,
                comment: "分诊设置类型 1001:绿色通道 1002:群伤事件 1003:院前分诊评分类型 1004:来院方式  1005:科室配置 1006:院前分诊去向",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigName",
                table: "TriageConfig",
                type: "nvarchar(50)",
                nullable: true,
                comment: "分诊设置名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigCode",
                table: "TriageConfig",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊设置代码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "TriageConfig",
                nullable: true,
                comment: "排序",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "TriageConfig",
                maxLength: 256,
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Py",
                table: "TriageConfig",
                type: "varchar(50)",
                nullable: true,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "TriageConfig",
                maxLength: 50,
                nullable: true,
                comment: "修改人",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "TriageConfig",
                nullable: true,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IsDisable",
                table: "TriageConfig",
                nullable: false,
                comment: "是否启用 0：不启用 1：启用",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TriageConfig",
                nullable: false,
                defaultValue: false,
                comment: "是否删除",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalName",
                table: "TriageConfig",
                maxLength: 250,
                nullable: true,
                comment: "医院名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalCode",
                table: "TriageConfig",
                maxLength: 250,
                nullable: true,
                comment: "医院编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField5",
                table: "TriageConfig",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段5",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField4",
                table: "TriageConfig",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段4",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField3",
                table: "TriageConfig",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段3",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField2",
                table: "TriageConfig",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段2",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField1",
                table: "TriageConfig",
                maxLength: 1000,
                nullable: true,
                comment: "扩展字段1",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "TriageConfig",
                nullable: true,
                comment: "删除时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteUser",
                table: "TriageConfig",
                maxLength: 50,
                nullable: true,
                comment: "删除人",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "TriageConfig",
                nullable: false,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "AddUser",
                table: "TriageConfig",
                maxLength: 50,
                nullable: true,
                comment: "添加人",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TriageConfig_TriageConfigCode",
                table: "TriageConfig",
                column: "TriageConfigCode",
                unique: true,
                filter: "[TriageConfigCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TriageConfig_TriageConfigCode",
                table: "TriageConfig");

            migrationBuilder.AlterTable(
                name: "TriageConfig",
                oldComment: "分诊字典");

            migrationBuilder.AlterColumn<int>(
                name: "TriageConfigType",
                table: "TriageConfig",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "分诊设置类型 1001:绿色通道 1002:群伤事件 1003:院前分诊评分类型 1004:来院方式  1005:科室配置 1006:院前分诊去向");

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigName",
                table: "TriageConfig",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "分诊设置名称");

            migrationBuilder.AlterColumn<string>(
                name: "TriageConfigCode",
                table: "TriageConfig",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "分诊设置代码");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "TriageConfig",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "排序");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "TriageConfig",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "Py",
                table: "TriageConfig",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "ModUser",
                table: "TriageConfig",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "修改人");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationTime",
                table: "TriageConfig",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<int>(
                name: "IsDisable",
                table: "TriageConfig",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "是否启用 0：不启用 1：启用");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TriageConfig",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false,
                oldComment: "是否删除");

            migrationBuilder.AlterColumn<string>(
                name: "HospitalName",
                table: "TriageConfig",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "医院名称");

            migrationBuilder.AlterColumn<string>(
                name: "HospitalCode",
                table: "TriageConfig",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "医院编码");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField5",
                table: "TriageConfig",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段5");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField4",
                table: "TriageConfig",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段4");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField3",
                table: "TriageConfig",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段3");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField2",
                table: "TriageConfig",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段2");

            migrationBuilder.AlterColumn<string>(
                name: "ExtensionField1",
                table: "TriageConfig",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "扩展字段1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "TriageConfig",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "删除时间");

            migrationBuilder.AlterColumn<string>(
                name: "DeleteUser",
                table: "TriageConfig",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "删除人");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "TriageConfig",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<string>(
                name: "AddUser",
                table: "TriageConfig",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "添加人");
        }
    }
}
