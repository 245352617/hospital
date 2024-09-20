using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Init_DoctorRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallDoctorRegular",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医生id"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生名称"),
                    DoctorDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医生所属科室id"),
                    DoctorDepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生所属科室名称"),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "对应急诊科室id"),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "是否使用"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallDoctorRegular", x => x.Id);
                },
                comment: "医生变动表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallDoctorRegular");
        }
    }
}
