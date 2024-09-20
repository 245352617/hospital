using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class recipestarttime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "NursingPrescribe");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "NursingPrescribe");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "NursingRecipe",
                type: "datetime2",
                nullable: true,
                comment: "结束时间");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "NursingRecipe",
                type: "datetime2",
                nullable: true,
                comment: "开始时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "NursingRecipe");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "NursingRecipe");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "NursingPrescribe",
                type: "datetime2",
                nullable: true,
                comment: "结束时间");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "NursingPrescribe",
                type: "datetime2",
                nullable: true,
                comment: "开始时间");
        }
    }
}
