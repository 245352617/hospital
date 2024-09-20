using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class ADD_InformPatInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Triage_InformPatInfo",
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
                    PatientInfoId = table.Column<Guid>(nullable: false),
                    SendToHospital = table.Column<string>(maxLength: 200, nullable: true, comment: "送往医院"),
                    Source = table.Column<string>(maxLength: 50, nullable: true, comment: "告知患者来源"),
                    WarningLv = table.Column<string>(maxLength: 50, nullable: true, comment: "预警级别"),
                    CarNum = table.Column<string>(maxLength: 50, nullable: true, comment: "车牌号"),
                    PatientName = table.Column<string>(maxLength: 50, nullable: true, comment: "患者姓名"),
                    GreenRoadName = table.Column<string>(maxLength: 50, nullable: true, comment: "绿通名称"),
                    Narration = table.Column<string>(maxLength: 50, nullable: true, comment: "电话判断(调度判断)"),
                    HelpCauseName = table.Column<string>(maxLength: 50, nullable: true, comment: "呼救原因(主诉)"),
                    Gender = table.Column<string>(maxLength: 50, nullable: true, comment: "性别"),
                    Age = table.Column<string>(maxLength: 50, nullable: true, comment: "年龄"),
                    InformTime = table.Column<DateTime>(nullable: false),
                    ExpectedTime = table.Column<DateTime>(nullable: true),
                    PreparationContent = table.Column<string>(maxLength: 200, nullable: true, comment: "准备内容"),
                    ConsultationDept = table.Column<string>(maxLength: 200, nullable: true, comment: "会诊科室"),
                    CarCard = table.Column<string>(maxLength: 60, nullable: true, comment: "车辆编码"),
                    DiseaseIdentification = table.Column<string>(maxLength: 200, nullable: true, comment: "病种判断"),
                    CarPhone = table.Column<string>(nullable: true),
                    ContactsPhone = table.Column<string>(nullable: true),
                    DoctorName = table.Column<string>(maxLength: 50, nullable: true, comment: "医生"),
                    NurseName = table.Column<string>(maxLength: 50, nullable: true, comment: "护士"),
                    SiteAddress = table.Column<string>(maxLength: 200, nullable: true, comment: "现场地址"),
                    GLU = table.Column<float>(nullable: false),
                    ArriveTime = table.Column<DateTime>(nullable: true),
                    BoardingTime = table.Column<DateTime>(nullable: true),
                    ArriveHospitalTime = table.Column<DateTime>(nullable: true),
                    SamplingTime = table.Column<DateTime>(nullable: true),
                    UploadTime = table.Column<DateTime>(nullable: true),
                    ReportTime = table.Column<DateTime>(nullable: true),
                    Pid = table.Column<Guid>(nullable: false),
                    TaskInfoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_InformPatInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triage_InformPatInfo_Triage_PatientInfo_PatientInfoId",
                        column: x => x.PatientInfoId,
                        principalTable: "Triage_PatientInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "告知单患者");

            migrationBuilder.CreateIndex(
                name: "IX_Triage_InformPatInfo_PatientInfoId",
                table: "Triage_InformPatInfo",
                column: "PatientInfoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Triage_InformPatInfo");
        }
    }
}
