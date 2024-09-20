using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Add_NurseHandover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Handover_NurseHandovers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HandoverDate = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "交班日期"),
                    HandoverTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "交班时间"),
                    HandoverDoctorCode = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "交班医生编码"),
                    HandoverDoctorName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "交班医生名称"),
                    SuccessionDoctorCode = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "接班医生编码"),
                    SuccessionDoctorName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "接班医生名称"),
                    ShiftSettingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "班次id"),
                    ShiftSettingName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false, comment: "班次名称"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "交班状态，0：未提交，1：提交交班"),
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
                    table.PrimaryKey("PK_Handover_NurseHandovers", x => x.Id);
                },
                comment: "护士交班");

            migrationBuilder.CreateTable(
                name: "Handover_NursePatients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NurseHandoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护士交班id"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "triage分诊患者id"),
                    PatientId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "患者id"),
                    VisitNo = table.Column<int>(type: "int", nullable: true, comment: "就诊号"),
                    PatientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "患者姓名"),
                    Sex = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "性别"),
                    Age = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "年龄"),
                    TriageLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "分诊级别"),
                    InDeptTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "入科时间"),
                    DiagnoseName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "诊断"),
                    Bed = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "床号"),
                    IsNoThree = table.Column<bool>(type: "bit", nullable: false, comment: "是否三无"),
                    CriticallyIll = table.Column<bool>(type: "bit", nullable: false, comment: "是否病危"),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "交班内容"),
                    Test = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "检验"),
                    Inspect = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "检查"),
                    Emr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "电子病历"),
                    InOutVolume = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "出入量"),
                    VitalSigns = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "生命体征"),
                    Medicine = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "药物"),
                    LatestStatus = table.Column<string>(type: "ntext", nullable: true, comment: "最新现状"),
                    Background = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "背景"),
                    Assessment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "评估"),
                    Proposal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "建议"),
                    Devices = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "设备"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handover_NursePatients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Handover_NursePatients_Handover_NurseHandovers_NurseHandoverId",
                        column: x => x.NurseHandoverId,
                        principalTable: "Handover_NurseHandovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "护士交班id");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_NurseHandovers_HandoverDate",
                table: "Handover_NurseHandovers",
                column: "HandoverDate");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_NursePatients_NurseHandoverId",
                table: "Handover_NursePatients",
                column: "NurseHandoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Handover_NursePatients");

            migrationBuilder.DropTable(
                name: "Handover_NurseHandovers");
        }
    }
}
