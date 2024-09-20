using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Update_DoctorPatients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Handover_DoctorPatientStatistics");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Handover_DoctorPatientStatistics");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Handover_DoctorPatientStatistics");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Handover_DoctorPatients");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Handover_DoctorPatients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Handover_DoctorPatients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Handover_DoctorPatientStatistics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Handover_DoctorPatientStatistics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Handover_DoctorPatientStatistics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Handover_DoctorPatients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Handover_DoctorPatients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Handover_DoctorPatients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
