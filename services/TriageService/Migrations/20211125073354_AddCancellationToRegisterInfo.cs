using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddCancellationToRegisterInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationTime",
                table: "Triage_RegisterInfo",
                nullable: true,
                comment: "取消挂号时间");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Triage_RegisterInfo",
                nullable: false,
                defaultValue: false,
                comment: "是否已取消挂号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationTime",
                table: "Triage_RegisterInfo");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Triage_RegisterInfo");
        }
    }
}
