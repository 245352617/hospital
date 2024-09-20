using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CatalogueTitle",
                table: "EmrTemplateCatalogue",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "最初引入病历库的名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "最初引入病历库的名称");

            migrationBuilder.CreateTable(
                name: "EmrEmrProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "属性值"),
                    Label = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "属性标签"),
                    Lv = table.Column<int>(type: "int", nullable: false, comment: "属性层级")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrEmrProperty", x => x.Id);
                },
                comment: "电子病历属性");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrEmrProperty");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogueTitle",
                table: "EmrTemplateCatalogue",
                type: "nvarchar(max)",
                nullable: true,
                comment: "最初引入病历库的名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "最初引入病历库的名称");
        }
    }
}
