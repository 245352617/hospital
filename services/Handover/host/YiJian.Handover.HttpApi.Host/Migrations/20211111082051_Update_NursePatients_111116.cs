using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_NursePatients_111116 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TriageLevelName",
                table: "Handover_NursePatients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "分诊级别名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "分诊级别");

            migrationBuilder.AddColumn<string>(
                name: "TriageLevel",
                table: "Handover_NursePatients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "分诊级别");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriageLevel",
                table: "Handover_NursePatients");

            migrationBuilder.AlterColumn<string>(
                name: "TriageLevelName",
                table: "Handover_NursePatients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "分诊级别",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "分诊级别名称");
        }
    }
}
