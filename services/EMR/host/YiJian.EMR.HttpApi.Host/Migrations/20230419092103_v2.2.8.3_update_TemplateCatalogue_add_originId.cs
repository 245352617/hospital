using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v2283_update_TemplateCatalogue_add_originId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CatalogueId",
                table: "EmrTemplateCatalogue",
                type: "uniqueidentifier",
                nullable: true,
                comment: "引用的模板Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "最初引入病历库的Id");

            migrationBuilder.AddColumn<Guid>(
                name: "OriginId",
                table: "EmrTemplateCatalogue",
                type: "uniqueidentifier",
                nullable: true,
                comment: "最初引入病历库的Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginId",
                table: "EmrTemplateCatalogue");

            migrationBuilder.AlterColumn<Guid>(
                name: "CatalogueId",
                table: "EmrTemplateCatalogue",
                type: "uniqueidentifier",
                nullable: true,
                comment: "最初引入病历库的Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "引用的模板Id");
        }
    }
}
