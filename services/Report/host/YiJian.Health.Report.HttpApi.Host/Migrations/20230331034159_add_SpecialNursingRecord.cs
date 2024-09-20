using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Health.Report.Migrations
{
    public partial class add_SpecialNursingRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NursingRelevanceId",
                table: "RpNursingRecord");

            migrationBuilder.CreateTable(
                name: "RpSpecialNursingRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Special = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "特殊护理记录"),
                    NursingRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "护理记录Id"),
                    NursingRelevanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "特殊护理记录关联Id"),
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
                    table.PrimaryKey("PK_RpSpecialNursingRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpSpecialNursingRecord_RpNursingRecord_NursingRecordId",
                        column: x => x.NursingRecordId,
                        principalTable: "RpNursingRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "特殊护理记录");

            migrationBuilder.CreateIndex(
                name: "IX_RpSpecialNursingRecord_NursingRecordId",
                table: "RpSpecialNursingRecord",
                column: "NursingRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpSpecialNursingRecord");

            migrationBuilder.AddColumn<Guid>(
                name: "NursingRelevanceId",
                table: "RpNursingRecord",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "护理记录关联Id");
        }
    }
}
