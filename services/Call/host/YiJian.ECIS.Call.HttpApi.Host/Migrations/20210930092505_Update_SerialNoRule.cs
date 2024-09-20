using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Update_SerialNoRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SerialLength",
                table: "CallSerialNoRule",
                type: "int",
                nullable: false,
                defaultValue: 3,
                comment: "流水号位数",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1,
                oldComment: "流水号位数");

            migrationBuilder.AddColumn<int>(
                name: "CurrentNo",
                table: "CallSerialNoRule",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "当前流水号");

            migrationBuilder.AddColumn<DateTime>(
                name: "SerialDateTime",
                table: "CallSerialNoRule",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "CallBaseConfig",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "CallBaseConfig",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "CallBaseConfig",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentCallMode",
                table: "CallBaseConfig",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "当前叫号模式");

            migrationBuilder.AddColumn<int>(
                name: "CurrentUpdateNoHour",
                table: "CallBaseConfig",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "当前的 每日更新号码时间（小时）（0-23）");

            migrationBuilder.AddColumn<int>(
                name: "CurrentUpdateNoMinute",
                table: "CallBaseConfig",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "每日更新号码时间（分钟）");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "CallBaseConfig",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "CallBaseConfig",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "CallBaseConfig",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentNo",
                table: "CallSerialNoRule");

            migrationBuilder.DropColumn(
                name: "SerialDateTime",
                table: "CallSerialNoRule");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "CallBaseConfig");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "CallBaseConfig");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "CallBaseConfig");

            migrationBuilder.DropColumn(
                name: "CurrentCallMode",
                table: "CallBaseConfig");

            migrationBuilder.DropColumn(
                name: "CurrentUpdateNoHour",
                table: "CallBaseConfig");

            migrationBuilder.DropColumn(
                name: "CurrentUpdateNoMinute",
                table: "CallBaseConfig");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "CallBaseConfig");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "CallBaseConfig");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "CallBaseConfig");

            migrationBuilder.AlterColumn<int>(
                name: "SerialLength",
                table: "CallSerialNoRule",
                type: "int",
                nullable: false,
                defaultValue: 1,
                comment: "流水号位数",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 3,
                oldComment: "流水号位数");
        }
    }
}
