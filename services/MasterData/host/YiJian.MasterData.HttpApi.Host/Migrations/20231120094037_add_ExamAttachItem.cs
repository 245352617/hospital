using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class add_ExamAttachItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_ExamAttachItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "检查编码"),
                    MedicineCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "药品或处置编码"),
                    Qty = table.Column<float>(type: "real", nullable: false, comment: "剂量或数量"),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "类型")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ExamAttachItem", x => x.Id);
                },
                comment: "检查附加项");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamAttachItem_ProjectCode",
                table: "Dict_ExamAttachItem",
                column: "ProjectCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ExamAttachItem");
        }
    }
}
