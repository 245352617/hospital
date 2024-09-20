using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class add_region : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AddCard",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加卡片类型 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)14.新型冠状病毒RNA检测申请单13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "附加卡片类型10.注射单,皮试单  08.雾化申请单  09.输液卡");

            migrationBuilder.AlterColumn<string>(
                name: "AddCard",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加卡片类型 12.TCT细胞学检查申请单 11.病理检验申请单 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "附加卡片类型10.注射单,皮试单  08.雾化申请单  09.输液卡");

            migrationBuilder.CreateTable(
                name: "Sys_ReceivedLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "路由键"),
                    Queue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "队列"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true, comment: "内容"),
                    Retries = table.Column<int>(type: "int", nullable: false, comment: "重试次数"),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "新增时间"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "终止时间"),
                    StatusName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "状态")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ReceivedLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "区域编码"),
                    RegionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "区域名称"),
                    RegionType = table.Column<int>(type: "int", nullable: false, comment: "区域类型"),
                    ParentCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "父级编码")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Region", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_ReceivedLog");

            migrationBuilder.DropTable(
                name: "Sys_Region");

            migrationBuilder.AlterColumn<string>(
                name: "AddCard",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加卡片类型10.注射单,皮试单  08.雾化申请单  09.输液卡",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "附加卡片类型 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)14.新型冠状病毒RNA检测申请单13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单");

            migrationBuilder.AlterColumn<string>(
                name: "AddCard",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加卡片类型10.注射单,皮试单  08.雾化申请单  09.输液卡",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "附加卡片类型 12.TCT细胞学检查申请单 11.病理检验申请单 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用");
        }
    }
}
