using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace YiJian.Recipe.Migrations
{
    public partial class add_PacsPathologyItemNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RC_PacsPathologyItemNo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100000, 1"),
                    PacsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "检查项Id"),
                    SpecimenName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "标本名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_PacsPathologyItemNo", x => x.Id);
                },
                comment: "检查病理小项序号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_PacsPathologyItemNo");
        }
    }
}
