using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class initdb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NursingPrescribes_NursingRecipe_RecipeId",
                table: "NursingPrescribes");

            migrationBuilder.DropTable(
                name: "NursingLisTargets");

            migrationBuilder.DropTable(
                name: "NursingPacsTargets");

            migrationBuilder.DropTable(
                name: "NursingTreats");

            migrationBuilder.DropTable(
                name: "NursingLis");

            migrationBuilder.DropTable(
                name: "NursingPacs");

            migrationBuilder.DropIndex(
                name: "IX_NursingPrescribes_RecipeId",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "BussinessCatalogCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "BussinessCatalogDescription",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ExecStatus",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ExecStatusCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "InsuranceCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "InsuranceDescription",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "IsReceiptPrinted",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "IsRecipePrinted",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "PayTypeCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "PayTypeDescrition",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "PrescribeType",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "PrescribeTypeCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "RePrintReceiptCount",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "RePrintReceiptTime",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "RePrintReceiptor",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "RecipeSubNo",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "SignCert",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "SignFlow",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "SignValue",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "StatusDescription",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "TimestampValue",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "FrequencyRatio",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "IsBindingTreat",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "ToxicPropertyCode",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "ToxicPropertyDescription",
                table: "NursingPrescribes");

            migrationBuilder.AlterTable(
                name: "NursingRecipe",
                comment: "医嘱");

            migrationBuilder.AlterTable(
                name: "NursingPrescribes",
                comment: "药物",
                oldComment: "处方业务");

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
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "收费状态码");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeFeeStatus",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
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
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
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
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
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
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "执行科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
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
                oldComment: "开立时间");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "申请医生编码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctor",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "申请医生");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "申请科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "申请科室");

            migrationBuilder.AlterColumn<string>(
                name: "UsageCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "用法编码");

            migrationBuilder.AlterColumn<string>(
                name: "Usage",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "用法");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "NursingPrescribes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
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

            migrationBuilder.AlterColumn<decimal>(
                name: "QtyPerTime",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldMaxLength: 20,
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true,
                oldComment: "每次用量");

            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "药物属性：西药、中药、西药制剂、中药制剂");

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
                name: "FrequencyCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "频次码");

            migrationBuilder.AlterColumn<string>(
                name: "FactoryCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "厂家代码");

            migrationBuilder.AlterColumn<string>(
                name: "Factory",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
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

            migrationBuilder.AlterColumn<string>(
                name: "ExecTime",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "执行时间：例如13:00.");

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "剂量单位");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "每次计量");

            migrationBuilder.AlterColumn<string>(
                name: "BigPackUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
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

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "NursingPrescribes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrequencyExecDayTimes",
                table: "NursingPrescribes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrequencyName",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FrequencyTimes",
                table: "NursingPrescribes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrequencyUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrescribeType",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeTypeCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RecipeGroupNo",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "NursingPrescribes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "NursingPrescribes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NursingRecipeExec",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanExcuteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanExcuteHours = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TotalDosage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Dosage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DosageUnit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IfCalcIn = table.Column<int>(type: "int", nullable: true),
                    SortNum = table.Column<int>(type: "int", nullable: false),
                    NursingStatus = table.Column<int>(type: "int", nullable: false),
                    CheckNurseCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CheckNurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CheckTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExecuteType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ExcuteNurseCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ExcuteNurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExcuteNurseTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishNurseCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FinishNurse = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipeExec", x => x.Id);
                },
                comment: "医嘱执行");

            migrationBuilder.CreateTable(
                name: "NursingRecipeExecHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    ExecTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecOperator = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CancelTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelOperator = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RecipeExecId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipeExecHistory", x => x.Id);
                },
                comment: "医嘱执行历史记录");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingRecipeExec");

            migrationBuilder.DropTable(
                name: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "FrequencyExecDayTimes",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "FrequencyName",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "FrequencyTimes",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "FrequencyUnit",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "PrescribeType",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "PrescribeTypeCode",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "RecipeGroupNo",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "NursingPrescribes");

            migrationBuilder.AlterTable(
                name: "NursingRecipe",
                oldComment: "医嘱");

            migrationBuilder.AlterTable(
                name: "NursingPrescribes",
                comment: "处方业务",
                oldComment: "药物");

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
                nullable: false,
                comment: "收费状态码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "RecipeFeeStatus",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
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
                nullable: false,
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
                nullable: false,
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
                nullable: false,
                comment: "执行科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ExecDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
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
                comment: "开立时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "申请医生编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctor",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "申请医生",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "申请科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "申请科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "BussinessCatalogCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "业务类型编码");

            migrationBuilder.AddColumn<string>(
                name: "BussinessCatalogDescription",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "业务类型描述");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "NursingRecipe",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "",
                comment: "临床诊断（冗余设计）");

            migrationBuilder.AddColumn<string>(
                name: "ExecStatus",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "执行状态");

            migrationBuilder.AddColumn<string>(
                name: "ExecStatusCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "执行状态码");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医保目录编码");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceDescription",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保目录描述");

            migrationBuilder.AddColumn<bool>(
                name: "IsReceiptPrinted",
                table: "NursingRecipe",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "申请单打印标识");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecipePrinted",
                table: "NursingRecipe",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "医嘱单打印标识");

            migrationBuilder.AddColumn<string>(
                name: "PayTypeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "付费类型编码");

            migrationBuilder.AddColumn<string>(
                name: "PayTypeDescrition",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "付费类型描述：自费项目 医保项目");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeType",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱类型：字典、临嘱、长嘱、出院带药");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeTypeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱类型编码");

            migrationBuilder.AddColumn<int>(
                name: "RePrintReceiptCount",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "补打次数 默认0次");

            migrationBuilder.AddColumn<DateTime>(
                name: "RePrintReceiptTime",
                table: "NursingRecipe",
                type: "datetime2",
                nullable: true,
                comment: "补打时间");

            migrationBuilder.AddColumn<string>(
                name: "RePrintReceiptor",
                table: "NursingRecipe",
                type: "nvarchar(max)",
                nullable: true,
                comment: "补打印人");

            migrationBuilder.AddColumn<int>(
                name: "RecipeSubNo",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医嘱子号：药物为1.2.3...其它项目取默认值");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "NursingRecipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "医嘱说明");

            migrationBuilder.AddColumn<string>(
                name: "SignCert",
                table: "NursingRecipe",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "签名证书");

            migrationBuilder.AddColumn<string>(
                name: "SignFlow",
                table: "NursingRecipe",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                comment: " Base64 编码格式的签章图片");

            migrationBuilder.AddColumn<string>(
                name: "SignValue",
                table: "NursingRecipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "签名值");

            migrationBuilder.AddColumn<string>(
                name: "StatusCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "状态编码");

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "状态描述");

            migrationBuilder.AddColumn<string>(
                name: "TimestampValue",
                table: "NursingRecipe",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "时间戳值");

            migrationBuilder.AlterColumn<string>(
                name: "UsageCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "用法编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Usage",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "用法",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "NursingPrescribes",
                type: "nvarchar(4000)",
                maxLength: 4000,
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

            migrationBuilder.AlterColumn<decimal>(
                name: "QtyPerTime",
                table: "NursingPrescribes",
                type: "decimal(18,4)",
                maxLength: 20,
                precision: 18,
                scale: 4,
                nullable: true,
                comment: "每次用量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Property",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "药物属性：西药、中药、西药制剂、中药制剂",
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
                nullable: false,
                defaultValue: false,
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
                name: "FrequencyCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "频次码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FactoryCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "厂家代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Factory",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
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

            migrationBuilder.AlterColumn<string>(
                name: "ExecTime",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "执行时间：例如13:00.",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "剂量单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingPrescribes",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "每次计量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "BigPackUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
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
                name: "Frequency",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "频次");

            migrationBuilder.AddColumn<int>(
                name: "FrequencyRatio",
                table: "NursingPrescribes",
                type: "int",
                nullable: true,
                comment: "频次执行系数");

            migrationBuilder.AddColumn<bool>(
                name: "IsBindingTreat",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "用于判断关联耗材是否手动删除");

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialPrice",
                table: "NursingPrescribes",
                type: "decimal(18,4)",
                maxLength: 20,
                precision: 18,
                scale: 4,
                nullable: true,
                comment: "耗材金额");

            migrationBuilder.AddColumn<string>(
                name: "ToxicPropertyCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "药理等级编码：毒、麻、精一、精二");

            migrationBuilder.AddColumn<string>(
                name: "ToxicPropertyDescription",
                table: "NursingPrescribes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "药理等级描述：毒、麻、精一、精二");

            migrationBuilder.CreateTable(
                name: "NursingLis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Catalog = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "检验类别"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检验类别编码"),
                    ClinicalSymptom = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "临床症状"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasReport = table.Column<bool>(type: "bit", nullable: false, comment: "报告标识"),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false, comment: "是否紧急"),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportDT = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "出报告时间"),
                    ReportDoctor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认报告医生"),
                    ReportDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认报告医生编码"),
                    Specimen = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "标本"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "标本编码"),
                    SpecimenCollectDT = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "标本采集时间"),
                    SpecimenContainer = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器"),
                    SpecimenContainerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本容器代码"),
                    SpecimenContainerColor = table.Column<int>(type: "int", maxLength: 20, nullable: true, comment: "标本容器颜色 0 红帽 1 蓝帽 2 紫帽"),
                    SpecimenDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "标本说明"),
                    SpecimenPart = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本采集部位"),
                    SpecimenPartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "标本采集部位编码"),
                    SpecimenReceivedDT = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "标本接收时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingLis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingLis_NursingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "NursingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检验项");

            migrationBuilder.CreateTable(
                name: "NursingPacs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Catalog = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "检查类别"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检查类别编码"),
                    CatalogDisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "分类类型名称 例如心电图申请单、超声申请单"),
                    ClinicalSymptom = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "临床症状"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExamPart = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位"),
                    ExamPartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "检查部位编码"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasReport = table.Column<bool>(type: "bit", nullable: false, comment: "报告标识"),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false, comment: "是否紧急"),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "简要病史"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportDT = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: true, comment: "出报告时间"),
                    ReportDoctor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认报告医生"),
                    ReportDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认报告医生编码")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingPacs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingPacs_NursingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "NursingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检查项");

            migrationBuilder.CreateTable(
                name: "NursingTreats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false, comment: "用量"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "用量单位")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingTreats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingTreats_NursingRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "NursingRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "诊疗项");

            migrationBuilder.CreateTable(
                name: "NursingLisTargets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "数量"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "小项编码"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Insurance = table.Column<int>(type: "int", nullable: false, comment: "医保目录：0-自费项目1-医保项目(甲、乙)"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    InsuranceDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医保目录描述"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "项目标识-外键"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "小项名称"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "单价"),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "小项标识"),
                    TotalFee = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "总费用"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingLisTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingLisTargets_NursingLis_LisId",
                        column: x => x.LisId,
                        principalTable: "NursingLis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检验小项");

            migrationBuilder.CreateTable(
                name: "NursingPacsTargets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "数量"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "小项编码"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Insurance = table.Column<int>(type: "int", nullable: false, comment: "医保目录：0-自费项目1-医保项目(甲、乙)"),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保目录编码"),
                    InsuranceDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医保目录描述"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "小项名称"),
                    PacsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "项目标识-外键"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "单价"),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "小项标识"),
                    TotalFee = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "总费用"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingPacsTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingPacsTargets_NursingPacs_PacsId",
                        column: x => x.PacsId,
                        principalTable: "NursingPacs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "检查小项");

            migrationBuilder.CreateIndex(
                name: "IX_NursingPrescribes_RecipeId",
                table: "NursingPrescribes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingLis_RecipeId",
                table: "NursingLis",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingLisTargets_LisId",
                table: "NursingLisTargets",
                column: "LisId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingPacs_RecipeId",
                table: "NursingPacs",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingPacsTargets_PacsId",
                table: "NursingPacsTargets",
                column: "PacsId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingTreats_RecipeId",
                table: "NursingTreats",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_NursingPrescribes_NursingRecipe_RecipeId",
                table: "NursingPrescribes",
                column: "RecipeId",
                principalTable: "NursingRecipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
