using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class add_PacsPathologyItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RC_PacsPathologyItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "检查项Id"),
                    Specimen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "标本名称"),
                    DrawMaterialsPart = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "取材部位"),
                    SpecimenQty = table.Column<int>(type: "int", nullable: false, comment: "标本数量"),
                    LeaveTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "离体时间"),
                    RegularTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "固定时间"),
                    SpecificityInfect = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "特异性感染"),
                    ApplyForObjective = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "申请目的"),
                    Symptom = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "临床症状及体征")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_PacsPathologyItem", x => x.Id);
                },
                comment: "检查病理小项");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_PacsPathologyItem");
        }
    }
}
