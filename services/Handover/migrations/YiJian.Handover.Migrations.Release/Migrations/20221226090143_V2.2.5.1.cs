using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations.Migrations
{
    public partial class V2251 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Handover_DoctorHandovers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HandoverDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "交班日期"),
                    HandoverTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "交班时间"),
                    HandoverDoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "交班医生编码"),
                    HandoverDoctorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "交班医生名称"),
                    ShiftSettingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "班次id"),
                    ShiftSettingName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "班次名称"),
                    OtherMatters = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "其他事项"),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                name: "Handover_NurseHandovers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "triage分诊患者id"),
                    PatientId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "患者id"),
                    VisitNo = table.Column<int>(type: "int", nullable: true, comment: "就诊号"),
                    PatientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "患者姓名"),
                    Sex = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "性别编码"),
                    SexName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "性别名称"),
                    Age = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "年龄"),
                    TriageLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "分诊级别"),
                    TriageLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "分诊级别名称"),
                    AreaCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "区域编码"),
                    AreaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "区域名称"),
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
                    LatestStatus = table.Column<string>(type: "ntext", maxLength: 4000, nullable: true, comment: "最新现状"),
                    Background = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "背景"),
                    Assessment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "评估"),
                    Proposal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "建议"),
                    Devices = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "设备"),
                    HandoverTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "交班时间"),
                    HandoverNurseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "交班护士编码"),
                    HandoverNurseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "交班护士名称"),
                    SuccessionNurseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "接班护士编码"),
                    SuccessionNurseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "接班护士名称"),
                    HandoverDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "交班日期"),
                    ShiftSettingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "班次id"),
                    ShiftSettingName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "班次名称"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "交班状态，0：未提交，1：提交交班"),
                    CreationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "创建人编码"),
                    CreationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "创建人名称"),
                    TotalPatient = table.Column<int>(type: "int", nullable: false, comment: "查询的全部患者"),
                    DutyNurseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                name: "Handover_ShiftHandoverSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "类别编码"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "类别名称"),
                    ShiftName = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "班次名称"),
                    StartTime = table.Column<string>(type: "nvarchar(20)", nullable: false, comment: "开始时间"),
                    EndTime = table.Column<string>(type: "nvarchar(20)", nullable: false, comment: "结束时间"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    MatchingColor = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "匹配颜色"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "类型，医生1，护士0"),
                    CreationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Handover_ShiftHandoverSetting", x => x.Id);
                },
                comment: "交接班配置表");

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
                    Sex = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "性别"),
                    SexName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "性别名称"),
                    Age = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "年龄"),
                    TriageLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "分诊级别"),
                    DiagnoseName = table.Column<string>(type: "nvarchar(max)", maxLength: 1000, nullable: true, comment: "诊断"),
                    Bed = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "床号"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "交班内容"),
                    Test = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "检验"),
                    Inspect = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "检查"),
                    Emr = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "电子病历"),
                    InOutVolume = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "出入量"),
                    VitalSigns = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "生命体征"),
                    Medicine = table.Column<string>(type: "nvarchar(max)", maxLength: 4000, nullable: true, comment: "药物"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "IX_Handover_DoctorHandovers_HandoverDate",
                table: "Handover_DoctorHandovers",
                column: "HandoverDate");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_DoctorHandovers_HandoverDoctorCode",
                table: "Handover_DoctorHandovers",
                column: "HandoverDoctorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_DoctorHandovers_HandoverTime",
                table: "Handover_DoctorHandovers",
                column: "HandoverTime");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_DoctorPatients_DoctorHandoverId",
                table: "Handover_DoctorPatients",
                column: "DoctorHandoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_DoctorPatientStatistics_DoctorHandoverId",
                table: "Handover_DoctorPatientStatistics",
                column: "DoctorHandoverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Handover_NurseHandovers_PI_ID",
                table: "Handover_NurseHandovers",
                column: "PI_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Handover_DoctorPatients");

            migrationBuilder.DropTable(
                name: "Handover_DoctorPatientStatistics");

            migrationBuilder.DropTable(
                name: "Handover_NurseHandovers");

            migrationBuilder.DropTable(
                name: "Handover_ShiftHandoverSetting");

            migrationBuilder.DropTable(
                name: "Handover_DoctorHandovers");
        }
    }
}
