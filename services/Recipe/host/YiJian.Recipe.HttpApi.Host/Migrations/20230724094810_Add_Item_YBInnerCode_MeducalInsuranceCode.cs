using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class Add_Item_YBInnerCode_MeducalInsuranceCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MeducalInsuranceCode",
                table: "RC_PacsItem",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保机构编码");

            migrationBuilder.AddColumn<string>(
                name: "YBInneCode",
                table: "RC_PacsItem",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保二级编码");

            migrationBuilder.AddColumn<string>(
                name: "MeducalInsuranceCode",
                table: "RC_LisItem",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保机构编码");

            migrationBuilder.AddColumn<string>(
                name: "YBInneCode",
                table: "RC_LisItem",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保二级编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeducalInsuranceCode",
                table: "RC_PacsItem");

            migrationBuilder.DropColumn(
                name: "YBInneCode",
                table: "RC_PacsItem");

            migrationBuilder.DropColumn(
                name: "MeducalInsuranceCode",
                table: "RC_LisItem");

            migrationBuilder.DropColumn(
                name: "YBInneCode",
                table: "RC_LisItem");
        }
    }
}
