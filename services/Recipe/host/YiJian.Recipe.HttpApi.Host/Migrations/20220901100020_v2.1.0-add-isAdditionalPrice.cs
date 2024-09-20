using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addisAdditionalPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChildren",
                table: "RC_Treat");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdditionalPrice",
                table: "RC_DoctorsAdvice",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否是加收价格 1=是加收价格， 0=不是加收价格");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdditionalPrice",
                table: "RC_DoctorsAdvice");

            migrationBuilder.AddColumn<bool>(
                name: "IsChildren",
                table: "RC_Treat",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "当前是否是儿童");
        }
    }
}
