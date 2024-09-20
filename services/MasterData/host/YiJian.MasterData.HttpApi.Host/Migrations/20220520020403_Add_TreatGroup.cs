using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_TreatGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_TreatGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatalogCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "目录编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "目录名称"),
                    DictionaryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "字典编码"),
                    DictionaryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "字典名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_TreatGroup", x => x.Id);
                },
                comment: "诊疗分组");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_TreatGroup");
        }
    }
}
