using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2290rp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Url",
            //    table: "RpReportData");

            //migrationBuilder.AddColumn<string>(
            //    name: "PrescriptionNo",
            //    table: "RpReportData",
            //    type: "nvarchar(100)",
            //    maxLength: 100,
            //    nullable: true,
            //    comment: "处方号");

            migrationBuilder.CreateTable(
                name: "RpStatisticsMonthDoctorAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearMonth = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "月份，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年"),
                    Month = table.Column<int>(type: "int", nullable: false, comment: "月"),
                    DoctorTotal = table.Column<int>(type: "int", nullable: false, comment: "在岗医师总数"),
                    ReceptionTotal = table.Column<int>(type: "int", nullable: false, comment: "接诊总数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsMonthDoctorAndPatient", x => x.Id);
                },
                comment: "急诊科医患月度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsMonthEmergencyroomAndDeathPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearMonth = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "月份，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    Month = table.Column<int>(type: "int", nullable: false, comment: "月份"),
                    RescueTotal = table.Column<int>(type: "int", nullable: false, comment: "抢救总数"),
                    DeathToll = table.Column<int>(type: "int", nullable: false, comment: "死亡总数"),
                    DeathRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "死亡率")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsMonthEmergencyroomAndDeathPatient", x => x.Id);
                },
                comment: "急诊抢救室患者死亡率月度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsMonthEmergencyroomAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearMonth = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "月份，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    Month = table.Column<int>(type: "int", nullable: false, comment: "月份"),
                    RescueTotal = table.Column<int>(type: "int", nullable: false, comment: "抢救总数"),
                    AvgDetentionTime = table.Column<int>(type: "int", nullable: false, comment: "平均滞留时间"),
                    MidDetentionTime = table.Column<int>(type: "int", nullable: false, comment: "滞留时间中位数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsMonthEmergencyroomAndPatient", x => x.Id);
                },
                comment: "抢救室滞留时间中位数月度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsMonthLevelAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearMonth = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "月份，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    Month = table.Column<int>(type: "int", nullable: false, comment: "月份"),
                    LI = table.Column<int>(type: "int", nullable: false, comment: "I级"),
                    LII = table.Column<int>(type: "int", nullable: false, comment: "II级"),
                    LIII = table.Column<int>(type: "int", nullable: false, comment: "III级"),
                    LIVa = table.Column<int>(type: "int", nullable: false, comment: "IVa级"),
                    LIVb = table.Column<int>(type: "int", nullable: false, comment: "IVb级")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsMonthLevelAndPatient", x => x.Id);
                },
                comment: "急诊科各级患者比例月度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsMonthNurseAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearMonth = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "月份，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    Month = table.Column<int>(type: "int", nullable: false, comment: "月份"),
                    NurseTotal = table.Column<int>(type: "int", nullable: false, comment: "在岗护士总数"),
                    ReceptionTotal = table.Column<int>(type: "int", nullable: false, comment: "接诊总数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsMonthNurseAndPatient", x => x.Id);
                },
                comment: "急诊科护患月度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsQuarterDoctorAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearQuarter = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "季度，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年"),
                    Quarter = table.Column<int>(type: "int", nullable: false, comment: "季度"),
                    DoctorTotal = table.Column<int>(type: "int", nullable: false, comment: "在岗医师总数"),
                    ReceptionTotal = table.Column<int>(type: "int", nullable: false, comment: "接诊总数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsQuarterDoctorAndPatient", x => x.Id);
                },
                comment: "急诊科医患季度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsQuarterEmergencyroomAndDeathPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearQuarter = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "季度，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    Quarter = table.Column<int>(type: "int", nullable: false, comment: "季度"),
                    RescueTotal = table.Column<int>(type: "int", nullable: false, comment: "抢救总数"),
                    DeathToll = table.Column<int>(type: "int", nullable: false, comment: "死亡总数"),
                    DeathRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "死亡率")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsQuarterEmergencyroomAndDeathPatient", x => x.Id);
                },
                comment: "急诊抢救室患者死亡率季度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsQuarterEmergencyroomAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearQuarter = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "季度，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    Quarter = table.Column<int>(type: "int", nullable: false, comment: "季度"),
                    RescueTotal = table.Column<int>(type: "int", nullable: false, comment: "抢救总数"),
                    AvgDetentionTime = table.Column<int>(type: "int", nullable: false, comment: "平均滞留时间"),
                    MidDetentionTime = table.Column<int>(type: "int", nullable: false, comment: "滞留时间中位数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsQuarterEmergencyroomAndPatient", x => x.Id);
                },
                comment: "抢救室滞留时间中位数季度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsQuarterLevelAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearQuarter = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "季度，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    Quarter = table.Column<int>(type: "int", nullable: false, comment: "季度"),
                    LI = table.Column<int>(type: "int", nullable: false, comment: "I级"),
                    LII = table.Column<int>(type: "int", nullable: false, comment: "II级"),
                    LIII = table.Column<int>(type: "int", nullable: false, comment: "III级"),
                    LIVa = table.Column<int>(type: "int", nullable: false, comment: "IVa级"),
                    LIVb = table.Column<int>(type: "int", nullable: false, comment: "IVb级")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsQuarterLevelAndPatient", x => x.Id);
                },
                comment: "急诊科各级患者比例季度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsQuarterNurseAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearQuarter = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "季度，方便查询用的字段"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    Quarter = table.Column<int>(type: "int", nullable: false, comment: "季度"),
                    NurseTotal = table.Column<int>(type: "int", nullable: false, comment: "在岗护士总数"),
                    ReceptionTotal = table.Column<int>(type: "int", nullable: false, comment: "接诊总数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsQuarterNurseAndPatient", x => x.Id);
                },
                comment: "急诊科护患季度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsYearDoctorAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    DoctorTotal = table.Column<int>(type: "int", nullable: false, comment: "在岗医师总数"),
                    ReceptionTotal = table.Column<int>(type: "int", nullable: false, comment: "接诊总数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsYearDoctorAndPatient", x => x.Id);
                },
                comment: "急诊科医患年度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsYearEmergencyroomAndDeathPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    RescueTotal = table.Column<int>(type: "int", nullable: false, comment: "抢救总数"),
                    DeathToll = table.Column<int>(type: "int", nullable: false, comment: "死亡总数"),
                    DeathRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "死亡率")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsYearEmergencyroomAndDeathPatient", x => x.Id);
                },
                comment: "急诊抢救室患者死亡率年度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsYearEmergencyroomAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    RescueTotal = table.Column<int>(type: "int", nullable: false, comment: "抢救总数"),
                    AvgDetentionTime = table.Column<int>(type: "int", nullable: false, comment: "平均滞留时间"),
                    MidDetentionTime = table.Column<int>(type: "int", nullable: false, comment: "滞留时间中位数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsYearEmergencyroomAndPatient", x => x.Id);
                },
                comment: "抢救室滞留时间中位数年度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsYearLevelAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    LI = table.Column<int>(type: "int", nullable: false, comment: "I级"),
                    LII = table.Column<int>(type: "int", nullable: false, comment: "II级"),
                    LIII = table.Column<int>(type: "int", nullable: false, comment: "III级"),
                    LIVa = table.Column<int>(type: "int", nullable: false, comment: "IVa级"),
                    LIVb = table.Column<int>(type: "int", nullable: false, comment: "IVb级")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsYearLevelAndPatient", x => x.Id);
                },
                comment: "急诊科各级患者比例年度视图");

            migrationBuilder.CreateTable(
                name: "RpStatisticsYearNurseAndPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false, comment: "年份"),
                    NurseTotal = table.Column<int>(type: "int", nullable: false, comment: "在岗护士总数"),
                    ReceptionTotal = table.Column<int>(type: "int", nullable: false, comment: "接诊总数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpStatisticsYearNurseAndPatient", x => x.Id);
                },
                comment: "急诊科护患年度视图");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpStatisticsMonthDoctorAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsMonthEmergencyroomAndDeathPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsMonthEmergencyroomAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsMonthLevelAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsMonthNurseAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsQuarterDoctorAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsQuarterEmergencyroomAndDeathPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsQuarterEmergencyroomAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsQuarterLevelAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsQuarterNurseAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsYearDoctorAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsYearEmergencyroomAndDeathPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsYearEmergencyroomAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsYearLevelAndPatient");

            migrationBuilder.DropTable(
                name: "RpStatisticsYearNurseAndPatient");

            //migrationBuilder.DropColumn(
            //    name: "PrescriptionNo",
            //    table: "RpReportData");

            //migrationBuilder.AddColumn<string>(
            //    name: "Url",
            //    table: "RpReportData",
            //    type: "nvarchar(100)",
            //    maxLength: 100,
            //    nullable: true,
            //    comment: "Url");
        }
    }
}
