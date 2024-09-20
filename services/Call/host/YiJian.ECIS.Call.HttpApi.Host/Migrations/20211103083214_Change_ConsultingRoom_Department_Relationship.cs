using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Change_ConsultingRoom_Department_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dept_ConsultingRoom",
                table: "CallConsultingRoom");

            migrationBuilder.DropIndex(
                name: "IX_CallConsultingRoom_DeptID",
                table: "CallConsultingRoom");

            migrationBuilder.DropColumn(
                name: "DeptID",
                table: "CallConsultingRoom");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "CallDoctorRegular",
                type: "nvarchar(max)",
                nullable: false,
                comment: "医生id",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "医生id");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "CallConsultingRoomRegular",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "CallConsultingRoom",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CallConsultingRoom_DepartmentId",
                table: "CallConsultingRoom",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CallConsultingRoom_CallDepartment_DepartmentId",
                table: "CallConsultingRoom",
                column: "DepartmentId",
                principalTable: "CallDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CallConsultingRoom_CallDepartment_DepartmentId",
                table: "CallConsultingRoom");

            migrationBuilder.DropIndex(
                name: "IX_CallConsultingRoom_DepartmentId",
                table: "CallConsultingRoom");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CallConsultingRoomRegular");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CallConsultingRoom");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "CallDoctorRegular",
                type: "uniqueidentifier",
                nullable: false,
                comment: "医生id",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "医生id");

            migrationBuilder.AddColumn<Guid>(
                name: "DeptID",
                table: "CallConsultingRoom",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "科室ID");

            migrationBuilder.CreateIndex(
                name: "IX_CallConsultingRoom_DeptID",
                table: "CallConsultingRoom",
                column: "DeptID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dept_ConsultingRoom",
                table: "CallConsultingRoom",
                column: "DeptID",
                principalTable: "CallDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
