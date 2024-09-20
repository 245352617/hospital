using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HisApiMockService.Migrations
{
    /// <inheritdoc />
    public partial class addtablebylgzx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<string>(type: "TEXT", nullable: true),
                    PatientName = table.Column<string>(type: "TEXT", nullable: true),
                    Py = table.Column<string>(type: "TEXT", nullable: true),
                    Sex = table.Column<string>(type: "TEXT", nullable: true),
                    IdentityNo = table.Column<string>(type: "TEXT", nullable: true),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ContactsPhone = table.Column<string>(type: "TEXT", nullable: true),
                    ChargeType = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Nation = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    CountryCode = table.Column<string>(type: "TEXT", nullable: true),
                    CardNo = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<string>(type: "TEXT", nullable: true),
                    ErrorMsg = table.Column<string>(type: "TEXT", nullable: true),
                    StartTriageTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildPatient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargeDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DrugCode = table.Column<string>(type: "TEXT", nullable: true),
                    ChargeDetailNo = table.Column<string>(type: "TEXT", nullable: true),
                    FirmID = table.Column<string>(type: "TEXT", nullable: true),
                    DrugQuantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    DrugPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    DrugTotamount = table.Column<decimal>(type: "TEXT", nullable: false),
                    DrugChannel = table.Column<string>(type: "TEXT", nullable: true),
                    DrugUsageDic = table.Column<string>(type: "TEXT", nullable: true),
                    DrugGroupNo = table.Column<string>(type: "TEXT", nullable: true),
                    PharSpec = table.Column<string>(type: "TEXT", nullable: true),
                    PharmUnit = table.Column<string>(type: "TEXT", nullable: true),
                    PackageAmount = table.Column<string>(type: "TEXT", nullable: true),
                    SkinTest = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyFrequency = table.Column<string>(type: "TEXT", nullable: true),
                    PrimaryDose = table.Column<string>(type: "TEXT", nullable: true),
                    RestrictedDrugs = table.Column<int>(type: "INTEGER", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Storage = table.Column<int>(type: "INTEGER", nullable: false),
                    DrugCode = table.Column<string>(type: "TEXT", nullable: true),
                    DrugName = table.Column<string>(type: "TEXT", nullable: true),
                    DrugSpec = table.Column<string>(type: "TEXT", nullable: true),
                    MinPackageUnit = table.Column<string>(type: "TEXT", nullable: true),
                    FirmID = table.Column<string>(type: "TEXT", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    RetailPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    PharSpec = table.Column<string>(type: "TEXT", nullable: true),
                    PharmUnit = table.Column<string>(type: "TEXT", nullable: true),
                    PackageAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinPackageIndicator = table.Column<int>(type: "INTEGER", nullable: false),
                    Dosage = table.Column<int>(type: "INTEGER", nullable: false),
                    DosageUnit = table.Column<string>(type: "TEXT", nullable: true),
                    DrugDose = table.Column<decimal>(type: "TEXT", nullable: false),
                    DrugUnit = table.Column<string>(type: "TEXT", nullable: true),
                    ReturnDesc = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalInfoStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisSerialNo = table.Column<string>(type: "TEXT", nullable: true),
                    ChannelBillId = table.Column<string>(type: "TEXT", nullable: true),
                    HisBillId = table.Column<string>(type: "TEXT", nullable: true),
                    PatientName = table.Column<string>(type: "TEXT", nullable: true),
                    DeptCode = table.Column<string>(type: "TEXT", nullable: true),
                    DoctorCode = table.Column<string>(type: "TEXT", nullable: true),
                    BillState = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInfoStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectDetailNo = table.Column<string>(type: "TEXT", nullable: true),
                    GroupsId = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectType = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectMerge = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectMain = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProjectQuantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProjectTotamount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: true),
                    RestrictedDrugs = table.Column<int>(type: "INTEGER", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordStatusRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecordType = table.Column<int>(type: "INTEGER", nullable: false),
                    VisSerialNo = table.Column<string>(type: "TEXT", nullable: false),
                    PatientId = table.Column<string>(type: "TEXT", nullable: false),
                    OperatorCode = table.Column<string>(type: "TEXT", nullable: false),
                    DeptCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordStatusRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendMedicalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisSerialNo = table.Column<string>(type: "TEXT", nullable: true),
                    PatientId = table.Column<string>(type: "TEXT", nullable: true),
                    DoctorCode = table.Column<string>(type: "TEXT", nullable: true),
                    DeptCode = table.Column<string>(type: "TEXT", nullable: true),
                    DoctorName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendMedicalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OperatorDate = table.Column<string>(type: "TEXT", nullable: false),
                    RecordState = table.Column<int>(type: "INTEGER", nullable: false),
                    RecordNo = table.Column<string>(type: "TEXT", nullable: false),
                    RecordDetailNo = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateRecordStatusRequestId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordInfo_RecordStatusRequest_UpdateRecordStatusRequestId",
                        column: x => x.UpdateRecordStatusRequestId,
                        principalTable: "RecordStatusRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DrugItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DrugType = table.Column<int>(type: "INTEGER", nullable: false),
                    PrescriptionNo = table.Column<string>(type: "TEXT", nullable: true),
                    Storage = table.Column<string>(type: "TEXT", nullable: true),
                    PrescriptionDate = table.Column<string>(type: "TEXT", nullable: true),
                    PrescriptionDays = table.Column<int>(type: "INTEGER", nullable: false),
                    PrescriptionType = table.Column<int>(type: "INTEGER", nullable: false),
                    AgencyPeopleName = table.Column<string>(type: "TEXT", nullable: true),
                    AgencyPeopleCard = table.Column<string>(type: "TEXT", nullable: true),
                    AgencyPeopleSex = table.Column<int>(type: "INTEGER", nullable: false),
                    AgencyPeopleAge = table.Column<int>(type: "INTEGER", nullable: false),
                    AgencyPeopleMobile = table.Column<string>(type: "TEXT", nullable: true),
                    DrugAdministration = table.Column<int>(type: "INTEGER", nullable: false),
                    DrugDecoct = table.Column<int>(type: "INTEGER", nullable: false),
                    ChargeDetailId = table.Column<int>(type: "INTEGER", nullable: false),
                    SendMedicalInfoRequestId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugItem_ChargeDetail_ChargeDetailId",
                        column: x => x.ChargeDetailId,
                        principalTable: "ChargeDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugItem_SendMedicalInfo_SendMedicalInfoRequestId",
                        column: x => x.SendMedicalInfoRequestId,
                        principalTable: "SendMedicalInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExecuteDeptCode = table.Column<string>(type: "TEXT", nullable: false),
                    EmergencySign = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupType = table.Column<string>(type: "TEXT", nullable: false),
                    GroupId = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectItemNo = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectDetailId = table.Column<int>(type: "INTEGER", nullable: false),
                    SendMedicalInfoRequestId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectItem_ProjectDetail_ProjectDetailId",
                        column: x => x.ProjectDetailId,
                        principalTable: "ProjectDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectItem_SendMedicalInfo_SendMedicalInfoRequestId",
                        column: x => x.SendMedicalInfoRequestId,
                        principalTable: "SendMedicalInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugItem_ChargeDetailId",
                table: "DrugItem",
                column: "ChargeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugItem_SendMedicalInfoRequestId",
                table: "DrugItem",
                column: "SendMedicalInfoRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectItem_ProjectDetailId",
                table: "ProjectItem",
                column: "ProjectDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectItem_SendMedicalInfoRequestId",
                table: "ProjectItem",
                column: "SendMedicalInfoRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordInfo_UpdateRecordStatusRequestId",
                table: "RecordInfo",
                column: "UpdateRecordStatusRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildPatient");

            migrationBuilder.DropTable(
                name: "DrugItem");

            migrationBuilder.DropTable(
                name: "DrugStock");

            migrationBuilder.DropTable(
                name: "MedicalInfoStatus");

            migrationBuilder.DropTable(
                name: "ProjectItem");

            migrationBuilder.DropTable(
                name: "RecordInfo");

            migrationBuilder.DropTable(
                name: "ChargeDetail");

            migrationBuilder.DropTable(
                name: "ProjectDetail");

            migrationBuilder.DropTable(
                name: "SendMedicalInfo");

            migrationBuilder.DropTable(
                name: "RecordStatusRequest");
        }
    }
}
