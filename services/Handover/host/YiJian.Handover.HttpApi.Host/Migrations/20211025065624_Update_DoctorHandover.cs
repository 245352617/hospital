using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_DoctorHandover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Handover_DoctorPatients",
                type: "nvarchar(max)",
                nullable: true,
                comment: "交班内容",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "交班内容");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Handover_DoctorPatients",
                type: "text",
                nullable: true,
                comment: "交班内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "交班内容");
        }
    }
}
