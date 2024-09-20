using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class v2277_Add_WardRound_WardRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RpWardRound",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "护理单Id(外键)"),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "签名"),
                    SheetIndex = table.Column<int>(type: "int", nullable: false, comment: "护理单记录索引"),
                    NursingDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理单Id(外键)"),
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
                    table.PrimaryKey("PK_RpWardRound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpWardRound_RpNursingDocument_NursingDocumentId",
                        column: x => x.NursingDocumentId,
                        principalTable: "RpNursingDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "查房信息");

            migrationBuilder.CreateIndex(
                name: "IX_RpWardRound_NursingDocumentId",
                table: "RpWardRound",
                column: "NursingDocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpWardRound");
        }
    }
}
