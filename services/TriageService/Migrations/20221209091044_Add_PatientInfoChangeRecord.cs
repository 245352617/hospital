using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class Add_PatientInfoChangeRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Triage_PatientInfoChangeRecord",
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
                    PI_Id = table.Column<Guid>(nullable: false),
                    ChangeField = table.Column<string>(maxLength: 500, nullable: true, comment: "变更字段"),
                    BeforeValue = table.Column<string>(maxLength: 500, nullable: true, comment: "变更之前的值"),
                    AfterValue = table.Column<string>(maxLength: 500, nullable: true, comment: "变更之后的值"),
                    ChangeReason = table.Column<string>(maxLength: 500, nullable: true, comment: "变更原因"),
                    OperatedCode = table.Column<string>(maxLength: 500, nullable: true, comment: "操作人编码"),
                    OperatedName = table.Column<string>(maxLength: 500, nullable: true, comment: "操作人名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage_PatientInfoChangeRecord", x => x.Id);
                },
                comment: "病人信息变更记录表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Triage_PatientInfoChangeRecord");
        }
    }
}
