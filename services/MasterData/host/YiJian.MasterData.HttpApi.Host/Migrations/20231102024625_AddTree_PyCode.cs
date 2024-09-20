using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class AddTree_PyCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PyCode",
                table: "Dict_LabTree",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "拼音编码");

            migrationBuilder.AddColumn<string>(
                name: "PyCode",
                table: "Dict_ExamTree",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "拼音编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PyCode",
                table: "Dict_LabTree");

            migrationBuilder.DropColumn(
                name: "PyCode",
                table: "Dict_ExamTree");
        }
    }
}
