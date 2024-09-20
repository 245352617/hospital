using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v2280adddoctorinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorCode",
                table: "EmrXmlHistory",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                comment: "医生编号");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "EmrXmlHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "医生名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorCode",
                table: "EmrXmlHistory");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "EmrXmlHistory");
        }
    }
}
