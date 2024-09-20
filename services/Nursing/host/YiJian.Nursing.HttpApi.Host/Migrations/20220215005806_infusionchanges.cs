using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class infusionchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastStatus",
                table: "NursingRecipeExecHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "操作前状态");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecipeId",
                table: "NursingRecipeExec",
                type: "uniqueidentifier",
                nullable: false,
                comment: "关联医嘱表编号",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PIID",
                table: "NursingRecipeExec",
                type: "uniqueidentifier",
                nullable: false,
                comment: "病人标识",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "IsInfusion",
                table: "NursingRecipeExec",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否输液");

            migrationBuilder.AddColumn<int>(
                name: "PlatformType",
                table: "NursingRecipeExec",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "系统标识: 0=急诊，1=院前");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastStatus",
                table: "NursingRecipeExecHistory");

            migrationBuilder.DropColumn(
                name: "IsInfusion",
                table: "NursingRecipeExec");

            migrationBuilder.DropColumn(
                name: "PlatformType",
                table: "NursingRecipeExec");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecipeId",
                table: "NursingRecipeExec",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "关联医嘱表编号");

            migrationBuilder.AlterColumn<Guid>(
                name: "PIID",
                table: "NursingRecipeExec",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "病人标识");
        }
    }
}
