using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.EMR.Migrations
{
    public partial class patientEmroriginId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "OriginalId",
                table: "EmrPatientEmr",
                type: "uniqueidentifier",
                nullable: false,
                comment: "原电子病历模板Id（上一级）",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OriginId",
                table: "EmrPatientEmr",
                type: "uniqueidentifier",
                nullable: true,
                comment: "最初引入病历库的Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginId",
                table: "EmrPatientEmr");

            migrationBuilder.AlterColumn<Guid>(
                name: "OriginalId",
                table: "EmrPatientEmr",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "原电子病历模板Id（上一级）");
        }
    }
}
