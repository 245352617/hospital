using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "EmrTemplateCatalogue",
                type: "uniqueidentifier",
                nullable: true,
                comment: "父级Id，根级=0",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "父级Id，根级=0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "EmrTemplateCatalogue",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "父级Id，根级=0",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "父级Id，根级=0");
        }
    }
}
