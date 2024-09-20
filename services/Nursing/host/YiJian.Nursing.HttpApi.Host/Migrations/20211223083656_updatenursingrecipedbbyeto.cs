using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class updatenursingrecipedbbyeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NursingRecipe_RecipeCode",
                table: "NursingRecipe");

            migrationBuilder.DropIndex(
                name: "IX_NursingRecipe_RecipeName",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "ApplyDept",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ApplyDoctor",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "BussinessCatalog",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ExecDept",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Insurance",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "RecipeCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "RecipeFeeStatus",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "RecipeFeeStatusCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "RecipeName",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Trainee",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "Factory",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "HospitalCode",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "IsProHospitalUse",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "Pharmacy",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "PositionCode",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "QtyPerTime",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "RecipeGroupNo",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "NursingPrescribes");

            migrationBuilder.RenameColumn(
                name: "StopDoctor",
                table: "NursingRecipe",
                newName: "StopDoctorName");

            migrationBuilder.RenameColumn(
                name: "RecipeCategory",
                table: "NursingRecipe",
                newName: "RecipeCategoryName");

            migrationBuilder.RenameColumn(
                name: "PrescribeType",
                table: "NursingPrescribes",
                newName: "PrescribeTypeName");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalFee",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "总费用",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "总费用");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeGroupNo",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "医嘱子号",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱子号：药物为1.2.3...其它项目取默认值");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeCategoryCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱项目分类编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "医嘱项目分类编码 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "单价",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "单价");

            migrationBuilder.AlterColumn<int>(
                name: "PayType",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "付费类型: 0=自费,1=医保,2=其它",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "付费类型  0 自费项目 1 医保项目");

            migrationBuilder.AlterColumn<Guid>(
                name: "PIID",
                table: "NursingRecipe",
                type: "uniqueidentifier",
                nullable: false,
                comment: "患者唯一标识",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "病人标识");

            migrationBuilder.AlterColumn<bool>(
                name: "IsChronicDisease",
                table: "NursingRecipe",
                type: "bit",
                nullable: true,
                comment: "是否慢性病",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "慢性病标识");

            migrationBuilder.AlterColumn<string>(
                name: "Diagnosis",
                table: "NursingRecipe",
                type: "nvarchar(max)",
                nullable: true,
                comment: "临床诊断",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true,
                oldComment: "临床诊断");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "申请医生编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "开嘱医生编码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "申请科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "开嘱科室编码");

            migrationBuilder.AddColumn<string>(
                name: "ApplyDeptName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "申请科室");

            migrationBuilder.AddColumn<string>(
                name: "ApplyDoctorName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "申请医生");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱编码");

            migrationBuilder.AddColumn<string>(
                name: "ExecDeptName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室名称");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExecTime",
                table: "NursingRecipe",
                type: "datetime2",
                nullable: true,
                comment: "执行时间");

            migrationBuilder.AddColumn<string>(
                name: "ExecutorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行者编码");

            migrationBuilder.AddColumn<string>(
                name: "ExecutorName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行者名称");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医保目录编码");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceType",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医保目录:0=自费,1=甲类,2=乙类,3=其它");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecipePrinted",
                table: "NursingRecipe",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否打印过");

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "NursingRecipe",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "医嘱名称");

            migrationBuilder.AddColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "患者Id");

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "NursingRecipe",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "患者名称");

            migrationBuilder.AddColumn<string>(
                name: "PayTypeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "付费类型编码");

            migrationBuilder.AddColumn<int>(
                name: "PlatformType",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "系统标识: 0=急诊，1=院前");

            migrationBuilder.AddColumn<string>(
                name: "TraineeName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "管培生名称");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "单位");

            migrationBuilder.AlterColumn<string>(
                name: "UsageCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "用法编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "用法编码");

            migrationBuilder.AlterColumn<string>(
                name: "ToxicProperty",
                table: "NursingPrescribes",
                type: "nvarchar(max)",
                nullable: false,
                comment: "药理等级",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "药理等级：毒、麻、精一、精二");

            migrationBuilder.AlterColumn<bool>(
                name: "SkinTestResult",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "皮试结果:false=阴性 ture=阳性",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "皮试结果 0-阴性 1-阳性");

            migrationBuilder.AlterColumn<string>(
                name: "PrescribeTypeCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱类型编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "医嘱类型编码");

            migrationBuilder.AlterColumn<string>(
                name: "MedicineProperty",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "药物属性：西药、中药、西药制剂、中药制剂",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "药物属性：西药、中药、西药制剂、中药制剂");

            migrationBuilder.AlterColumn<int>(
                name: "LongDays",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "开药天数",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "开药天数");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSkinTest",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "是否皮试 false=不需要皮试 true=需要皮试",
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
                defaultValue: false,
                comment: "是否自备药：false=非自备药,true=自备药",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "是否自备药：0-非自备药1-自备药");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAmendedMark",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "是否抢救后补：false=非抢救后补，true=抢救后补",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "是否抢救后补：0-非抢救后补1-抢救后补");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "周期单位");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyName",
                table: "NursingPrescribes",
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

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "频次码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "频次码");

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "剂量单位",
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
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "每次剂量");

            migrationBuilder.AddColumn<int>(
                name: "BigPackFactor",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "大包装系数(拆零系数)");

            migrationBuilder.AddColumn<decimal>(
                name: "BigPackPrice",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "包装价格");

            migrationBuilder.AddColumn<string>(
                name: "FactoryName",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "厂家名称");

            migrationBuilder.AddColumn<bool>(
                name: "IsBindingTreat",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "用于判断关联耗材是否手动删除");

            migrationBuilder.AddColumn<decimal>(
                name: "MaterialPrice",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                maxLength: 20,
                nullable: true,
                comment: "耗材金额");

            migrationBuilder.AddColumn<string>(
                name: "PharmacyName",
                table: "NursingPrescribes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "药房名称");

            migrationBuilder.AddColumn<decimal>(
                name: "QtyPerTimes",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                nullable: true,
                comment: "每次用量");

            migrationBuilder.AddColumn<int>(
                name: "RecieveQty",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "领量(数量)");

            migrationBuilder.AddColumn<string>(
                name: "RecieveUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "领量单位");

            migrationBuilder.AddColumn<int>(
                name: "SmallPackFactor",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "小包装系数(拆零系数)");

            migrationBuilder.AddColumn<decimal>(
                name: "SmallPackPrice",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "小包装单价");

            migrationBuilder.AddColumn<string>(
                name: "SmallPackUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "小包装单位");

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

            migrationBuilder.AddColumn<int>(
                name: "Unpack",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "门诊拆分属性 0=最小单位总量取整 1=包装单位总量取整 2=最小单位每次取整 3=包装单位每次取整 4=最小单位可拆分");

            migrationBuilder.AddColumn<string>(
                name: "UsageName",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "用法名称");

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipe_Code",
                table: "NursingRecipe",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipe_Name",
                table: "NursingRecipe",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NursingRecipe_Code",
                table: "NursingRecipe");

            migrationBuilder.DropIndex(
                name: "IX_NursingRecipe_Name",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ApplyDeptName",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ApplyDoctorName",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ExecDeptName",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ExecTime",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ExecutorCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ExecutorName",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "InsuranceCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "InsuranceType",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "IsRecipePrinted",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "NursingStatus",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "PayTypeCode",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "PlatformType",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "TraineeName",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "BigPackFactor",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "BigPackPrice",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "FactoryName",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "IsBindingTreat",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "MaterialPrice",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "PharmacyName",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "QtyPerTimes",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "RecieveQty",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "RecieveUnit",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "SmallPackFactor",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "SmallPackPrice",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "SmallPackUnit",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "StopDateTime",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "StopDoctorCode",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "StopDoctorName",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "Unpack",
                table: "NursingPrescribes");

            migrationBuilder.DropColumn(
                name: "UsageName",
                table: "NursingPrescribes");

            migrationBuilder.RenameColumn(
                name: "StopDoctorName",
                table: "NursingRecipe",
                newName: "StopDoctor");

            migrationBuilder.RenameColumn(
                name: "RecipeCategoryName",
                table: "NursingRecipe",
                newName: "RecipeCategory");

            migrationBuilder.RenameColumn(
                name: "PrescribeTypeName",
                table: "NursingPrescribes",
                newName: "PrescribeType");

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "NursingRecipeExec",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "NursingRecipeExec",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalFee",
                table: "NursingRecipe",
                type: "decimal(18,4)",
                nullable: false,
                comment: "总费用",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "总费用");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeGroupNo",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "医嘱子号：药物为1.2.3...其它项目取默认值",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱子号");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeCategoryCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医嘱项目分类编码 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "医嘱项目分类编码");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,4)",
                nullable: false,
                comment: "单价",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "单价");

            migrationBuilder.AlterColumn<int>(
                name: "PayType",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                comment: "付费类型  0 自费项目 1 医保项目",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "付费类型: 0=自费,1=医保,2=其它");

            migrationBuilder.AlterColumn<Guid>(
                name: "PIID",
                table: "NursingRecipe",
                type: "uniqueidentifier",
                nullable: false,
                comment: "病人标识",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "患者唯一标识");

            migrationBuilder.AlterColumn<bool>(
                name: "IsChronicDisease",
                table: "NursingRecipe",
                type: "bit",
                nullable: true,
                comment: "慢性病标识",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "是否慢性病");

            migrationBuilder.AlterColumn<string>(
                name: "Diagnosis",
                table: "NursingRecipe",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "临床诊断",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "临床诊断");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "开嘱医生编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "申请医生编码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "开嘱科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "申请科室编码");

            migrationBuilder.AddColumn<string>(
                name: "ApplyDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "开嘱科室");

            migrationBuilder.AddColumn<string>(
                name: "ApplyDoctor",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "开嘱医生");

            migrationBuilder.AddColumn<int>(
                name: "BussinessCatalog",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "业务类型");

            migrationBuilder.AddColumn<string>(
                name: "ExecDept",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室");

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "NursingRecipe",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Insurance",
                table: "NursingRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医保目录：0-自费项目1-医保项目(甲、乙)");

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "NursingRecipe",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "编码");

            migrationBuilder.AddColumn<string>(
                name: "RecipeFeeStatus",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "收费状态");

            migrationBuilder.AddColumn<string>(
                name: "RecipeFeeStatusCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "收费状态码");

            migrationBuilder.AddColumn<string>(
                name: "RecipeName",
                table: "NursingRecipe",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "名称");

            migrationBuilder.AddColumn<string>(
                name: "Trainee",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "管培生");

            migrationBuilder.AlterColumn<string>(
                name: "UsageCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "用法编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "用法编码");

            migrationBuilder.AlterColumn<int>(
                name: "ToxicProperty",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                comment: "药理等级：毒、麻、精一、精二",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "药理等级");

            migrationBuilder.AlterColumn<bool>(
                name: "SkinTestResult",
                table: "NursingPrescribes",
                type: "bit",
                maxLength: 20,
                nullable: true,
                comment: "皮试结果 0-阴性 1-阳性",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "皮试结果:false=阴性 ture=阳性");

            migrationBuilder.AlterColumn<string>(
                name: "PrescribeTypeCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医嘱类型编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "医嘱类型编码");

            migrationBuilder.AlterColumn<string>(
                name: "MedicineProperty",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "药物属性：西药、中药、西药制剂、中药制剂",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "药物属性：西药、中药、西药制剂、中药制剂");

            migrationBuilder.AlterColumn<int>(
                name: "LongDays",
                table: "NursingPrescribes",
                type: "int",
                nullable: true,
                comment: "开药天数",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "开药天数");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSkinTest",
                table: "NursingPrescribes",
                type: "bit",
                maxLength: 20,
                nullable: true,
                comment: "是否皮试 0 不需要皮试 1 需要皮试",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "是否皮试 false=不需要皮试 true=需要皮试");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOutDrug",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "是否自备药：0-非自备药1-自备药",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否自备药：false=非自备药,true=自备药");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAmendedMark",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "是否抢救后补：0-非抢救后补1-抢救后补",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "是否抢救后补：false=非抢救后补，true=抢救后补");

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
                oldNullable: true,
                oldComment: "周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyName",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "频次",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "频次");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "频次码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "频次码");

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "剂量单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "剂量单位");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingPrescribes",
                type: "decimal(18,4)",
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "每次剂量");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "NursingPrescribes",
                type: "int",
                nullable: true,
                comment: "领量");

            migrationBuilder.AddColumn<string>(
                name: "Factory",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "厂家");

            migrationBuilder.AddColumn<string>(
                name: "HospitalCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProHospitalUse",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pharmacy",
                table: "NursingPrescribes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "药房");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "NursingPrescribes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "位置");

            migrationBuilder.AddColumn<string>(
                name: "PositionCode",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "位置编码");

            migrationBuilder.AddColumn<decimal>(
                name: "QtyPerTime",
                table: "NursingPrescribes",
                type: "decimal(18,4)",
                maxLength: 20,
                nullable: true,
                comment: "每次用量");

            migrationBuilder.AddColumn<int>(
                name: "RecipeGroupNo",
                table: "NursingPrescribes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医嘱分组号：分组顺序1,2,3");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "NursingPrescribes",
                type: "nvarchar(max)",
                nullable: true,
                comment: "领量单位");

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "NursingPrescribes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "用法");

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipe_RecipeCode",
                table: "NursingRecipe",
                column: "RecipeCode");

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipe_RecipeName",
                table: "NursingRecipe",
                column: "RecipeName");
        }
    }
}
