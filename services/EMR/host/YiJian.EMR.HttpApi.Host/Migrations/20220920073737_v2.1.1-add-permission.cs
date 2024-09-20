using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v211addpermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "模块"),
                    PermissionTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "权限"),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "描述"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrPermission", x => x.Id);
                },
                comment: "EMR权限管理模型");

            migrationBuilder.CreateTable(
                name: "EmrOperatingAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室编码"),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    DoctorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医生编码"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "医生"),
                    PermissionId = table.Column<int>(type: "int", nullable: false, comment: "权限Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrOperatingAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrOperatingAccount_EmrPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "EmrPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "操作账号信息");

            migrationBuilder.CreateIndex(
                name: "IX_EmrOperatingAccount_PermissionId",
                table: "EmrOperatingAccount",
                column: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrOperatingAccount");

            migrationBuilder.DropTable(
                name: "EmrPermission");
        }
    }
}
