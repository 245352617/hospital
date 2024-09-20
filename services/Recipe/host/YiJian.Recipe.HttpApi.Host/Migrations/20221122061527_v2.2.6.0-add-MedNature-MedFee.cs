using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v2260addMedNatureMedFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedFee",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                comment: "费别 用于申请单费别显示");

            migrationBuilder.AddColumn<string>(
                name: "MedNature",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                comment: "性质 用于申请单性质显示");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedFee",
                table: "RC_MedDetailResult");

            migrationBuilder.DropColumn(
                name: "MedNature",
                table: "RC_MedDetailResult");
        }
    }
}
