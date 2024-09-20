using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class FastTrackRegisterInfo20210726 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<Guid>(
                name: "PoliceId",
                table: "Triage_FastTrackRegisterInfo",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "所属派出所Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoliceId",
                table: "Triage_FastTrackRegisterInfo");

            migrationBuilder.CreateIndex(
                name: "IX_TriageConfig_TriageConfigCode",
                table: "TriageConfig",
                column: "TriageConfigCode",
                unique: true,
                filter: "[TriageConfigCode] IS NOT NULL");
        }
    }
}
