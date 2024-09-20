using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class add_ReservationTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Dict_ExamProject",
                type: "nvarchar(max)",
                nullable: true,
                comment: "注意事项");

            migrationBuilder.AddColumn<string>(
                name: "ReservationTime",
                table: "Dict_ExamProject",
                type: "nvarchar(max)",
                nullable: true,
                comment: "预约时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "ReservationTime",
                table: "Dict_ExamProject");
        }
    }
}
