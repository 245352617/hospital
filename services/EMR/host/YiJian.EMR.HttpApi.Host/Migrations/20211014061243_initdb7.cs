using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CatalogueId",
                table: "EmrTemplateCatalogue",
                type: "uniqueidentifier",
                nullable: true,
                comment: "最初引入病历库的Id");

            migrationBuilder.AddColumn<string>(
                name: "CatalogueTitle",
                table: "EmrTemplateCatalogue",
                type: "nvarchar(max)",
                nullable: true,
                comment: "最初引入病历库的名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatalogueId",
                table: "EmrTemplateCatalogue");

            migrationBuilder.DropColumn(
                name: "CatalogueTitle",
                table: "EmrTemplateCatalogue");
        }
    }
}
