using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrAppSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "配置名称"),
                    Data = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "配置值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrAppSetting", x => x.Id);
                },
                comment: "应用配置");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrAppSetting");
        }
    }
}
