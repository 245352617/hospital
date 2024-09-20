using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2290rpremovecolomun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeathRate",
                table: "RpStatisticsYearEmergencyroomAndDeathPatient");

            migrationBuilder.DropColumn(
                name: "DeathRate",
                table: "RpStatisticsQuarterEmergencyroomAndDeathPatient");

            migrationBuilder.DropColumn(
                name: "DeathRate",
                table: "RpStatisticsMonthEmergencyroomAndDeathPatient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DeathRate",
                table: "RpStatisticsYearEmergencyroomAndDeathPatient",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "死亡率");

            migrationBuilder.AddColumn<decimal>(
                name: "DeathRate",
                table: "RpStatisticsQuarterEmergencyroomAndDeathPatient",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "死亡率");

            migrationBuilder.AddColumn<decimal>(
                name: "DeathRate",
                table: "RpStatisticsMonthEmergencyroomAndDeathPatient",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "死亡率");
        }
    }
}
