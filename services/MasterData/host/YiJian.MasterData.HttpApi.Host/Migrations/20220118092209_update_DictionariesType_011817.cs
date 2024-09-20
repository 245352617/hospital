using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_DictionariesType_011817 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Dict_DictionariesType",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Dict_DictionariesType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Dict_DictionariesType",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Dict_DictionariesType",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Dict_DictionariesType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Dict_DictionariesType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Dict_DictionariesType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Dict_DictionariesType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Dict_DictionariesType",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Dict_DictionariesType",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "状态");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Dict_DictionariesType");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Dict_DictionariesType");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Dict_DictionariesType");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Dict_DictionariesType");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Dict_DictionariesType");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Dict_DictionariesType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Dict_DictionariesType");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Dict_DictionariesType");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Dict_DictionariesType");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Dict_DictionariesType");
        }
    }
}
