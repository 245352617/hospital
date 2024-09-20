using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class Update_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalDosage",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "总剂量",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SortNum",
                table: "NursingRecipeExec",
                type: "int",
                nullable: false,
                comment: "排序编号",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PlanExcuteTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: false,
                comment: "计划执行时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "PlanExcuteHours",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "预计执行时长",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipeExec",
                type: "int",
                nullable: false,
                comment: "医嘱执行状态",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NurseTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                comment: "护理时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IfCalcIn",
                table: "NursingRecipeExec",
                type: "int",
                nullable: true,
                comment: "是否计算进入量",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                comment: "执行完成时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FinishNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "完成护士",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FinishNurse",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "完成护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExecuteType",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "执行方式PC/PDA",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExcuteNurseTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                comment: "护士执行时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExcuteNurseName",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExcuteNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "执行护士",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "每次剂量单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dosage",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "每次剂量",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConversionTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: false,
                comment: "拆分时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                comment: "核对时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckStatus",
                table: "NursingRecipeExec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "核对结果",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckNurseName",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "核对护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CheckNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "核对护士",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "用法");

            migrationBuilder.AddColumn<string>(
                name: "UsageCode",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "用法编码");

            migrationBuilder.AlterColumn<string>(
                name: "DeptCode",
                table: "Duct_CanulaPart",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "Duct_Canula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "导管主表主键"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理时间"),
                    CanulaWay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "置入方式"),
                    CanulaLength = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "置管长度"),
                    NurseId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "护士名称"),
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
                    table.PrimaryKey("PK_Duct_Canula", x => x.Id);
                },
                comment: "表:导管护理记录信息");

            migrationBuilder.CreateTable(
                name: "Duct_CanulaDynamic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "导管主表主键"),
                    GroupName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "管道分类"),
                    ParaCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "参数代码"),
                    ParaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "参数名称"),
                    ParaValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "参数值"),
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
                    table.PrimaryKey("PK_Duct_CanulaDynamic", x => x.Id);
                },
                comment: "表:导管参数动态");

            migrationBuilder.CreateTable(
                name: "Duct_NursingEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NurseDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理日期"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理时间"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者id"),
                    Context = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "内容"),
                    NurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护士工号"),
                    NurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护士名称"),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "记录时间"),
                    AuditNurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "审核人"),
                    AuditNurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "审核人名称"),
                    AuditTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "审核时间"),
                    AuditState = table.Column<int>(type: "int", nullable: true, comment: "审核状态（0-未审核，1，已审核，2-取消审核）"),
                    SignatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "签名记录编号对应icu_signature的id"),
                    Sort = table.Column<int>(type: "int", nullable: true, comment: "排序"),
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
                    table.PrimaryKey("PK_Duct_NursingEvent", x => x.Id);
                },
                comment: "表:护理记录");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Duct_Canula");

            migrationBuilder.DropTable(
                name: "Duct_CanulaDynamic");

            migrationBuilder.DropTable(
                name: "Duct_NursingEvent");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "UsageCode",
                table: "NursingRecipeExec");

            migrationBuilder.AlterColumn<string>(
                name: "TotalDosage",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "总剂量");

            migrationBuilder.AlterColumn<int>(
                name: "SortNum",
                table: "NursingRecipeExec",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序编号");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PlanExcuteTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "计划执行时间");

            migrationBuilder.AlterColumn<string>(
                name: "PlanExcuteHours",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "预计执行时长");

            migrationBuilder.AlterColumn<int>(
                name: "NursingStatus",
                table: "NursingRecipeExec",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医嘱执行状态");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NurseTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "护理时间");

            migrationBuilder.AlterColumn<int>(
                name: "IfCalcIn",
                table: "NursingRecipeExec",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "是否计算进入量");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "执行完成时间");

            migrationBuilder.AlterColumn<string>(
                name: "FinishNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "完成护士");

            migrationBuilder.AlterColumn<string>(
                name: "FinishNurse",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "完成护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "ExecuteType",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "执行方式PC/PDA");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExcuteNurseTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "护士执行时间");

            migrationBuilder.AlterColumn<string>(
                name: "ExcuteNurseName",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "ExcuteNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "执行护士");

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "每次剂量单位");

            migrationBuilder.AlterColumn<string>(
                name: "Dosage",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "每次剂量");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConversionTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "拆分时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "核对时间");

            migrationBuilder.AlterColumn<string>(
                name: "CheckStatus",
                table: "NursingRecipeExec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "核对结果");

            migrationBuilder.AlterColumn<string>(
                name: "CheckNurseName",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "核对护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "CheckNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "核对护士");

            migrationBuilder.AlterColumn<string>(
                name: "DeptCode",
                table: "Duct_CanulaPart",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
