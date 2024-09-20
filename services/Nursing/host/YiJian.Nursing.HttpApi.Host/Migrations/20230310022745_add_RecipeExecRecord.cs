using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class add_RecipeExecRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExcuteNurseTime",
                table: "NursingRecipeExec",
                newName: "ExecuteNurseTime");

            migrationBuilder.RenameColumn(
                name: "ExcuteNurseName",
                table: "NursingRecipeExec",
                newName: "ExecuteNurseName");

            migrationBuilder.RenameColumn(
                name: "ExcuteNurseCode",
                table: "NursingRecipeExec",
                newName: "ExecuteNurseCode");

            migrationBuilder.AlterTable(
                name: "NursingRecipeExec",
                comment: "拆分记录表(执行单)",
                oldComment: "医嘱执行");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "总剂量",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "总剂量");

            migrationBuilder.AddColumn<int>(
                name: "ExecuteStatus",
                table: "NursingRecipeExec",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "护士站执行单状态");

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscard",
                table: "NursingRecipeExec",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否废弃");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscardDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "废弃量");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExecDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "总执行量");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRemainDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "总余量");

            migrationBuilder.AddColumn<string>(
                name: "TwoCheckNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "二次核对护士");

            migrationBuilder.AddColumn<string>(
                name: "TwoCheckNurseName",
                table: "NursingRecipeExec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "二次核对护士名称");

            migrationBuilder.AddColumn<DateTime>(
                name: "TwoCheckTime",
                table: "NursingRecipeExec",
                type: "datetime2",
                nullable: true,
                comment: "二次核对时间");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "计量单位");

            migrationBuilder.CreateTable(
                name: "NursingRecipeExecRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeExecId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "执行单Id"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "执行剂量"),
                    RemainDosage = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "余量"),
                    DiscardDosage = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "废弃量"),
                    Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "计量单位"),
                    ExcuteNurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行护士"),
                    ExcuteNurseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行护士名称"),
                    ExcuteNurseTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护士执行时间"),
                    ExecRecordStatus = table.Column<int>(type: "int", nullable: false, comment: "执行记录状态")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingRecipeExecRecord", x => x.Id);
                },
                comment: "执行记录表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingRecipeExecRecord");

            migrationBuilder.DropColumn(
                name: "ExecuteStatus",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "IsDiscard",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "TotalDiscardDosage",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "TotalExecDosage",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "TotalRemainDosage",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "TwoCheckNurseCode",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "TwoCheckNurseName",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "TwoCheckTime",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "NursingRecipeExec");

            migrationBuilder.RenameColumn(
                name: "ExecuteNurseTime",
                table: "NursingRecipeExec",
                newName: "ExcuteNurseTime");

            migrationBuilder.RenameColumn(
                name: "ExecuteNurseName",
                table: "NursingRecipeExec",
                newName: "ExcuteNurseName");

            migrationBuilder.RenameColumn(
                name: "ExecuteNurseCode",
                table: "NursingRecipeExec",
                newName: "ExcuteNurseCode");

            migrationBuilder.AlterTable(
                name: "NursingRecipeExec",
                comment: "医嘱执行",
                oldComment: "拆分记录表(执行单)");

            migrationBuilder.AlterColumn<string>(
                name: "TotalDosage",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "总剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "总剂量");
        }
    }
}
