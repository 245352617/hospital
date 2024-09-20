using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_Entrust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_Entrust",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "嘱托编码"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "嘱托名称"),
                    PrescribeTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型编码"),
                    PrescribeTypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医嘱类型：临嘱、长嘱、出院带药等"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次编码"),
                    FrequencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "频次名称"),
                    RecieveQty = table.Column<int>(type: "int", nullable: false, comment: "领量(数量)"),
                    RecieveUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "领量单位"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "五笔码"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Entrust", x => x.Id);
                },
                comment: "嘱托配置");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_Entrust");
        }
    }
}
