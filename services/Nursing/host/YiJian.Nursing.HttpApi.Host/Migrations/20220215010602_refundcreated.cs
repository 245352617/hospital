using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class refundcreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NursingRecipeExecRefund",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExecId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "关联医嘱执行表编号"),
                    RecipeNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱号"),
                    PIID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "病人标识"),
                    PlatformType = table.Column<int>(type: "int", nullable: false, comment: "系统标识: 0=急诊，1=院前"),
                    IsWithDrawn = table.Column<bool>(type: "bit", nullable: false, comment: "是退药"),
                    NursingRefundStatus = table.Column<int>(type: "int", nullable: false, comment: "退药退费状态"),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "申请时间"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "医嘱名称"),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "规格"),
                    RefundQty = table.Column<int>(type: "int", nullable: false, comment: "数量"),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "单价"),
                    NurseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "申请护士编码"),
                    NurseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "申请护士名称"),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "原因"),
                    ConfirmedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "确认时间"),
                    ConfirmmerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "确认人编码"),
                    ConfirmmerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "确认人名称"),
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
                    table.PrimaryKey("PK_NursingRecipeExecRefund", x => x.Id);
                },
                comment: "医嘱执行退款退费表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingRecipeExecRefund");
        }
    }
}
