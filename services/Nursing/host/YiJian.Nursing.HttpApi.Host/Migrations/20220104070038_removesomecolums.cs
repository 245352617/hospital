using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class removesomecolums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecipePrinted",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "StopDateTime",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "AntibioticPermission",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "BigPackFactor",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "BigPackPrice",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "BigPackUnit",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "ExpirDate",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "FactoryCode",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "FactoryName",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "IsBindingTreat",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "PrescriptionPermission",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "SmallPackFactor",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "SmallPackPrice",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "SmallPackUnit",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "Unpack",
                table: "NursingPrescribe");

            migrationBuilder.RenameColumn(
                name: "HasReportName",
                table: "NursingLis",
                newName: "HasReport");

            migrationBuilder.AlterColumn<Guid>(
                name: "PIID",
                table: "NursingRecipe",
                type: "uniqueidentifier",
                nullable: false,
                comment: "患者入科流水号",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "患者唯一标识");

            migrationBuilder.AddColumn<int>(
                name: "PrintedTimes",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "打印次数");

            migrationBuilder.AddColumn<DateTime>(
                name: "StopOptTime",
                table: "NursingRecipe",
                type: "datetime2",
                nullable: true,
                comment: "停嘱操作时间");

            migrationBuilder.AddColumn<DateTime>(
                name: "StopTime",
                table: "NursingRecipe",
                type: "datetime2",
                nullable: true,
                comment: "停嘱生效时间");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyName",
                table: "NursingPrescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "频次",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "频次");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrintedTimes",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "StopOptTime",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "StopTime",
                table: "NursingRecipe");

            migrationBuilder.RenameColumn(
                name: "HasReport",
                table: "NursingLis",
                newName: "HasReportName");

            migrationBuilder.AlterColumn<Guid>(
                name: "PIID",
                table: "NursingRecipe",
                type: "uniqueidentifier",
                nullable: false,
                comment: "患者唯一标识",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "患者入科流水号");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecipePrinted",
                table: "NursingRecipe",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否打印过");

            migrationBuilder.AddColumn<DateTime>(
                name: "StopDateTime",
                table: "NursingRecipe",
                type: "datetime2",
                maxLength: 20,
                nullable: true,
                comment: "停嘱时间");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "单位");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyName",
                table: "NursingPrescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "频次",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "频次");

            migrationBuilder.AddColumn<int>(
                name: "AntibioticPermission",
                table: "NursingPrescribe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "抗生素权限");

            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                table: "NursingPrescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "批次号");

            migrationBuilder.AddColumn<int>(
                name: "BigPackFactor",
                table: "NursingPrescribe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "大包装系数(拆零系数)");

            migrationBuilder.AddColumn<decimal>(
                name: "BigPackPrice",
                table: "NursingPrescribe",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "包装价格");

            migrationBuilder.AddColumn<string>(
                name: "BigPackUnit",
                table: "NursingPrescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "包装单位");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirDate",
                table: "NursingPrescribe",
                type: "datetime2",
                nullable: true,
                comment: "失效期");

            migrationBuilder.AddColumn<string>(
                name: "FactoryCode",
                table: "NursingPrescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "厂家代码");

            migrationBuilder.AddColumn<string>(
                name: "FactoryName",
                table: "NursingPrescribe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "厂家名称");

            migrationBuilder.AddColumn<bool>(
                name: "IsBindingTreat",
                table: "NursingPrescribe",
                type: "bit",
                nullable: true,
                comment: "用于判断关联耗材是否手动删除");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionPermission",
                table: "NursingPrescribe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "处方权");

            migrationBuilder.AddColumn<int>(
                name: "SmallPackFactor",
                table: "NursingPrescribe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "小包装系数(拆零系数)");

            migrationBuilder.AddColumn<decimal>(
                name: "SmallPackPrice",
                table: "NursingPrescribe",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "小包装单价");

            migrationBuilder.AddColumn<string>(
                name: "SmallPackUnit",
                table: "NursingPrescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "小包装单位");

            migrationBuilder.AddColumn<int>(
                name: "Unpack",
                table: "NursingPrescribe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "门诊拆分属性 0=最小单位总量取整 1=包装单位总量取整 2=最小单位每次取整 3=包装单位每次取整 4=最小单位可拆分");
        }
    }
}
