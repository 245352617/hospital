using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class add_CloudSignInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrCloudSignInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EMRId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    BusinessTypeCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "深圳市CA业务类型编码"),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "病人Id"),
                    BizId = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "业务系统id"),
                    WithTsa = table.Column<bool>(type: "bit", nullable: false, comment: "是否进行时间戳签名"),
                    StatusCode = table.Column<int>(type: "int", nullable: false, comment: "云签结果状态码"),
                    EventMsg = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "状态信息"),
                    SignedData = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "签名值"),
                    Timestamp = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "时间戳签名值"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrCloudSignInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrCloudSignInfo");
        }
    }
}
