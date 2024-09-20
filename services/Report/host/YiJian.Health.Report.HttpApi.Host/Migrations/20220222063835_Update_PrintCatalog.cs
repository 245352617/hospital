using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class Update_PrintCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "RpPrintCatalog");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RpPrintCatalog");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "RpPrintCatalog");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "RpPrintCatalog");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RpPrintCatalog");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "RpPrintCatalog");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RpPrintCatalog");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "RpPrintCatalog");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "RpPrintCatalog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "RpPrintCatalog",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RpPrintCatalog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "RpPrintCatalog",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "RpPrintCatalog",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "RpPrintCatalog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "RpPrintCatalog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RpPrintCatalog",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "RpPrintCatalog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "RpPrintCatalog",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
