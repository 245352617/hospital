using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addownmedicine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RC_OwnMedicine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "系统标识: 0=急诊，1=院前"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者唯一标识"),
                    PatientId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者Id"),
                    PatientName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "患者名称"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "医嘱名称"),
                    ApplyTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "开嘱时间"),
                    ApplyDoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "申请医生编码"),
                    ApplyDoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "申请医生"),
                    ApplyDeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室编码"),
                    ApplyDeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请科室"),
                    RecieveQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "领量(数量)"),
                    RecieveUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "领量单位"),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法名称"),
                    DosageQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "每次剂量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂量单位"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次"),
                    FrequencyTimes = table.Column<int>(type: "int", nullable: true, comment: "在一个周期内执行的次数"),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "医嘱说明")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_OwnMedicine", x => x.Id);
                },
                comment: "自备药");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_OwnMedicine");
        }
    }
}
