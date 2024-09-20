using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class pku_v2270_add_IntakeStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "RpWardRound",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "级别",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "护理单Id(外键)");

            migrationBuilder.CreateTable(
                name: "RpIntakeStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Begintime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "统计开始时间"),
                    Endtime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "统计结束时间"),
                    InIntakesTotal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "入量总量"),
                    OutIntakesTotal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "出量总量"),
                    NursingDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理记录Id"),
                    SheetIndex = table.Column<int>(type: "int", nullable: true, comment: "护理记录单SheetIndex")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpIntakeStatistics", x => x.Id);
                },
                comment: "入量出量统计");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpIntakeStatistics");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "RpWardRound",
                type: "nvarchar(max)",
                nullable: true,
                comment: "护理单Id(外键)",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "级别");
        }
    }
}
