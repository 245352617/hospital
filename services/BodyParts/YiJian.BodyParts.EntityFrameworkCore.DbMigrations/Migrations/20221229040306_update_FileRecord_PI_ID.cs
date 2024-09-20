using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.BodyParts.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class update_FileRecord_PI_ID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanulaDynamic");

            migrationBuilder.DropTable(
                name: "IcuCanula");

            migrationBuilder.DropTable(
                name: "IcuNursingCanula");

            migrationBuilder.DropTable(
                name: "IcuPatientRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanulaDynamic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键"),
                    CanulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键"),
                    GroupName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "管道分类"),
                    ParaCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "参数代码"),
                    ParaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "参数名称"),
                    ParaValue = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "参数值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanulaDynamic", x => x.Id);
                },
                comment: "导管动态列表");

            migrationBuilder.CreateTable(
                name: "IcuCanula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键"),
                    CanulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "导管主表主键"),
                    CanulaLength = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "置管长度"),
                    CanulaWay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "置入方式"),
                    NurseId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护士名称"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "护理时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuCanula", x => x.Id);
                },
                comment: "导管护理记录信息");

            migrationBuilder.CreateTable(
                name: "IcuNursingCanula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键"),
                    CanulaLength = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "置管长度"),
                    CanulaName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "管道名称"),
                    CanulaNumber = table.Column<int>(type: "int", nullable: true, comment: "插管次数"),
                    CanulaPart = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "插管部位"),
                    CanulaPosition = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "插管地点"),
                    CanulaWay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "置入方式"),
                    DoctorId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "置管人代码"),
                    DoctorName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "置管人名称"),
                    DrawReason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "拔管原因"),
                    ModuleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "导管分类"),
                    ModuleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "导管名称"),
                    NurseId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护士名称"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "护理时间"),
                    PI_ID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "患者id"),
                    RiskLevel = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "风险级别 默认空，1低危 2中危 3高危"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "插管时间"),
                    StopTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "拔管时间"),
                    TubeDrawState = table.Column<int>(type: "int", nullable: false, comment: "管道状态（0拔管，1换管，2取消拔管，-1其他）"),
                    UseFlag = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "使用标志：（Y在用，N已拔管）"),
                    ValidState = table.Column<int>(type: "int", nullable: false, comment: "有效状态（1-有效，0-无效）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuNursingCanula", x => x.Id);
                },
                comment: "导管护理信息");

            migrationBuilder.CreateTable(
                name: "IcuPatientRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID"),
                    Age = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "年龄"),
                    Allergy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "过敏史"),
                    ArchiveId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "档案号"),
                    BedNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "采集器(绑定床位)的编号 "),
                    ClinicDiagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "临床诊断"),
                    CriticaDegree = table.Column<int>(type: "int", nullable: true, comment: "危重程度（0：其他，1：病危，2：病重）"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室代码"),
                    DeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "科室名称"),
                    DoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "主管医生"),
                    DoctorName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "主管医生名称"),
                    Height = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "身高"),
                    InDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "来源科室代码"),
                    InDeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "来源科室名称"),
                    InDeptNurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "入科交接护士代码"),
                    InDeptNurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "入科交接护士名称"),
                    InDeptState = table.Column<int>(type: "int", nullable: false, comment: "是否在科（1：在科；0：出科；2，取消入科，3:待入科，4:待出科)"),
                    InDeptTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "入科时间"),
                    InDeptTransferTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "入科交接时间"),
                    InHosId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "住院号"),
                    InPlan = table.Column<int>(type: "int", nullable: true, comment: "入科计划（0：非计划转入，1：计划转入）"),
                    InReason = table.Column<int>(type: "int", nullable: true, comment: "转入原因（非计划转入原因：1：缺少病情变化的预警，2：手术因素，3：麻醉因素；计划转入原因：4：危及生命的急性器官功能不全，5：具有潜在危及生命的高危因素，6：慢性器官功能不全急性加重，7：围手术期危重患者，0：其他）"),
                    InReturn = table.Column<int>(type: "int", nullable: true, comment: "重返（0：否，1：24小时重返，2：48小时重返）"),
                    InSource = table.Column<int>(type: "int", nullable: true, comment: "入科来源（7-入院、5-手术、6-外院转入、8-转入）"),
                    InStandard = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "转入标准"),
                    Indiagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "入科诊断"),
                    InsulateFlag = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "隔离标志（Y ：隔离，N：不隔离）"),
                    IsDoctorConfirm = table.Column<bool>(type: "bit", nullable: false, comment: "是否医生确认"),
                    NurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "责任护士"),
                    NurseGrade = table.Column<int>(type: "int", nullable: true, comment: "护理级别（1：一级护理，2：二级护理，3：三级护理，4.特级护理）"),
                    NurseName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "责任护士名称"),
                    NurseType = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "护理类型（0：其他，1：基础护理，2：特殊护理，3：辩证施护）"),
                    OperationNurseId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "最后操作人ID"),
                    OperationNurseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "最后操作人名称"),
                    OperationState = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true, comment: "病人操作状态(Y：锁住，N：解锁)"),
                    OperationTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "最后操作时间"),
                    OutDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "转出科室"),
                    OutDeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "转出科室名称"),
                    OutDeptNurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "出科交接护士工号"),
                    OutDeptNurseName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "出科交接护士名称"),
                    OutDeptTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "出科时间"),
                    OutDeptTransferTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "出科交接时间"),
                    OutStandard = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "转出标准"),
                    OutState = table.Column<int>(type: "int", nullable: true, comment: "出科状态(1：恶化，2：好转，3：未愈)"),
                    OutTurnover = table.Column<int>(type: "int", nullable: true, comment: "出科转归(1：出院，2：转出，3：死亡，4：转上级医院)"),
                    Outdiagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "出科诊断"),
                    PI_ID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "患者id(通过业务构造的流水号，每个患者每次入科号码唯一)"),
                    PatState = table.Column<int>(type: "int", nullable: true, comment: "病人状态 1:正常入科(入院/入科转icu实床) 2:紧急入科 3:虚床转实床 4:病人紧急入科，通过消息更新后"),
                    PayType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "付费方式"),
                    PreHistory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "既往史"),
                    PrePayMent = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, comment: "预交金额"),
                    SpDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "专科医生"),
                    SpDoctorName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "专科医生名称"),
                    TotalCost = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, comment: "已消费"),
                    UnsettledCost = table.Column<decimal>(type: "decimal(18, 2)", nullable: true, comment: "结余"),
                    VisitNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "就诊号"),
                    VisitTimes = table.Column<int>(type: "int", nullable: true, comment: "住院次数"),
                    WardCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "病区代码"),
                    Weight = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "体重")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuPatientRecord", x => x.Id);
                },
                comment: "患者记录表");

            migrationBuilder.CreateIndex(
                name: "Index_CanulaId",
                table: "IcuCanula",
                column: "CanulaId");

            migrationBuilder.CreateIndex(
                name: "Index_NurseTime",
                table: "IcuCanula",
                column: "NurseTime");

            migrationBuilder.CreateIndex(
                name: "Index_PI_ID",
                table: "IcuNursingCanula",
                column: "PI_ID");

            migrationBuilder.CreateIndex(
                name: "Index_ValidState",
                table: "IcuNursingCanula",
                column: "ValidState");

            migrationBuilder.CreateIndex(
                name: "Index_ArchiveId",
                table: "IcuPatientRecord",
                column: "ArchiveId");

            migrationBuilder.CreateIndex(
                name: "Index_InDeptState",
                table: "IcuPatientRecord",
                column: "InDeptState");

            migrationBuilder.CreateIndex(
                name: "Index_InDeptTime",
                table: "IcuPatientRecord",
                column: "InDeptTime");

            migrationBuilder.CreateIndex(
                name: "Index_InHosId",
                table: "IcuPatientRecord",
                column: "InHosId");

            migrationBuilder.CreateIndex(
                name: "Index_PI_ID",
                table: "IcuPatientRecord",
                column: "PI_ID");

            migrationBuilder.CreateIndex(
                name: "Index_VisitNum",
                table: "IcuPatientRecord",
                column: "VisitNum");
        }
    }
}
