using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AdmissionTime",
                table: "EmrPatientEmr",
                type: "datetime2",
                nullable: true,
                comment: "入院时间");

            migrationBuilder.AddColumn<DateTime>(
                name: "DischargeTime",
                table: "EmrPatientEmr",
                type: "datetime2",
                nullable: true,
                comment: "出院时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionTime",
                table: "EmrPatientEmr");

            migrationBuilder.DropColumn(
                name: "DischargeTime",
                table: "EmrPatientEmr");
        }
    }
}
