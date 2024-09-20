using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class FastTrackSetting2021070716 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Config_FastTrackSetting",
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
                    FastTrackName = table.Column<string>(type: "varchar(100)", nullable: true, comment: "快速通道名称"),
                    FastTrackPhone = table.Column<string>(type: "varchar(20)", nullable: true, comment: "快速通道电话"),
                    PhoneAndName = table.Column<string>(type: "varchar(150)", nullable: true, comment: "快速通道电话和名称"),
                    IsEnable = table.Column<bool>(nullable: false, comment: "是否启用")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config_FastTrackSetting", x => x.Id);
                },
                comment: "快速通道");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config_FastTrackSetting");
        }
    }
}
