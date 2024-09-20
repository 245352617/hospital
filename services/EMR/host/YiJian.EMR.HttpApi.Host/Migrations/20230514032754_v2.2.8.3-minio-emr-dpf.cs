using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.EMR.Migrations
{
    public partial class v2283minioemrdpf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrMinioEmrInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientEmrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmrTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "病历名称"),
                    MinioUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "患者编号"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "患者名称"),
                    DoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医生编号"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医生名称"),
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
                    table.PrimaryKey("PK_EmrMinioEmrInfo", x => x.Id);
                },
                comment: "Minio对象存储采集表");

            migrationBuilder.CreateIndex(
                name: "IX_EmrMinioEmrInfo_PatientEmrId",
                table: "EmrMinioEmrInfo",
                column: "PatientEmrId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrMinioEmrInfo");
        }
    }
}
