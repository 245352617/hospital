using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addspecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LongDays",
                table: "RC_OwnMedicine",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "天数");

            migrationBuilder.AddColumn<string>(
                name: "Specification",
                table: "RC_OwnMedicine",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "规格型号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDays",
                table: "RC_OwnMedicine");

            migrationBuilder.DropColumn(
                name: "Specification",
                table: "RC_OwnMedicine");
        }
    }
}
