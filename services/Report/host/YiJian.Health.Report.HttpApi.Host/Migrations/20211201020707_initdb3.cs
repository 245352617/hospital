using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class initdb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RpDynamicField",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "RpDynamicField",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "RpDynamicField",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "RpDynamicField",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RpDynamicField",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "RpDynamicField",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "RpDynamicField",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RpDynamicField");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "RpDynamicField");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "RpDynamicField");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RpDynamicField");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RpDynamicField");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "RpDynamicField");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "RpDynamicField");
        }
    }
}
