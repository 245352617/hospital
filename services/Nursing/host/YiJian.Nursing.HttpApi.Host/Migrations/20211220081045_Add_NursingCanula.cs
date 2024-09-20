using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class Add_NursingCanula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Duct_NursingCanula",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者id"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "插管时间"),
                    StopTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "拔管时间"),
                    ModuleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "导管分类"),
                    ModuleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "排序"),
                    CanulaName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "管道名称"),
                    CanulaPart = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "插管部位"),
                    CanulaNumber = table.Column<int>(type: "int", nullable: true, comment: "插管次数"),
                    CanulaPosition = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "插管地点"),
                    DoctorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "置管人代码"),
                    DoctorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "置管人名称"),
                    CanulaWay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "置入方式"),
                    CanulaLength = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "置管长度"),
                    DrawReason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "拔管原因"),
                    TubeDrawState = table.Column<int>(type: "int", nullable: false, comment: "管道状态"),
                    UseFlag = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "使用标志：（Y在用，N已拔管）"),
                    NurseId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "护士Id"),
                    NurseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "护士名称"),
                    NurseTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "护理时间"),
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
                    table.PrimaryKey("PK_Duct_NursingCanula", x => x.Id);
                },
                comment: "表:导管护理信息");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Duct_NursingCanula");
        }
    }
}
