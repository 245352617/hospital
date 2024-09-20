using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class initdb_recipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalFee",
                table: "NursingRecipe",
                type: "decimal(18,4)",
                nullable: false,
                comment: "总费用",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "总费用");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,4)",
                nullable: false,
                comment: "单价",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "单价");

            migrationBuilder.AlterColumn<decimal>(
                name: "QtyPerTime",
                table: "NursingPrescribes",
                type: "decimal(18,4)",
                maxLength: 20,
                nullable: true,
                comment: "每次用量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "每次用量");

            migrationBuilder.AlterColumn<int>(
                name: "LongDays",
                table: "NursingPrescribes",
                type: "int",
                nullable: true,
                comment: "开药天数",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldComment: "开药天数");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOutDrug",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "是否自备药：0-非自备药1-自备药",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否自备药：0-非自备药1-自备药");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAmendedMark",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "是否抢救后补：0-非抢救后补1-抢救后补",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否抢救后补：0-非抢救后补1-抢救后补");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingPrescribes",
                type: "decimal(18,4)",
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "每次剂量");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "NursingPrescribes",
                type: "int",
                nullable: true,
                comment: "领量",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldComment: "领量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalFee",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "总费用",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "总费用");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NursingRecipe",
                type: "decimal(18,2)",
                nullable: false,
                comment: "单价",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "单价");

            migrationBuilder.AlterColumn<decimal>(
                name: "QtyPerTime",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                maxLength: 20,
                nullable: true,
                comment: "每次用量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "每次用量");

            migrationBuilder.AlterColumn<int>(
                name: "LongDays",
                table: "NursingPrescribes",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "开药天数",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "开药天数");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOutDrug",
                table: "NursingPrescribes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否自备药：0-非自备药1-自备药",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "是否自备药：0-非自备药1-自备药");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAmendedMark",
                table: "NursingPrescribes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否抢救后补：0-非抢救后补1-抢救后补",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComment: "是否抢救后补：0-非抢救后补1-抢救后补");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "NursingPrescribes",
                type: "decimal(18,2)",
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "每次剂量");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "NursingPrescribes",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "领量",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "领量");
        }
    }
}
