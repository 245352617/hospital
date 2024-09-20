using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class Update_IllnessObserveMain_1015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VisitNo",
                table: "NursingIllnessObserveMain",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "就诊号",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "就诊号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VisitNo",
                table: "NursingIllnessObserveMain",
                type: "nvarchar(max)",
                nullable: true,
                comment: "就诊号",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "就诊号");
        }
    }
}
