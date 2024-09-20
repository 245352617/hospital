using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "科室名称"),
                    DeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "科室名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrDepartment", x => x.Id);
                },
                comment: "科室历史记录");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrDepartment");
        }
    }
}
