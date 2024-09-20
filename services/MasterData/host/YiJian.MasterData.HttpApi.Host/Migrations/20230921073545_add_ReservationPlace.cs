using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class add_ReservationPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationPlace",
                table: "Dict_ExamProject",
                type: "nvarchar(max)",
                nullable: true,
                comment: "预约地点");

            migrationBuilder.AddColumn<string>(
                name: "TemplateId",
                table: "Dict_ExamProject",
                type: "nvarchar(max)",
                nullable: true,
                comment: "打印模板Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationPlace",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Dict_ExamProject");
        }
    }
}
