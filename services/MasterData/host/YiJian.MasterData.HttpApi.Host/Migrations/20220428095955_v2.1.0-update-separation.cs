using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210updateseparation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Dict_Separation");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Dict_Separation");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Dict_Separation");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Dict_Separation");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Dict_Separation");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Dict_Separation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Dict_Separation");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Dict_Separation");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Dict_Separation");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Dict_Separation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "Dict_Medicine",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                comment: "剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "剂量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Dict_Separation");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Dict_Separation",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Dict_Separation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Dict_Separation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Dict_Separation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Dict_Separation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Dict_Separation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Dict_Separation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Dict_Separation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Dict_Separation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "Dict_Medicine",
                type: "decimal(18,2)",
                nullable: false,
                comment: "剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3,
                oldComment: "剂量");
        }
    }
}
