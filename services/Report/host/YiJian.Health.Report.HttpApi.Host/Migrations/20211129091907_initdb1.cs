using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class initdb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RpNursingRecord_RecordTime",
                table: "RpNursingRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RpNursingRecord_RecordTime",
                table: "RpNursingRecord",
                column: "RecordTime",
                unique: true);
        }
    }
}
