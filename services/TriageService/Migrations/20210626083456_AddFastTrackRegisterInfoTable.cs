using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddFastTrackRegisterInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Triage_FastTrackRegisterInfo",
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
                    PatientName = table.Column<string>(nullable: true, comment: "患者姓名"),
                    Sex = table.Column<string>(type: "varchar(20)", nullable: true, comment: "性别"),
                    Age = table.Column<string>(nullable: true, comment: "患者年龄"),
                    PoliceStationPhone = table.Column<string>(type: "varchar(20)", nullable: true, comment: "所属派出所电话号码"),
                    PoliceStationName = table.Column<string>(nullable: true, comment: "所处派出所名称"),
                    PoliceCode = table.Column<string>(type: "varchar(20)", nullable: true, comment: "警务人员警号"),
                    PoliceName = table.Column<string>(nullable: true, comment: "警务人员姓名"),
                    ReceptionNurse = table.Column<string>(type: "nvarchar(20)", nullable: true, comment: "接诊护士")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_FastTrackRegisterInfo", x => x.Id);
                },
                comment: "快速通道登记信息表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Triage_FastTrackRegisterInfo");
        }
    }
}
