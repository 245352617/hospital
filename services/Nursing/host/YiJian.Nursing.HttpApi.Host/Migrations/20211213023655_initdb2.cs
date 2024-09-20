using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class initdb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelOperator",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "CancelTime",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "ExecOperator",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "ExecTime",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "RecipeExecId",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "ExecTime",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "Property",
                table: "NursingPrescribes");

            migrationBuilder.AlterColumn<int>(
                name: "OperationType",
                table: "NursingRecipeExecHistory",
                type: "int",
                nullable: false,
                comment: "操作类型",
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "PlanExcuteTime",
                table: "NursingRecipeExecHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "计划执行时间");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                table: "NursingRecipeExecHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "医嘱Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "NursingRecipeExecHistory",
                type: "datetime2",
                nullable: true,
                comment: "开始时间");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeNo",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "医嘱号",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "TraineeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "管培生代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Trainee",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "管培生",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalFee",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "总费用",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "StopDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "停嘱医生代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StopDoctor",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "停嘱医生",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StopDateTime",
                table: "NursingRecipe",
                type: "datetime2",
                maxLength: 20,
                nullable: true,
                comment: "停嘱时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RecipeNo",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "医嘱号",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "RecipeName",
                table: "NursingRecipe",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "RecipeFeeStatusCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "收费状态码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "RecipeFeeStatus",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "收费状态",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "RecipeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "RecipeCategoryCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医嘱项目分类编码 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "RecipeCategory",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "单价",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PrescriptionNo",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "处方号",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "PayType",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "付费类型  0 自费项目 1 医保项目",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "PIID",
                table: "NursingRecipe",
                type: "uniqueidentifier",
                nullable: false,
                comment: "病人标识",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsChronicDisease",
                table: "NursingRecipe",
                type: "bit",
                nullable: true,
                comment: "慢性病标识",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsBackTracking",
                table: "NursingRecipe",
                type: "bit",
                nullable: false,
                comment: "是否补录",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Insurance",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "医保目录：0-自费项目1-医保项目(甲、乙)",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "HisOrderNo",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "HIS医嘱号",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ExecDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "BussinessCatalog",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "业务类型",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplyTime",
                table: "NursingRecipe",
                type: "datetime2",
                nullable: false,
                comment: "开嘱时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "开嘱医生编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctor",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "开嘱医生",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "开嘱科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "开嘱科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "RecipeSubNo",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医嘱子号：药物为1.2.3...其它项目取默认值");

            migrationBuilder.AlterColumn<string>(
                name: "UsageCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "用法编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Usage",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "用法",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "NursingPrescribes",
                type: "nvarchar(max)",
                nullable: true,
                comment: "领量单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ToxicProperty",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                comment: "药理等级：毒、麻、精一、精二",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "NursingPrescribes",
                type: "datetime2",
                nullable: true,
                comment: "开始时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Speed",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "滴速",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "包装规格",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "SkinTestResult",
                table: "NursingPrescribes",
                type: "bit",
                maxLength: 20,
                nullable: true,
                comment: "皮试结果 0-阴性 1-阳性",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "NursingPrescribes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "医嘱说明",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RecipeId",
                table: "NursingPrescribes",
                type: "uniqueidentifier",
                nullable: false,
                comment: "医嘱主表ID",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeGroupNo",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                comment: "医嘱分组号：分组顺序1,2,3",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "QtyPerTime",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                maxLength: 20,
                nullable: true,
                comment: "每次用量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrescribeTypeCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医嘱类型编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PrescribeType",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "医嘱类型：临嘱、长嘱、出院带药等",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PositionCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "位置编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "位置",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "药房编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pharmacy",
                table: "NursingPrescribes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "药房",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LongDays",
                table: "NursingPrescribes",
                type: "int",
                maxLength: 20,
                nullable: false,
                comment: "开药天数",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSkinTest",
                table: "NursingPrescribes",
                type: "bit",
                maxLength: 20,
                nullable: true,
                comment: "是否皮试 0 不需要皮试 1 需要皮试",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsOutDrug",
                table: "NursingPrescribes",
                type: "bit",
                nullable: false,
                comment: "是否自备药：0-非自备药1-自备药",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAmendedMark",
                table: "NursingPrescribes",
                type: "bit",
                nullable: false,
                comment: "是否抢救后补：0-非抢救后补1-抢救后补",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdaptationDisease",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "是否医保适应症",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "周期单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FrequencyTimes",
                table: "NursingPrescribes",
                type: "int",
                nullable: true,
                comment: "在一个周期内执行的次数",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyName",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "频次",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyExecDayTimes",
                table: "NursingPrescribes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "一天内的执行时间",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "频次码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FactoryCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "厂家代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Factory",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "厂家",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirDate",
                table: "NursingPrescribes",
                type: "datetime2",
                nullable: true,
                comment: "失效期",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "NursingPrescribes",
                type: "datetime2",
                nullable: true,
                comment: "结束时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "剂量单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "BigPackUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "包装单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "BatchNo",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "批次号",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "NursingPrescribes",
                type: "int",
                maxLength: 20,
                nullable: false,
                comment: "领量",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "ActualDays",
                table: "NursingPrescribes",
                type: "int",
                maxLength: 20,
                nullable: true,
                comment: "实际天数",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicineProperty",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "药物属性：西药、中药、西药制剂、中药制剂");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PlanExcuteTime",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "RecipeSubNo",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "MedicineProperty",
                table: "NursingPrescribes");

            migrationBuilder.AlterColumn<int>(
                name: "OperationType",
                table: "NursingRecipeExecHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "操作类型");

            migrationBuilder.AddColumn<string>(
                name: "CancelOperator",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelTime",
                table: "NursingRecipeExecHistory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExecOperator",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExecTime",
                table: "NursingRecipeExecHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeExecId",
                table: "NursingRecipeExecHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "RecipeNo",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "医嘱号");

            migrationBuilder.AlterColumn<string>(
                name: "TraineeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "管培生代码");

            migrationBuilder.AlterColumn<string>(
                name: "Trainee",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "管培生");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalFee",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "总费用");

            migrationBuilder.AlterColumn<string>(
                name: "StopDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "停嘱医生代码");

            migrationBuilder.AlterColumn<string>(
                name: "StopDoctor",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "停嘱医生");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StopDateTime",
                table: "NursingRecipe",
                type: "datetime2",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "停嘱时间");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeNo",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "医嘱号");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeName",
                table: "NursingRecipe",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeFeeStatusCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "收费状态码");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeFeeStatus",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "收费状态");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "编码");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeCategoryCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "医嘱项目分类编码 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeCategory",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "单价");

            migrationBuilder.AlterColumn<string>(
                name: "PrescriptionNo",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "处方号");

            migrationBuilder.AlterColumn<int>(
                name: "PayType",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "付费类型  0 自费项目 1 医保项目");

            migrationBuilder.AlterColumn<Guid>(
                name: "PIID",
                table: "NursingRecipe",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "病人标识");

            migrationBuilder.AlterColumn<bool>(
                name: "IsChronicDisease",
                table: "NursingRecipe",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "慢性病标识");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBackTracking",
                table: "NursingRecipe",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否补录");

            migrationBuilder.AlterColumn<int>(
                name: "Insurance",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医保目录：0-自费项目1-医保项目(甲、乙)");

            migrationBuilder.AlterColumn<string>(
                name: "HisOrderNo",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "HIS医嘱号");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行科室");

            migrationBuilder.AlterColumn<int>(
                name: "BussinessCatalog",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "业务类型");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplyTime",
                table: "NursingRecipe",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "开嘱时间");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "开嘱医生编码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctor",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "开嘱医生");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "开嘱科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "开嘱科室");

            migrationBuilder.AlterColumn<string>(
                name: "UsageCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "用法编码");

            migrationBuilder.AlterColumn<string>(
                name: "Usage",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "用法");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "NursingPrescribes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "领量单位");

            migrationBuilder.AlterColumn<int>(
                name: "ToxicProperty",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "药理等级：毒、麻、精一、精二");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "NursingPrescribes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "开始时间");

            migrationBuilder.AlterColumn<string>(
                name: "Speed",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "滴速");

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "包装规格");

            migrationBuilder.AlterColumn<bool>(
                name: "SkinTestResult",
                table: "NursingPrescribes",
                type: "bit",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "皮试结果 0-阴性 1-阳性");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "NursingPrescribes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "医嘱说明");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecipeId",
                table: "NursingPrescribes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "医嘱主表ID");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeGroupNo",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱分组号：分组顺序1,2,3");

            migrationBuilder.AlterColumn<decimal>(
                name: "QtyPerTime",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "每次用量");

            migrationBuilder.AlterColumn<string>(
                name: "PrescribeTypeCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "医嘱类型编码");

            migrationBuilder.AlterColumn<string>(
                name: "PrescribeType",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "医嘱类型：临嘱、长嘱、出院带药等");

            migrationBuilder.AlterColumn<string>(
                name: "PositionCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "位置编码");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "位置");

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "药房编码");

            migrationBuilder.AlterColumn<string>(
                name: "Pharmacy",
                table: "NursingPrescribes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "药房");

            migrationBuilder.AlterColumn<int>(
                name: "LongDays",
                table: "NursingPrescribes",
                type: "int",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldComment: "开药天数");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSkinTest",
                table: "NursingPrescribes",
                type: "bit",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "是否皮试 0 不需要皮试 1 需要皮试");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOutDrug",
                table: "NursingPrescribes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否自备药：0-非自备药1-自备药");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAmendedMark",
                table: "NursingPrescribes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否抢救后补：0-非抢救后补1-抢救后补");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdaptationDisease",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "是否医保适应症");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "周期单位");

            migrationBuilder.AlterColumn<int>(
                name: "FrequencyTimes",
                table: "NursingPrescribes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "在一个周期内执行的次数");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyName",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "频次");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyExecDayTimes",
                table: "NursingPrescribes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "一天内的执行时间");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "频次码");

            migrationBuilder.AlterColumn<string>(
                name: "FactoryCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "厂家代码");

            migrationBuilder.AlterColumn<string>(
                name: "Factory",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "厂家");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirDate",
                table: "NursingPrescribes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "失效期");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "NursingPrescribes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "结束时间");

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "剂量单位");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "每次剂量");

            migrationBuilder.AlterColumn<string>(
                name: "BigPackUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "包装单位");

            migrationBuilder.AlterColumn<string>(
                name: "BatchNo",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "批次号");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "NursingPrescribes",
                type: "int",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldComment: "领量");

            migrationBuilder.AlterColumn<int>(
                name: "ActualDays",
                table: "NursingPrescribes",
                type: "int",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "实际天数");

            migrationBuilder.AddColumn<string>(
                name: "ExecTime",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Property",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
