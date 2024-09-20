using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddCovid19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Triage_Covid19Exam",
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
                    PatientId = table.Column<string>(nullable: true),
                    PatientName = table.Column<string>(maxLength: 50, nullable: true, comment: "患者姓名"),
                    ContactsPhone = table.Column<string>(maxLength: 20, nullable: true, comment: "联系电话"),
                    IdentityNo = table.Column<string>(maxLength: 20, nullable: true, comment: "身份证号"),
                    Temperature = table.Column<decimal>(nullable: false, comment: "体温"),
                    IsHot = table.Column<bool>(nullable: false, comment: "是否发热"),
                    IsCough = table.Column<bool>(nullable: false, comment: "是否干咳"),
                    IsFeeble = table.Column<bool>(nullable: false, comment: "是否乏力"),
                    IsHearingAndSmellingLoss = table.Column<bool>(nullable: false, comment: "是否嗅觉、味觉减退"),
                    IsStuffyNose = table.Column<bool>(nullable: false, comment: "是否鼻塞"),
                    IsNoseRunning = table.Column<bool>(nullable: false, comment: "是否流涕"),
                    IsSoreThroat = table.Column<bool>(nullable: false, comment: "是否咽痛"),
                    IsConjunctivitis = table.Column<bool>(nullable: false, comment: "是否结膜炎"),
                    IsMusclePain = table.Column<bool>(nullable: false, comment: "是否肌痛"),
                    IsDiarrhea = table.Column<bool>(nullable: false, comment: "是否腹泻"),
                    IsMediumAndHighRisk = table.Column<bool>(nullable: false, comment: "近14天内您是否去过境外以及境内中高风险地区，或有病例报告的社区"),
                    IsContactHotPatient = table.Column<bool>(nullable: false, comment: "近14天内您是否接触过来自境外以及境内中高风险地区的人员"),
                    IsContactNewCoronavirus = table.Column<bool>(nullable: false, comment: "近14天内是否接触过确诊病例或无症状感染者(核酸检测阳性者)"),
                    IsAggregation = table.Column<bool>(nullable: false, comment: "14天内您的家庭、办公室、学校或托幼机构班次、车间等集体单位是否出现2例及以上发热和/或呼吸道症状的聚集性病例"),
                    BeenAbroadStatus = table.Column<int>(nullable: false, defaultValue: 0, comment: "14天内是否有境外旅居史（0: 境内本市；1: 境内非本市; 2:  境外）"),
                    CountrySpecific = table.Column<string>(maxLength: 1024, nullable: true, comment: "具体国家或地区"),
                    ProvinceSpecific = table.Column<string>(maxLength: 1024, nullable: true, comment: "省"),
                    CitySpecific = table.Column<string>(maxLength: 1024, nullable: true, comment: "市"),
                    DistrictSpecific = table.Column<string>(maxLength: 1024, nullable: true, comment: "区"),
                    PatientSignPicture = table.Column<string>(maxLength: 1024, nullable: true, comment: "患者签名（图片地址或base64）"),
                    DoctorSignPicture = table.Column<string>(nullable: true, comment: "接诊医生签名（图片地址或base64）"),
                    Date = table.Column<DateTime>(nullable: false, comment: "日期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_Covid19Exam", x => x.Id);
                },
                comment: "新冠问卷");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Triage_Covid19Exam");
        }
    }
}
