using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations
{
    public partial class v2280unuseadvices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_EmrUsedAdviceRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RC_EmrUsedAdviceRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "使用人（导入人）"),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "使用人（导入人）"),
                    DoctorsAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱Id"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者唯一ID"),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "使用时间（导入时间）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_EmrUsedAdviceRecord", x => x.Id);
                },
                comment: "使用过的医嘱记录信息");

            migrationBuilder.CreateIndex(
                name: "IX_RC_EmrUsedAdviceRecord_DoctorsAdviceId",
                table: "RC_EmrUsedAdviceRecord",
                column: "DoctorsAdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_EmrUsedAdviceRecord_PIID",
                table: "RC_EmrUsedAdviceRecord",
                column: "PIID");
        }
    }
}
