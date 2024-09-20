using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v210addtableemrbaseinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrEmrBaseInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrgCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, defaultValue: "H7110", comment: "机构ID,固定：H7110"),
                    DeptCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "书写科室:一级科室代码"),
                    DoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "书写医生:书写医生工号"),
                    PatientId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "病人ID:His内部识别id"),
                    VisitNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "就诊号"),
                    RegisterNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "挂号识别号"),
                    ChiefComplaint = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "主诉"),
                    HistoryPresentIllness = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "现病史"),
                    AllergySign = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "药物过敏史"),
                    MedicalHistory = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "既往史"),
                    BodyExam = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "体格体查"),
                    PreliminaryDiagnosis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "初步诊断"),
                    HandlingOpinions = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "处理意见"),
                    OutpatientSurgery = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "门诊手术"),
                    AuxiliaryExamination = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "辅助检查"),
                    Channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "SZKJ", comment: "渠道来源,尚哲：SZKJ"),
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
                    table.PrimaryKey("PK_EmrEmrBaseInfo", x => x.Id);
                },
                comment: "电子病例采集的基础信息");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrEmrBaseInfo");
        }
    }
}
