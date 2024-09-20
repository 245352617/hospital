using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_VitalSignExpression : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Dict_VitalSignExpressions",
                comment: "生命体征表达式",
                oldComment: "评分项");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Dict_VitalSignExpressions",
                comment: "评分项",
                oldComment: "生命体征表达式");
        }
    }
}
