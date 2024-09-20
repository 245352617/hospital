using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class removerequiredfortoxicproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ToxicProperty",
                table: "NursingPrescribe",
                type: "nvarchar(max)",
                nullable: true,
                comment: "药理等级",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "药理等级");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ToxicProperty",
                table: "NursingPrescribe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "药理等级",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "药理等级");
        }
    }
}
