using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Update_CallInfo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeType",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "ChargeTypeName",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "ConsultingRoomCode",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "ConsultingRoomName",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "ContactsPerson",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "ContactsPhone",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "GreenChannelCode",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "IdentityNo",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "IdentityType",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "IdentityTypeName",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "IsGreenChannel",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "Nation",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "Py",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "SexName",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "ToHospitalWay",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "TriageDirection",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "TriageId",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "VisitFinishTime",
                table: "CallCallInfo");

            migrationBuilder.DropColumn(
                name: "VisitStartTime",
                table: "CallCallInfo");

            migrationBuilder.AddColumn<string>(
                name: "DoctorCode",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "急诊医生Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorCode",
                table: "CallCallInfo");

            migrationBuilder.AddColumn<string>(
                name: "ChargeType",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "费别");

            migrationBuilder.AddColumn<string>(
                name: "ChargeTypeName",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "费别");

            migrationBuilder.AddColumn<string>(
                name: "ConsultingRoomCode",
                table: "CallCallInfo",
                type: "nvarchar(max)",
                nullable: true,
                comment: "就诊诊室代码");

            migrationBuilder.AddColumn<string>(
                name: "ConsultingRoomName",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "就诊诊室名称");

            migrationBuilder.AddColumn<string>(
                name: "ContactsPerson",
                table: "CallCallInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "紧急联系人");

            migrationBuilder.AddColumn<string>(
                name: "ContactsPhone",
                table: "CallCallInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "联系电话");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "CallCallInfo",
                type: "datetime2",
                nullable: true,
                comment: "患者出生日期");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "急诊医生id");

            migrationBuilder.AddColumn<string>(
                name: "GreenChannelCode",
                table: "CallCallInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "绿色通道代码");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "CallCallInfo",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "患者住址");

            migrationBuilder.AddColumn<string>(
                name: "IdentityNo",
                table: "CallCallInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "身份证号");

            migrationBuilder.AddColumn<string>(
                name: "IdentityType",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "患者身份");

            migrationBuilder.AddColumn<string>(
                name: "IdentityTypeName",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "患者身份");

            migrationBuilder.AddColumn<bool>(
                name: "IsGreenChannel",
                table: "CallCallInfo",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否绿色通道");

            migrationBuilder.AddColumn<string>(
                name: "Nation",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "民族");

            migrationBuilder.AddColumn<string>(
                name: "Py",
                table: "CallCallInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "患者姓名拼音首字母");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "CallCallInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "患者性别");

            migrationBuilder.AddColumn<string>(
                name: "SexName",
                table: "CallCallInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "患者性别");

            migrationBuilder.AddColumn<string>(
                name: "ToHospitalWay",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "来院方式");

            migrationBuilder.AddColumn<string>(
                name: "TriageDirection",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "分诊去向编码");

            migrationBuilder.AddColumn<Guid>(
                name: "TriageId",
                table: "CallCallInfo",
                type: "uniqueidentifier",
                nullable: true,
                comment: "患者分诊 ID");

            migrationBuilder.AddColumn<DateTime>(
                name: "VisitFinishTime",
                table: "CallCallInfo",
                type: "datetime2",
                nullable: true,
                comment: "结束就诊时间");

            migrationBuilder.AddColumn<DateTime>(
                name: "VisitStartTime",
                table: "CallCallInfo",
                type: "datetime2",
                nullable: true,
                comment: "开始就诊时间");
        }
    }
}
