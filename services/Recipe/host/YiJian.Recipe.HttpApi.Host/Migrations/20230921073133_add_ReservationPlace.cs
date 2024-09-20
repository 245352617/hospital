using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class add_ReservationPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationPlace",
                table: "RC_ProjectExamProp",
                type: "nvarchar(max)",
                nullable: true,
                comment: "预约地点");

            migrationBuilder.AddColumn<string>(
                name: "TemplateId",
                table: "RC_ProjectExamProp",
                type: "nvarchar(max)",
                nullable: true,
                comment: "打印模板Id");

            migrationBuilder.AddColumn<string>(
                name: "ReservationPlace",
                table: "RC_Pacs",
                type: "nvarchar(max)",
                nullable: true,
                comment: "预约地点");

            migrationBuilder.AddColumn<string>(
                name: "TemplateId",
                table: "RC_Pacs",
                type: "nvarchar(max)",
                nullable: true,
                comment: "打印模板Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationPlace",
                table: "RC_ProjectExamProp");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "RC_ProjectExamProp");

            migrationBuilder.DropColumn(
                name: "ReservationPlace",
                table: "RC_Pacs");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "RC_Pacs");
        }
    }
}
