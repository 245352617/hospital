using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class add_LabSpecimenPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_LabSpecimenPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "标本编码"),
                    SpecimenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "标本名称"),
                    PositionCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "采集部位编码"),
                    PositionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "采集部位名称"),
                    IndexNo = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_LabSpecimenPositions", x => x.Id);
                },
                comment: "检验标本采集部位");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabSpecimenPositions_SpecimenCode",
                table: "Dict_LabSpecimenPositions",
                column: "SpecimenCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_LabSpecimenPositions");
        }
    }
}
