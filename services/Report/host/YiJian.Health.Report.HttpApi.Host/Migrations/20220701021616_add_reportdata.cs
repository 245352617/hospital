using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class add_reportdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RpReportData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者分诊id"),
                    TempId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "模板id"),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Url"),
                    DataContent = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "数据"),
                    OperationCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "操作人编码"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpReportData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpReportData");
        }
    }
}
