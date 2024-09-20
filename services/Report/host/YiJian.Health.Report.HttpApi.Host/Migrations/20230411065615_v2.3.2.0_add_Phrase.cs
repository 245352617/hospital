using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Health.Report.Migrations
{
    public partial class v2320_add_Phrase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RpPhraseCatalogue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "目录标题"),
                    TemplateType = table.Column<int>(type: "int", nullable: false, comment: "模板类型，0=通用(全院)，1=科室，2=个人"),
                    Belonger = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "归属人 如果 TemplateType=2 归属者为医生Id doctorId, 如果 TemplateType=1 归属者为科室id deptid , 如果 TemplateType=0 归属者为hospital"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号码"),
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
                    table.PrimaryKey("PK_RpPhraseCatalogue", x => x.Id);
                },
                comment: "常用语目录");

            migrationBuilder.CreateTable(
                name: "RpPhrase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "标题"),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "内容"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号码"),
                    CatalogueId = table.Column<int>(type: "int", nullable: false, comment: "目录Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpPhrase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RpPhrase_RpPhraseCatalogue_CatalogueId",
                        column: x => x.CatalogueId,
                        principalTable: "RpPhraseCatalogue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "病历常用语");

            migrationBuilder.CreateIndex(
                name: "IX_RpPhrase_CatalogueId",
                table: "RpPhrase",
                column: "CatalogueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpPhrase");

            migrationBuilder.DropTable(
                name: "RpPhraseCatalogue");
        }
    }
}
