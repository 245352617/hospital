using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class LevelTriageRelationDirection20210623 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherDirectionId",
                table: "Dict_LevelTriageRelationDirection");

            migrationBuilder.DropColumn(
                name: "TriageDirectionId",
                table: "Dict_LevelTriageRelationDirection");

            migrationBuilder.AddColumn<string>(
                name: "OtherDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                type: "varchar(50)",
                nullable: true,
                comment: "其他去向code");

            migrationBuilder.AddColumn<string>(
                name: "TriageDirectionCode",
                table: "Dict_LevelTriageRelationDirection",
                type: "varchar(50)",
                nullable: true,
                comment: "分诊去向code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherDirectionCode",
                table: "Dict_LevelTriageRelationDirection");

            migrationBuilder.DropColumn(
                name: "TriageDirectionCode",
                table: "Dict_LevelTriageRelationDirection");

            migrationBuilder.AddColumn<Guid>(
                name: "OtherDirectionId",
                table: "Dict_LevelTriageRelationDirection",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "其他去向ID");

            migrationBuilder.AddColumn<Guid>(
                name: "TriageDirectionId",
                table: "Dict_LevelTriageRelationDirection",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "分诊去向Id");
        }
    }
}
