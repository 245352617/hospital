using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210addtablepharmacy2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_Pharmacy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PharmacyCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "药房编号"),
                    PharmacyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "药房名称"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "默认药房，1=是默认药房")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Pharmacy", x => x.Id);
                },
                comment: "药房配置");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_Pharmacy");
        }
    }
}
