using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class Update_ParaModuleParaItemDict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidState",
                table: "Duct_ParaModule");

            migrationBuilder.DropColumn(
                name: "ValidState",
                table: "Duct_ParaItem");

            migrationBuilder.DropColumn(
                name: "ValidState",
                table: "Duct_Dict");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Duct_ParaModule",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Duct_ParaModule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Duct_ParaModule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Duct_ParaModule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Duct_ParaModule",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Duct_ParaModule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Duct_ParaModule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Duct_ParaItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Duct_ParaItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Duct_ParaItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Duct_ParaItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Duct_ParaItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Duct_ParaItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Duct_ParaItem",
                type: "uniqueidentifier",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Duct_ParaModule");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Duct_ParaModule");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Duct_ParaModule");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Duct_ParaModule");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Duct_ParaModule");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Duct_ParaModule");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Duct_ParaModule");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Duct_ParaItem");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Duct_ParaItem");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Duct_ParaItem");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Duct_ParaItem");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Duct_ParaItem");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Duct_ParaItem");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Duct_ParaItem");

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

            migrationBuilder.AddColumn<int>(
                name: "ValidState",
                table: "Duct_ParaModule",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "是否有效(1-有效，0-无效)");

            migrationBuilder.AddColumn<int>(
                name: "ValidState",
                table: "Duct_ParaItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "有效状态");

            migrationBuilder.AddColumn<int>(
                name: "ValidState",
                table: "Duct_Dict",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "有效状态（1-有效，0-无效）");
        }
    }
}
