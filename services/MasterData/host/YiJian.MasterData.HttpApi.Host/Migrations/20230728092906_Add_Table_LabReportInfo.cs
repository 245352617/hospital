using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class Add_Table_LabReportInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_LabReportInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "名称"),
                    Code = table.Column<int>(type: "int", nullable: false, comment: "编码"),
                    SampleCollectType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行科室编码"),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "注意信息"),
                    CatelogName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "指引单大类"),
                    ExecDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行科室名称"),
                    TestTubeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "试管名称"),
                    MergerNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "门诊合并号"),
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
                    table.PrimaryKey("PK_Dict_LabReportInfo", x => x.Id);
                },
                comment: "检验单信息");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabReportInfo_Id",
                table: "Dict_LabReportInfo",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_LabReportInfo");
        }
    }
}
