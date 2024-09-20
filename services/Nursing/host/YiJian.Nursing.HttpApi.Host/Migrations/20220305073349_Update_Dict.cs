using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class Update_Dict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Duct_Dict");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Duct_Dict");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Duct_Dict");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Duct_Dict");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Duct_Dict");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Duct_Dict");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Duct_Dict");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Duct_Dict",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Duct_Dict",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Duct_Dict",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Duct_Dict",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Duct_Dict",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Duct_Dict",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Duct_Dict",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
