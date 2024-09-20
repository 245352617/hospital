using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class Update_IllnessObserveMain_1014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "NursingIllnessObserveOutput",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "主键id");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "NursingIllnessObserveOutput",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "NursingIllnessObserveOutput",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "NursingIllnessObserveOutput",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "NursingIllnessObserveOutput",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "NursingIllnessObserveOutput",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "NursingIllnessObserveOutput",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "NursingIllnessObserveOutput",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "NursingIllnessObserveOutput",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "NursingIllnessObserveOutput",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "NursingIllnessObserveOther",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "主键id");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "NursingIllnessObserveOther",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "NursingIllnessObserveOther",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "NursingIllnessObserveOther",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "NursingIllnessObserveOther",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "NursingIllnessObserveOther",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "NursingIllnessObserveOther",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "NursingIllnessObserveOther",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "NursingIllnessObserveOther",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "NursingIllnessObserveOther",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "NursingIllnessObserveInput",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "主键id");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "NursingIllnessObserveInput",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "NursingIllnessObserveInput",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "NursingIllnessObserveInput",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "NursingIllnessObserveInput",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "NursingIllnessObserveInput",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "NursingIllnessObserveInput",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "NursingIllnessObserveInput",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "NursingIllnessObserveInput",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "NursingIllnessObserveInput",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "NursingIllnessObserveOutput");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "NursingIllnessObserveOther");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "NursingIllnessObserveInput");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "NursingIllnessObserveInput");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "NursingIllnessObserveInput");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "NursingIllnessObserveInput");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "NursingIllnessObserveInput");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "NursingIllnessObserveInput");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "NursingIllnessObserveInput");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "NursingIllnessObserveInput");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "NursingIllnessObserveInput");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "NursingIllnessObserveOutput",
                type: "uniqueidentifier",
                nullable: false,
                comment: "主键id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "NursingIllnessObserveOther",
                type: "uniqueidentifier",
                nullable: false,
                comment: "主键id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "NursingIllnessObserveInput",
                type: "uniqueidentifier",
                nullable: false,
                comment: "主键id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
