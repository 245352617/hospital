using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_MedicineFrequency_20220414 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExecDayTimes",
                table: "Dict_MedicineFrequency",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExecDayTimes",
                table: "Dict_MedicineFrequency",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开");
        }
    }
}
