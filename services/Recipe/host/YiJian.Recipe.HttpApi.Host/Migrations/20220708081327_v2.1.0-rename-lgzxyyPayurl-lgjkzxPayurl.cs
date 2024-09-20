using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210renamelgzxyyPayurllgjkzxPayurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lgzxyy_payurl",
                table: "RC_MedDetailResult",
                newName: "LgzxyyPayurl");

            migrationBuilder.RenameColumn(
                name: "Lgjkzx_payurl",
                table: "RC_MedDetailResult",
                newName: "LgjkzxPayurl");

            migrationBuilder.AlterColumn<int>(
                name: "ToxicLevel",
                table: "RC_Toxic",
                type: "int",
                nullable: true,
                comment: "药理 0=普通药品,1=毒性药品,2=麻醉、精一药品，3=精二类，4=放射性，5=贵重药品，6=妊娠药品，7=狂犬疫苗",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "精神药  0非精神药,1一类精神药,2二类精神药");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LgzxyyPayurl",
                table: "RC_MedDetailResult",
                newName: "Lgzxyy_payurl");

            migrationBuilder.RenameColumn(
                name: "LgjkzxPayurl",
                table: "RC_MedDetailResult",
                newName: "Lgjkzx_payurl");

            migrationBuilder.AlterColumn<int>(
                name: "ToxicLevel",
                table: "RC_Toxic",
                type: "int",
                nullable: true,
                comment: "精神药  0非精神药,1一类精神药,2二类精神药",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "药理 0=普通药品,1=毒性药品,2=麻醉、精一药品，3=精二类，4=放射性，5=贵重药品，6=妊娠药品，7=狂犬疫苗");
        }
    }
}
