using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210updatetypeRecieveQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RecieveQty",
                table: "RC_QuickStartMedicine",
                type: "decimal(18,2)",
                nullable: false,
                comment: "领量(数量)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "领量(数量)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RecieveQty",
                table: "RC_PackageProject",
                type: "decimal(18,2)",
                nullable: false,
                comment: "领量(数量)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "领量(数量)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RecieveQty",
                table: "RC_QuickStartMedicine",
                type: "int",
                nullable: false,
                comment: "领量(数量)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "领量(数量)");

            migrationBuilder.AlterColumn<int>(
                name: "RecieveQty",
                table: "RC_PackageProject",
                type: "int",
                nullable: false,
                comment: "领量(数量)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "领量(数量)");
        }
    }
}
