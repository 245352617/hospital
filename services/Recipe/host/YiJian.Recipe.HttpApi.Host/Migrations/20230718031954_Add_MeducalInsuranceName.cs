using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class Add_MeducalInsuranceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MeducalInsuranceName",
                table: "RC_DoctorsAdvice",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保统一名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeducalInsuranceName",
                table: "RC_DoctorsAdvice");
        }
    }
}
