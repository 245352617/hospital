using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Add_DoctorHandover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Handover_DoctorHandovers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HandoverDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "交班日期"),
                    HandoverTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "交班时间"),
                    HandoverDoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "交班医生编码"),
                    HandoverDoctorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "交班医生名称"),
                    ShiftSettingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "班次id"),
                    ShiftSettingName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "班次名称"),
                    OtherMatters = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "其他事项"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handover_DoctorHandovers", x => x.Id);
                },
                comment: "医生交接班表");

            migrationBuilder.CreateTable(
                name: "Handover_DoctorPatients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorHandoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医生交班id"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "triage分诊患者id"),
                    PatientId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者id"),
                    VisitNo = table.Column<int>(type: "int", nullable: true, comment: "就诊号"),
                    PatientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "患者姓名"),
                    Sex = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "性别"),
                    Age = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "年龄"),
                    TriageLevel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "分诊级别"),
                    Diagnose = table.Column<string>(type: "nvarchar(max)", maxLength: 1000, nullable: true, comment: "诊断"),
                    Bed = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "床号"),
                    Content = table.Column<string>(type: "text", nullable: true, comment: "交班内容"),
                    Test = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "检验"),
                    Inspect = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "检查"),
                    Emr = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "电子病历"),
                    InOutVolume = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "出入量"),
                    VitalSigns = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "生命体征"),
                    Medicine = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "药物"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handover_DoctorPatients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Handover_DoctorPatients_Handover_DoctorHandovers_DoctorHandoverId",
                        column: x => x.DoctorHandoverId,
                        principalTable: "Handover_DoctorHandovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "医生交班患者");

            migrationBuilder.CreateTable(
                name: "Handover_DoctorPatientStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorHandoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false, comment: "接诊总人数"),
                    ClassI = table.Column<int>(type: "int", nullable: false, comment: "I级  (病危人数)"),
                    ClassII = table.Column<int>(type: "int", nullable: false, comment: "II级  (病重人数)"),
                    ClassIII = table.Column<int>(type: "int", nullable: false, comment: "III级"),
                    ClassIV = table.Column<int>(type: "int", nullable: false, comment: "IV级"),
                    PreOperation = table.Column<int>(type: "int", nullable: false, comment: "预术人数"),
                    ExistingDisease = table.Column<int>(type: "int", nullable: false, comment: "现有病人数"),
                    OutDept = table.Column<int>(type: "int", nullable: false, comment: "出科人数"),
                    Rescue = table.Column<int>(type: "int", nullable: false, comment: "抢救人数"),
                    Visit = table.Column<int>(type: "int", nullable: false, comment: "出诊人数"),
                    Death = table.Column<int>(type: "int", nullable: false, comment: "死亡人数"),
                    CPR = table.Column<int>(type: "int", nullable: false, comment: "心肺复苏人数"),
                    Admission = table.Column<int>(type: "int", nullable: false, comment: "收住院人数"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handover_DoctorPatientStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Handover_DoctorPatientStatistics_Handover_DoctorHandovers_DoctorHandoverId",
                        column: x => x.DoctorHandoverId,
                        principalTable: "Handover_DoctorHandovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "医生患者统计");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_DoctorPatients_DoctorHandoverId",
                table: "Handover_DoctorPatients",
                column: "DoctorHandoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_DoctorPatientStatistics_DoctorHandoverId",
                table: "Handover_DoctorPatientStatistics",
                column: "DoctorHandoverId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Handover_DoctorPatients");

            migrationBuilder.DropTable(
                name: "Handover_DoctorPatientStatistics");

            migrationBuilder.DropTable(
                name: "Handover_DoctorHandovers");
        }
    }
}
