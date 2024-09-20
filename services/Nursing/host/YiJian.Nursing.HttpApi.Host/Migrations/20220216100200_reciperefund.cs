using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class reciperefund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "NursingRecipeExecRefund");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "NursingRecipeExecRefund");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "NursingRecipeExecRefund");

            migrationBuilder.AlterColumn<bool>(
                name: "IsWithDrawn",
                table: "NursingRecipeExecRefund",
                type: "bit",
                nullable: false,
                comment: "是否退药退费",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是退药");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmmerName",
                table: "NursingRecipeExecRefund",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "审批人名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "确认人名称");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmmerCode",
                table: "NursingRecipeExecRefund",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "审批人编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "确认人编码");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConfirmedTime",
                table: "NursingRecipeExecRefund",
                type: "datetime2",
                nullable: true,
                comment: "审批时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "确认时间");

            migrationBuilder.AddColumn<int>(
                name: "RefundType",
                table: "NursingRecipeExecRefund",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "退药退费类型");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundType",
                table: "NursingRecipeExecRefund");

            migrationBuilder.AlterColumn<bool>(
                name: "IsWithDrawn",
                table: "NursingRecipeExecRefund",
                type: "bit",
                nullable: false,
                comment: "是退药",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否退药退费");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmmerName",
                table: "NursingRecipeExecRefund",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "确认人名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "审批人名称");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmmerCode",
                table: "NursingRecipeExecRefund",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "确认人编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "审批人编码");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConfirmedTime",
                table: "NursingRecipeExecRefund",
                type: "datetime2",
                nullable: true,
                comment: "确认时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "审批时间");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "NursingRecipeExecRefund",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医嘱编码");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "NursingRecipeExecRefund",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "医嘱名称");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "NursingRecipeExecRefund",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                comment: "单价");
        }
    }
}
