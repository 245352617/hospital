using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class updatetablecomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "NursingPacs",
                comment: "检查",
                oldComment: "检验");

            migrationBuilder.AlterTable(
                name: "NursingLis",
                comment: "检验",
                oldComment: "检查");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "NursingPacs",
                comment: "检验",
                oldComment: "检查");

            migrationBuilder.AlterTable(
                name: "NursingLis",
                comment: "检查",
                oldComment: "检验");
        }
    }
}
