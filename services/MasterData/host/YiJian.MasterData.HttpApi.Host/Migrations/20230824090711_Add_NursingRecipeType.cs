using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class Add_NursingRecipeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_NursingRecipeType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "类别名称"),
                    UsageCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "编码"),
                    UsageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_NursingRecipeType", x => x.Id);
                },
                comment: "护士医嘱类别");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_NursingRecipeType_Id",
                table: "Dict_NursingRecipeType",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_NursingRecipeType");
        }
    }
}
