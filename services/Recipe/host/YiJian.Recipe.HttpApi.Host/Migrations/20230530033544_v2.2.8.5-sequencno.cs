using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class v2285sequencno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "RC_SettingPara",
                comment: "会诊配置");

            migrationBuilder.AlterTable(
                name: "RC_PackageProject",
                comment: "医嘱套餐-项目");

            migrationBuilder.AlterTable(
                name: "RC_MedicalTechnologyMap",
                comment: "医技映射字典，包括检查，检验，诊疗");

            migrationBuilder.AlterTable(
                name: "RC_DrugStockQuery",
                comment: "库存相关的信息");

            migrationBuilder.AlterTable(
                name: "RC_DoctorSummary",
                comment: "急诊医生会议纪要");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuickStartAdviceId",
                table: "RC_QuickStartMedicine",
                type: "uniqueidentifier",
                nullable: false,
                comment: "快速开嘱医嘱信息Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCriticalPrescription",
                table: "RC_QuickStartMedicine",
                type: "bit",
                nullable: false,
                comment: "是否危急药",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultDosageUnit",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(max)",
                nullable: true,
                comment: "默认规格剂量单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "RC_QuickStartAdvice",
                type: "int",
                nullable: false,
                comment: "排序序号",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuickStartCatalogueId",
                table: "RC_QuickStartAdvice",
                type: "uniqueidentifier",
                nullable: false,
                comment: "快速开嘱的目录Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPrintAgain",
                table: "RC_PrintInfo",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否是再次打印(补打)",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorsAdviceId",
                table: "RC_Prescription",
                type: "uniqueidentifier",
                nullable: false,
                comment: "医嘱Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "RC_PackageProject",
                type: "int",
                nullable: false,
                comment: "排序",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "RC_PackageProject",
                type: "decimal(18,2)",
                nullable: false,
                comment: "价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCriticalPrescription",
                table: "RC_PackageProject",
                type: "bit",
                nullable: false,
                comment: "是否处方权",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeName",
                table: "RC_PackageProject",
                type: "nvarchar(max)",
                nullable: true,
                comment: "费别名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChargeCode",
                table: "RC_PackageProject",
                type: "nvarchar(max)",
                nullable: true,
                comment: "费别代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LPTId",
                table: "RC_MedicalTechnologyMap",
                type: "uniqueidentifier",
                nullable: false,
                comment: "医技映射字典，包括检查，检验，诊疗外检",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ItemType",
                table: "RC_MedicalTechnologyMap",
                type: "int",
                nullable: false,
                comment: "医嘱各项分类: 0=药品,1=检查项,2=检验项,3=诊疗项",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Storage",
                table: "RC_DrugStockQuery",
                type: "int",
                nullable: false,
                comment: "药房编码",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SequenceNo",
                table: "RC_DoctorsAdvice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "单号序号，同一个处方单，单号从1开始递增");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SequenceNo",
                table: "RC_DoctorsAdvice");

            migrationBuilder.AlterTable(
                name: "RC_SettingPara",
                oldComment: "会诊配置");

            migrationBuilder.AlterTable(
                name: "RC_PackageProject",
                oldComment: "医嘱套餐-项目");

            migrationBuilder.AlterTable(
                name: "RC_MedicalTechnologyMap",
                oldComment: "医技映射字典，包括检查，检验，诊疗");

            migrationBuilder.AlterTable(
                name: "RC_DrugStockQuery",
                oldComment: "库存相关的信息");

            migrationBuilder.AlterTable(
                name: "RC_DoctorSummary",
                oldComment: "急诊医生会议纪要");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuickStartAdviceId",
                table: "RC_QuickStartMedicine",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "快速开嘱医嘱信息Id");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCriticalPrescription",
                table: "RC_QuickStartMedicine",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否危急药");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultDosageUnit",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "默认规格剂量单位");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "RC_QuickStartAdvice",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序序号");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuickStartCatalogueId",
                table: "RC_QuickStartAdvice",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "快速开嘱的目录Id");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPrintAgain",
                table: "RC_PrintInfo",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "是否是再次打印(补打)");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorsAdviceId",
                table: "RC_Prescription",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "医嘱Id");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "RC_PackageProject",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "RC_PackageProject",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "价格");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCriticalPrescription",
                table: "RC_PackageProject",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否处方权");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeName",
                table: "RC_PackageProject",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "费别名称");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeCode",
                table: "RC_PackageProject",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "费别代码");

            migrationBuilder.AlterColumn<Guid>(
                name: "LPTId",
                table: "RC_MedicalTechnologyMap",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "医技映射字典，包括检查，检验，诊疗外检");

            migrationBuilder.AlterColumn<int>(
                name: "ItemType",
                table: "RC_MedicalTechnologyMap",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱各项分类: 0=药品,1=检查项,2=检验项,3=诊疗项");

            migrationBuilder.AlterColumn<int>(
                name: "Storage",
                table: "RC_DrugStockQuery",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "药房编码");
        }
    }
}
