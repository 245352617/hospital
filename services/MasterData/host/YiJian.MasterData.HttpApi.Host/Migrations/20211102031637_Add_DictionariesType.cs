using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_DictionariesType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_DictionariesTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DictionariesTypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "字典类型编码"),
                    DictionariesTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "字典类型名称"),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "备注")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_DictionariesTypes", x => x.Id);
                },
                comment: "字典类型编码");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_DictionariesTypes_DictionariesTypeCode",
                table: "Dict_DictionariesTypes",
                column: "DictionariesTypeCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_DictionariesTypes");
        }
    }
}
