using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class v2286dosageinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "RC_Prescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "剂量单位（急诊的）",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "剂量单位");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "RC_Prescribe",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "每次剂量（急诊的）",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "每次剂量");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultDosageUnit",
                table: "RC_Prescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "默认规格剂量单位（急诊的）",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CommitHisDosageQty",
                table: "RC_Prescribe",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "急诊换算之后-每次剂量-标准单位的数量");

            migrationBuilder.AddColumn<decimal>(
                name: "HisDosageQty",
                table: "RC_Prescribe",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "每次剂量-标准单位的数量");

            migrationBuilder.AddColumn<string>(
                name: "HisDosageUnit",
                table: "RC_Prescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "每次剂量-标准单位");

            migrationBuilder.AddColumn<string>(
                name: "HisUnit",
                table: "RC_Prescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "剂量单位-视图里面的那个Unit单位");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommitHisDosageQty",
                table: "RC_Prescribe");

            migrationBuilder.DropColumn(
                name: "HisDosageQty",
                table: "RC_Prescribe");

            migrationBuilder.DropColumn(
                name: "HisDosageUnit",
                table: "RC_Prescribe");

            migrationBuilder.DropColumn(
                name: "HisUnit",
                table: "RC_Prescribe");

            migrationBuilder.AlterColumn<string>(
                name: "DosageUnit",
                table: "RC_Prescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "剂量单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "剂量单位（急诊的）");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "RC_Prescribe",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                comment: "每次剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldComment: "每次剂量（急诊的）");

            migrationBuilder.AlterColumn<string>(
                name: "DefaultDosageUnit",
                table: "RC_Prescribe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "默认规格剂量单位（急诊的）");
        }
    }
}
