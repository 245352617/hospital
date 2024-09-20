using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_NursePatients_1111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Handover_NursePatients",
                comment: "交班患者",
                oldComment: "护士交班患者");

            migrationBuilder.AddColumn<string>(
                name: "AreaCode",
                table: "Handover_NursePatients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "区域编码");

            migrationBuilder.AddColumn<string>(
                name: "AreaName",
                table: "Handover_NursePatients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "区域名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaCode",
                table: "Handover_NursePatients");

            migrationBuilder.DropColumn(
                name: "AreaName",
                table: "Handover_NursePatients");

            migrationBuilder.AlterTable(
                name: "Handover_NursePatients",
                comment: "护士交班患者",
                oldComment: "交班患者");
        }
    }
}
