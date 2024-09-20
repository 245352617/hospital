using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_NurseHandover_1120 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Handover_NursePatients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Handover_NurseHandovers",
                table: "Handover_NurseHandovers");

            migrationBuilder.DropIndex(
                name: "IX_Handover_NurseHandovers_HandoverDate",
                table: "Handover_NurseHandovers");

            migrationBuilder.RenameTable(
                name: "Handover_NurseHandovers",
                newName: "HandoverNurseHandovers");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPatient",
                table: "HandoverNurseHandovers",
                type: "int",
                nullable: false,
                comment: "查询的全部患者",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "所有患者数量");

            migrationBuilder.AlterColumn<string>(
                name: "ShiftSettingName",
                table: "HandoverNurseHandovers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "班次名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldComment: "班次名称");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HandoverDate",
                table: "HandoverNurseHandovers",
                type: "datetime2",
                nullable: false,
                comment: "交班日期",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldComment: "交班日期");

            migrationBuilder.AlterColumn<string>(
                name: "CreationName",
                table: "HandoverNurseHandovers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "创建人名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "CreationCode",
                table: "HandoverNurseHandovers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "创建人编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "HandoverNurseHandovers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "年龄");

            migrationBuilder.AddColumn<string>(
                name: "AreaCode",
                table: "HandoverNurseHandovers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "区域编码");

            migrationBuilder.AddColumn<string>(
                name: "AreaName",
                table: "HandoverNurseHandovers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "区域名称");

            migrationBuilder.AddColumn<string>(
                name: "Assessment",
                table: "HandoverNurseHandovers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "评估");

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "HandoverNurseHandovers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "背景");

            migrationBuilder.AddColumn<string>(
                name: "Bed",
                table: "HandoverNurseHandovers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "床号");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "HandoverNurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "交班内容");

            migrationBuilder.AddColumn<bool>(
                name: "CriticallyIll",
                table: "HandoverNurseHandovers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否病危");

            migrationBuilder.AddColumn<string>(
                name: "Devices",
                table: "HandoverNurseHandovers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "设备");

            migrationBuilder.AddColumn<string>(
                name: "DiagnoseName",
                table: "HandoverNurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "诊断");

            migrationBuilder.AddColumn<string>(
                name: "Emr",
                table: "HandoverNurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "电子病历");

            migrationBuilder.AddColumn<string>(
                name: "HandoverNurseCode",
                table: "HandoverNurseHandovers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "交班护士编码");

            migrationBuilder.AddColumn<string>(
                name: "HandoverNurseName",
                table: "HandoverNurseHandovers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "交班护士名称");

            migrationBuilder.AddColumn<DateTime>(
                name: "HandoverTime",
                table: "HandoverNurseHandovers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "交班时间");

            migrationBuilder.AddColumn<DateTime>(
                name: "InDeptTime",
                table: "HandoverNurseHandovers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "入科时间");

            migrationBuilder.AddColumn<string>(
                name: "InOutVolume",
                table: "HandoverNurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "出入量");

            migrationBuilder.AddColumn<string>(
                name: "Inspect",
                table: "HandoverNurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "检查");

            migrationBuilder.AddColumn<bool>(
                name: "IsNoThree",
                table: "HandoverNurseHandovers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否三无");

            migrationBuilder.AddColumn<string>(
                name: "LatestStatus",
                table: "HandoverNurseHandovers",
                type: "ntext",
                maxLength: 4000,
                nullable: true,
                comment: "最新现状");

            migrationBuilder.AddColumn<string>(
                name: "Medicine",
                table: "HandoverNurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "药物");

            migrationBuilder.AddColumn<Guid>(
                name: "PI_ID",
                table: "HandoverNurseHandovers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "triage分诊患者id");

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "HandoverNurseHandovers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "患者id");

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "HandoverNurseHandovers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "患者姓名");

            migrationBuilder.AddColumn<string>(
                name: "Proposal",
                table: "HandoverNurseHandovers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "建议");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "HandoverNurseHandovers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "性别编码");

            migrationBuilder.AddColumn<string>(
                name: "SexName",
                table: "HandoverNurseHandovers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "性别名称");

            migrationBuilder.AddColumn<string>(
                name: "SuccessionNurseCode",
                table: "HandoverNurseHandovers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "接班护士编码");

            migrationBuilder.AddColumn<string>(
                name: "SuccessionNurseName",
                table: "HandoverNurseHandovers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "接班护士名称");

            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "HandoverNurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "检验");

            migrationBuilder.AddColumn<string>(
                name: "TriageLevel",
                table: "HandoverNurseHandovers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "分诊级别");

            migrationBuilder.AddColumn<string>(
                name: "TriageLevelName",
                table: "HandoverNurseHandovers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "分诊级别名称");

            migrationBuilder.AddColumn<int>(
                name: "VisitNo",
                table: "HandoverNurseHandovers",
                type: "int",
                nullable: true,
                comment: "就诊号");

            migrationBuilder.AddColumn<string>(
                name: "VitalSigns",
                table: "HandoverNurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                comment: "生命体征");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HandoverNurseHandovers",
                table: "HandoverNurseHandovers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HandoverNurseHandovers_PI_ID",
                table: "HandoverNurseHandovers",
                column: "PI_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HandoverNurseHandovers",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropIndex(
                name: "IX_HandoverNurseHandovers_PI_ID",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "AreaCode",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "AreaName",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Assessment",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Bed",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "CriticallyIll",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Devices",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "DiagnoseName",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Emr",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "HandoverNurseCode",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "HandoverNurseName",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "HandoverTime",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "InDeptTime",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "InOutVolume",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Inspect",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "IsNoThree",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "LatestStatus",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Medicine",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "PI_ID",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Proposal",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "SexName",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "SuccessionNurseCode",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "SuccessionNurseName",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "Test",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "TriageLevel",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "TriageLevelName",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "VisitNo",
                table: "HandoverNurseHandovers");

            migrationBuilder.DropColumn(
                name: "VitalSigns",
                table: "HandoverNurseHandovers");

            migrationBuilder.RenameTable(
                name: "HandoverNurseHandovers",
                newName: "Handover_NurseHandovers");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPatient",
                table: "Handover_NurseHandovers",
                type: "int",
                nullable: false,
                comment: "所有患者数量",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "查询的全部患者");

            migrationBuilder.AlterColumn<string>(
                name: "ShiftSettingName",
                table: "Handover_NurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                comment: "班次名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "班次名称");

            migrationBuilder.AlterColumn<string>(
                name: "HandoverDate",
                table: "Handover_NurseHandovers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                comment: "交班日期",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "交班日期");

            migrationBuilder.AlterColumn<string>(
                name: "CreationName",
                table: "Handover_NurseHandovers",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "创建人名称");

            migrationBuilder.AlterColumn<string>(
                name: "CreationCode",
                table: "Handover_NurseHandovers",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "创建人编码");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Handover_NurseHandovers",
                table: "Handover_NurseHandovers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Handover_NursePatients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "年龄"),
                    AreaCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "区域编码"),
                    AreaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "区域名称"),
                    Assessment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "评估"),
                    Background = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "背景"),
                    Bed = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "床号"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "交班内容"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CriticallyIll = table.Column<bool>(type: "bit", nullable: false, comment: "是否病危"),
                    Devices = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "设备"),
                    DiagnoseName = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "诊断"),
                    Emr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "电子病历"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandoverNurseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "交班护士编码"),
                    HandoverNurseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "交班护士名称"),
                    HandoverTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "交班时间"),
                    InDeptTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "入科时间"),
                    InOutVolume = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "出入量"),
                    Inspect = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "检查"),
                    IsNoThree = table.Column<bool>(type: "bit", nullable: false, comment: "是否三无"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LatestStatus = table.Column<string>(type: "ntext", maxLength: 4000, nullable: true, comment: "最新现状"),
                    Medicine = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "药物"),
                    NurseHandoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护士交班id"),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "triage分诊患者id"),
                    PatientId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "患者id"),
                    PatientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "患者姓名"),
                    Proposal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "建议"),
                    Sex = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "性别"),
                    SexName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "性别名称"),
                    SuccessionNurseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "接班护士编码"),
                    SuccessionNurseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "接班护士名称"),
                    Test = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "检验"),
                    TriageLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "分诊级别"),
                    TriageLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "分诊级别名称"),
                    VisitNo = table.Column<int>(type: "int", nullable: true, comment: "就诊号"),
                    VitalSigns = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "生命体征")
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
                comment: "交班患者");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_NurseHandovers_HandoverDate",
                table: "Handover_NurseHandovers",
                column: "HandoverDate");

            migrationBuilder.CreateIndex(
                name: "IX_Handover_NursePatients_NurseHandoverId",
                table: "Handover_NursePatients",
                column: "NurseHandoverId");
        }
    }
}
