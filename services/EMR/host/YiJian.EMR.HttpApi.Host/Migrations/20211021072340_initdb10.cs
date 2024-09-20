using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrEmrProperty");

            migrationBuilder.AddColumn<string>(
                name: "CategoryLv1",
                table: "EmrPatientEmr",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                comment: "一级分类");

            migrationBuilder.AddColumn<string>(
                name: "CategoryLv2",
                table: "EmrPatientEmr",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                comment: "二级分类");

            migrationBuilder.CreateTable(
                name: "EmrCategoryProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "属性值"),
                    Label = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "属性标签"),
                    Lv = table.Column<int>(type: "int", nullable: false, comment: "属性层级"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "级联父节点Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrCategoryProperty", x => x.Id);
                },
                comment: "电子病历属性");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrCategoryProperty");

            migrationBuilder.DropColumn(
                name: "CategoryLv1",
                table: "EmrPatientEmr");

            migrationBuilder.DropColumn(
                name: "CategoryLv2",
                table: "EmrPatientEmr");

            migrationBuilder.CreateTable(
                name: "EmrEmrProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "属性标签"),
                    Lv = table.Column<int>(type: "int", nullable: false, comment: "属性层级"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "级联父节点Id"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "属性值")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrEmrProperty", x => x.Id);
                },
                comment: "电子病历属性");
        }
    }
}
