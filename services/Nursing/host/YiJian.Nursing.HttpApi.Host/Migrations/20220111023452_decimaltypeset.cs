using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class decimaltypeset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OtherPrice",
                table: "NursingTreat",
                type: "decimal(18,4)",
                nullable: true,
                comment: "其它价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "其它价格");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,4)",
                nullable: false,
                comment: "收费单价",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "收费单价");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "NursingRecipe",
                type: "decimal(18,4)",
                nullable: false,
                comment: "总费用",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "总费用");

            migrationBuilder.AlterColumn<decimal>(
                name: "QtyPerTimes",
                table: "NursingPrescribe",
                type: "decimal(18,4)",
                nullable: true,
                comment: "每次用量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "每次用量");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaterialPrice",
                table: "NursingPrescribe",
                type: "decimal(18,4)",
                nullable: true,
                comment: "耗材金额",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "耗材金额");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingPrescribe",
                type: "decimal(18,4)",
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "每次剂量");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinValue",
                table: "NursingIllnessObserveHeader",
                type: "decimal(18,4)",
                nullable: false,
                comment: "最小值",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "最小值");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinEarlyWarning",
                table: "NursingIllnessObserveHeader",
                type: "decimal(18,4)",
                nullable: false,
                comment: "最小值预警",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "最小值预警");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxValue",
                table: "NursingIllnessObserveHeader",
                type: "decimal(18,4)",
                nullable: false,
                comment: "最大值",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "最大值");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxEarlyWarning",
                table: "NursingIllnessObserveHeader",
                type: "decimal(18,4)",
                nullable: false,
                comment: "最大值预警",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "最大值预警");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OtherPrice",
                table: "NursingTreat",
                type: "decimal(18,2)",
                nullable: true,
                comment: "其它价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldComment: "其它价格");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "收费单价",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "收费单价");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "总费用",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "总费用");

            migrationBuilder.AlterColumn<decimal>(
                name: "QtyPerTimes",
                table: "NursingPrescribe",
                type: "decimal(18,2)",
                nullable: true,
                comment: "每次用量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldComment: "每次用量");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaterialPrice",
                table: "NursingPrescribe",
                type: "decimal(18,2)",
                nullable: true,
                comment: "耗材金额",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true,
                oldComment: "耗材金额");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingPrescribe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "每次剂量");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinValue",
                table: "NursingIllnessObserveHeader",
                type: "decimal(18,2)",
                nullable: false,
                comment: "最小值",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "最小值");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinEarlyWarning",
                table: "NursingIllnessObserveHeader",
                type: "decimal(18,2)",
                nullable: false,
                comment: "最小值预警",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "最小值预警");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxValue",
                table: "NursingIllnessObserveHeader",
                type: "decimal(18,2)",
                nullable: false,
                comment: "最大值",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "最大值");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxEarlyWarning",
                table: "NursingIllnessObserveHeader",
                type: "decimal(18,2)",
                nullable: false,
                comment: "最大值预警",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "最大值预警");
        }
    }
}
