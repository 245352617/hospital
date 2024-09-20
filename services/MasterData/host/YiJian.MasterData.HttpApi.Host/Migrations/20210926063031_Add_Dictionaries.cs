using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_Dictionaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterData_Dictionaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DictionariesCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DictionariesName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DictionariesTypeCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DictionariesTypeName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterData_Dictionaries", x => x.Id);
                },
                comment: "平台字典表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterData_Dictionaries");
        }
    }
}
