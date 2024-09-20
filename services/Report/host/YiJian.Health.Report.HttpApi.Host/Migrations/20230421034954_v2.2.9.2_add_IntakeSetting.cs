using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Health.Report.Migrations
{
    public partial class v2292_add_IntakeSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "RpReportData",
                comment: "报表打印数据");

            migrationBuilder.AlterTable(
                name: "RpCharacteristic",
                comment: "病人特征记录");

            migrationBuilder.AlterColumn<Guid>(
                name: "NursingRecordId",
                table: "RpPupil",
                type: "uniqueidentifier",
                nullable: false,
                comment: "护理记录Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CardNo",
                table: "RpNursingDocument",
                type: "nvarchar(max)",
                nullable: true,
                comment: "就诊卡号",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "NursingRecordId",
                table: "RpIntake",
                type: "uniqueidentifier",
                nullable: false,
                comment: "护理记录Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Endtime",
                table: "RpCriticalIllness",
                type: "datetime2",
                nullable: true,
                comment: "危重结束时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "NursingRecordId",
                table: "RpCharacteristic",
                type: "uniqueidentifier",
                nullable: false,
                comment: "护理记录Id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "JsonData",
                table: "RpCharacteristic",
                type: "nvarchar(max)",
                nullable: true,
                comment: "json结构的数据",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HeaderId",
                table: "RpCharacteristic",
                type: "uniqueidentifier",
                nullable: true,
                comment: "表头内容",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RpIntakeSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IntakeType = table.Column<int>(type: "int", nullable: false, comment: "入量出量类型（0=入量，1=出量）"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "出入量的代码"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "出入量的名称"),
                    InputMode = table.Column<int>(type: "int", maxLength: 50, nullable: false, comment: "输入类型"),
                    Way = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "方式"),
                    Color = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "颜色"),
                    Unit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "单位"),
                    Traits = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "性状"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpIntakeSetting", x => x.Id);
                },
                comment: "入量出量配置");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpIntakeSetting");

            migrationBuilder.AlterTable(
                name: "RpReportData",
                oldComment: "报表打印数据");

            migrationBuilder.AlterTable(
                name: "RpCharacteristic",
                oldComment: "病人特征记录");

            migrationBuilder.AlterColumn<Guid>(
                name: "NursingRecordId",
                table: "RpPupil",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "护理记录Id");

            migrationBuilder.AlterColumn<string>(
                name: "CardNo",
                table: "RpNursingDocument",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "就诊卡号");

            migrationBuilder.AlterColumn<Guid>(
                name: "NursingRecordId",
                table: "RpIntake",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "护理记录Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Endtime",
                table: "RpCriticalIllness",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "危重结束时间");

            migrationBuilder.AlterColumn<Guid>(
                name: "NursingRecordId",
                table: "RpCharacteristic",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "护理记录Id");

            migrationBuilder.AlterColumn<string>(
                name: "JsonData",
                table: "RpCharacteristic",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "json结构的数据");

            migrationBuilder.AlterColumn<Guid>(
                name: "HeaderId",
                table: "RpCharacteristic",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "表头内容");
        }
    }
}
