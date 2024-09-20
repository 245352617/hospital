using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class Add_PageSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RpPageSize",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "编码"),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "高"),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "宽")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpPageSize", x => x.Id);
                },
                comment: "纸张大小");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpPageSize");
        }
    }
}
