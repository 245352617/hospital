using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class update_RecipeExecHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RemainDosage",
                table: "NursingRecipeExecRecord",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "余量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "余量");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingRecipeExecRecord",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "执行剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "执行剂量");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscardDosage",
                table: "NursingRecipeExecRecord",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "废弃量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "废弃量");

            migrationBuilder.AddColumn<string>(
                name: "OperationContent",
                table: "NursingRecipeExecHistory",
                type: "nvarchar(max)",
                nullable: true,
                comment: "操作内容");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalRemainDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "总余量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "总余量");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalExecDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "总执行量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "总执行量");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "总剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "总剂量");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDiscardDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "废弃量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "废弃量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationContent",
                table: "NursingRecipeExecHistory");

            migrationBuilder.AlterColumn<decimal>(
                name: "RemainDosage",
                table: "NursingRecipeExecRecord",
                type: "decimal(18,2)",
                nullable: false,
                comment: "余量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "余量");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingRecipeExecRecord",
                type: "decimal(18,2)",
                nullable: false,
                comment: "执行剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "执行剂量");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscardDosage",
                table: "NursingRecipeExecRecord",
                type: "decimal(18,2)",
                nullable: false,
                comment: "废弃量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "废弃量");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalRemainDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                comment: "总余量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "总余量");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalExecDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                comment: "总执行量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "总执行量");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                comment: "总剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "总剂量");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDiscardDosage",
                table: "NursingRecipeExec",
                type: "decimal(18,2)",
                nullable: false,
                comment: "废弃量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "废弃量");
        }
    }
}
