using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class FastTrackRegisterInfo2021072617 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoliceId",
                table: "Triage_FastTrackRegisterInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "PoliceStationId",
                table: "Triage_FastTrackRegisterInfo",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "所属派出所Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoliceStationId",
                table: "Triage_FastTrackRegisterInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "PoliceId",
                table: "Triage_FastTrackRegisterInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "所属派出所Id");
        }
    }
}
