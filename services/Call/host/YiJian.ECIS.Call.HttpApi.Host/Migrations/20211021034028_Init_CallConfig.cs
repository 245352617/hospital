using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Init_CallConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallCallInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "登记时间"),
                    LogDate = table.Column<DateTime>(type: "date", nullable: true, comment: "排队日期（工作日计算）"),
                    CallingSn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "排队号"),
                    InCallQueueTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "开始排队时间"),
                    IsTop = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否置顶"),
                    TopTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "置顶时间"),
                    CallStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "叫号状态"),
                    DoctorId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "接诊医生id"),
                    DoctorName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "接诊医生姓名"),
                    ConsultingRoomCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "就诊诊室代码"),
                    ConsultingRoomName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "就诊诊室名称"),
                    VisitStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "就诊状态"),
                    VisitStartTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "开始就诊时间"),
                    VisitFinishTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "结束就诊时间"),
                    PatientID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "患者唯一标识(HIS)"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "患者姓名"),
                    Sex = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者性别"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "患者出生日期"),
                    IdentityType = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者身份"),
                    IdentityNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "身份证号"),
                    Nation = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "民族"),
                    Py = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "患者姓名拼音首字母"),
                    HomeAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "患者住址"),
                    ContactsPerson = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "紧急联系人"),
                    ContactsPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "联系电话"),
                    TriageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "患者分诊 ID"),
                    TriageDept = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "患者分诊科室编码"),
                    TriageDeptName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者分诊科室名称"),
                    ActTriageLevel = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者实际分诊级别"),
                    ActTriageLevelName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者实际分诊级别名称"),
                    RegisterNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "就诊号"),
                    ChargeType = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "费别"),
                    IsGreenChannel = table.Column<bool>(type: "bit", nullable: false, comment: "是否绿色通道"),
                    GreenChannelCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "绿色通道代码"),
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
                    table.PrimaryKey("PK_CallCallInfo", x => x.Id);
                },
                comment: "叫号信息");

            migrationBuilder.CreateTable(
                name: "CallCallingRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CallInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "叫号信息id"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间（叫号时间）"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "创建人id（叫号医生/护士）"),
                    DoctorId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "接诊医生id"),
                    DoctorName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "接诊医生姓名"),
                    ConsultingRoomCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "就诊诊室编码"),
                    ConsultingRoomName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "就诊诊室名称"),
                    TreatStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "就诊状态")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallCallingRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CallInfo_CallingRecord",
                        column: x => x.CallInfoId,
                        principalTable: "CallCallInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "叫号记录");

            migrationBuilder.CreateIndex(
                name: "IX_CallCallingRecord_CallInfoId",
                table: "CallCallingRecord",
                column: "CallInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallCallingRecord");

            migrationBuilder.DropTable(
                name: "CallCallInfo");
        }
    }
}
