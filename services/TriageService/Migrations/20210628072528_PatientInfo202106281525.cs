using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class PatientInfo202106281525 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Consciousness",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "意识");

            migrationBuilder.AddColumn<string>(
                name: "FaberCode",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "费别");

            migrationBuilder.AddColumn<bool>(
                name: "IsNoThree",
                table: "Triage_PatientInfo",
                nullable: false,
                defaultValue: false,
                comment: "是否三无病人");

            migrationBuilder.AddColumn<string>(
                name: "Narration",
                table: "Triage_PatientInfo",
                type: "varchar(20)",
                nullable: true,
                comment: "主诉");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfVisitCode",
                table: "Triage_PatientInfo",
                nullable: true,
                comment: "就诊类型");

            migrationBuilder.CreateTable(
                name: "Triage_AdmissionInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sort = table.Column<int>(nullable: true, comment: "排序"),
                    Remark = table.Column<string>(maxLength: 256, nullable: true, comment: "备注"),
                    AddUser = table.Column<string>(maxLength: 50, nullable: true, comment: "添加人"),
                    CreationTime = table.Column<DateTime>(nullable: false, comment: "创建时间"),
                    ModUser = table.Column<string>(maxLength: 50, nullable: true, comment: "修改人"),
                    LastModificationTime = table.Column<DateTime>(nullable: true, comment: "修改时间"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false, comment: "是否删除"),
                    DeleteUser = table.Column<string>(maxLength: 50, nullable: true, comment: "删除人"),
                    DeletionTime = table.Column<DateTime>(nullable: true, comment: "删除时间"),
                    HospitalCode = table.Column<string>(maxLength: 250, nullable: true, comment: "医院编码"),
                    HospitalName = table.Column<string>(maxLength: 250, nullable: true, comment: "医院名称"),
                    ExtensionField1 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段1"),
                    ExtensionField2 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段2"),
                    ExtensionField3 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段3"),
                    ExtensionField4 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段4"),
                    ExtensionField5 = table.Column<string>(maxLength: 1000, nullable: true, comment: "扩展字段5"),
                    PatientInfoId = table.Column<Guid>(nullable: false, comment: "院前分诊患者建档表主键Id"),
                    MedicalHistory = table.Column<string>(type: "varchar(500)", nullable: true, comment: "现病史"),
                    PastMedicalHistory = table.Column<string>(type: "varchar(500)", nullable: true, comment: "既往史"),
                    IsSoreThroatAndCough = table.Column<bool>(nullable: false, comment: "是否咽痛咳嗽"),
                    IsHot = table.Column<bool>(nullable: false, comment: "是否发热"),
                    IsMediumAndHighRisk = table.Column<bool>(nullable: false, comment: "是否去过中高风险区"),
                    IsAggregation = table.Column<bool>(nullable: false, comment: "是否聚集性发病"),
                    IsContactHotPatient = table.Column<bool>(nullable: false, comment: "2周内是否接触过中高风险区发热患者"),
                    IsContactNewCoronavirus = table.Column<bool>(nullable: false, comment: "2周内是否接触过确诊新冠阳性患者"),
                    IsFocusIsolated = table.Column<bool>(nullable: false, comment: "最近14天内您是否在集中隔离医学观察场所留观"),
                    IsBeenAbroad = table.Column<bool>(nullable: false, comment: "2周内是否有境外旅居史"),
                    CountrySpecific = table.Column<string>(type: "varchar(200)", nullable: true, comment: "具体国家/地区"),
                    AbroadStartTime = table.Column<string>(nullable: true, comment: "境外开始日期"),
                    AbroadEndTime = table.Column<string>(nullable: true, comment: "境外结束日期"),
                    ReturnTime = table.Column<string>(nullable: true, comment: "回国日期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_AdmissionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triage_AdmissionInfo_Triage_PatientInfo_PatientInfoId",
                        column: x => x.PatientInfoId,
                        principalTable: "Triage_PatientInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "入院情况信息表");

            migrationBuilder.CreateIndex(
                name: "IX_Triage_AdmissionInfo_PatientInfoId",
                table: "Triage_AdmissionInfo",
                column: "PatientInfoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Triage_AdmissionInfo");

            migrationBuilder.DropColumn(
                name: "Consciousness",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "FaberCode",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "IsNoThree",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "Narration",
                table: "Triage_PatientInfo");

            migrationBuilder.DropColumn(
                name: "TypeOfVisitCode",
                table: "Triage_PatientInfo");
        }
    }
}
