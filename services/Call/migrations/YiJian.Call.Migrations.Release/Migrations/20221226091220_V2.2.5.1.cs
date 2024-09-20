using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Call.Migrations.Migrations
{
    public partial class V2251 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallBaseConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TomorrowCallMode = table.Column<int>(type: "int", nullable: false, comment: "当前叫号模式"),
                    RegularEffectTime = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "模式生效时间"),
                    TomorrowUpdateNoHour = table.Column<int>(type: "int", nullable: false, comment: "每日更新号码时间（小时）"),
                    TomorrowUpdateNoMinute = table.Column<int>(type: "int", nullable: false, comment: "每日更新号码时间（分钟）"),
                    CurrentCallMode = table.Column<int>(type: "int", nullable: false, comment: "当前叫号模式"),
                    CurrentUpdateNoHour = table.Column<int>(type: "int", nullable: false, comment: "当前的 每日更新号码时间（小时）（0-23）"),
                    CurrentUpdateNoMinute = table.Column<int>(type: "int", nullable: false, comment: "每日更新号码时间（分钟）"),
                    FriendlyReminder = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "温馨提醒（大屏叫号端）"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallBaseConfig", x => x.Id);
                },
                comment: "叫号设置-基础设置");

            migrationBuilder.CreateTable(
                name: "CallCallInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "登记时间"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "排队日期（工作日计算）"),
                    CallingSn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "排队号"),
                    InCallQueueTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "开始排队时间"),
                    IsTop = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "是否置顶"),
                    TopTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "置顶时间"),
                    CallStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "叫号状态"),
                    LastCalledTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "叫号时间"),
                    DoctorId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "急诊医生id"),
                    DoctorName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "急诊医生姓名"),
                    ConsultingRoomCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "就诊诊室代码"),
                    ConsultingRoomName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "就诊诊室名称"),
                    VisitStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "就诊状态"),
                    VisitStartTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "开始就诊时间"),
                    VisitFinishTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "结束就诊时间"),
                    PatientID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "患者唯一标识(HIS)"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "患者姓名"),
                    Sex = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者性别"),
                    SexName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者性别"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "患者出生日期"),
                    IdentityType = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者身份"),
                    IdentityTypeName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者身份"),
                    IdentityNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "身份证号"),
                    Nation = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "民族"),
                    Py = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "患者姓名拼音首字母"),
                    HomeAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "患者住址"),
                    ContactsPerson = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "紧急联系人"),
                    ContactsPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "联系电话"),
                    TriageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "患者分诊 ID"),
                    TriageDept = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者分诊科室编码"),
                    TriageDeptName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者分诊科室名称"),
                    ActTriageLevel = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者实际分诊级别"),
                    ActTriageLevelName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "患者实际分诊级别名称"),
                    TriageDirection = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "分诊去向编码"),
                    RegisterNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "就诊号"),
                    ChargeType = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "费别"),
                    ChargeTypeName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "费别"),
                    ToHospitalWay = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "来院方式"),
                    IsShow = table.Column<bool>(type: "bit", nullable: false, comment: "是否显示"),
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
                name: "CallConsultingRoomRegular",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsultingRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "诊室id"),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "是否使用"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallConsultingRoomRegular", x => x.Id);
                },
                comment: "诊室固定表");

            migrationBuilder.CreateTable(
                name: "CallDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "编码"),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_CallDepartment", x => x.Id);
                },
                comment: "科室表");

            migrationBuilder.CreateTable(
                name: "CallDoctorRegular",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "医生id"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生名称"),
                    DoctorDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医生所属科室id"),
                    DoctorDepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生所属科室名称"),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "对应急诊科室id"),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "是否使用"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallDoctorRegular", x => x.Id);
                },
                comment: "医生变动表");

            migrationBuilder.CreateTable(
                name: "CallRowConfig",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Field = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Order = table.Column<short>(type: "smallint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Width = table.Column<short>(type: "smallint", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    Wrap = table.Column<bool>(type: "bit", nullable: false),
                    DefaultOrder = table.Column<short>(type: "smallint", nullable: false),
                    DefaultText = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DefaultWidth = table.Column<short>(type: "smallint", nullable: false),
                    DefaultVisible = table.Column<bool>(type: "bit", nullable: false),
                    DefaultWrap = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallRowConfig", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "CallSerialNoRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "科室id"),
                    Prefix = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, comment: "开头字母"),
                    SerialLength = table.Column<int>(type: "int", nullable: false, defaultValue: 3, comment: "流水号位数"),
                    CurrentNo = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "当前流水号"),
                    SerialDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallSerialNoRule", x => x.Id);
                },
                comment: "排队号规则");

            migrationBuilder.CreateTable(
                name: "CallCallingRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CallInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "叫号信息id"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间（叫号时间）"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "创建人id（叫号医生/护士）"),
                    DoctorId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "急诊医生id"),
                    DoctorName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "急诊医生姓名"),
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

            migrationBuilder.CreateTable(
                name: "CallConsultingRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "编码"),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "IP"),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallConsultingRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CallConsultingRoom_CallDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "CallDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "诊室表");

            migrationBuilder.CreateIndex(
                name: "IX_CallCallingRecord_CallInfoId",
                table: "CallCallingRecord",
                column: "CallInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CallConsultingRoom_DepartmentId",
                table: "CallConsultingRoom",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallBaseConfig");

            migrationBuilder.DropTable(
                name: "CallCallingRecord");

            migrationBuilder.DropTable(
                name: "CallConsultingRoom");

            migrationBuilder.DropTable(
                name: "CallConsultingRoomRegular");

            migrationBuilder.DropTable(
                name: "CallDoctorRegular");

            migrationBuilder.DropTable(
                name: "CallRowConfig");

            migrationBuilder.DropTable(
                name: "CallSerialNoRule");

            migrationBuilder.DropTable(
                name: "CallCallInfo");

            migrationBuilder.DropTable(
                name: "CallDepartment");
        }
    }
}
