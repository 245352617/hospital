using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamCatalog_1216 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ExamCatalogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ExamProjects",
                table: "Dict_ExamProjects");

            migrationBuilder.RenameTable(
                name: "Dict_ExamProjects",
                newName: "Dict_ExamProject");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamProjects_ProjectCode",
                table: "Dict_ExamProject",
                newName: "IX_Dict_ExamProject_ProjectCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ExamProject",
                table: "Dict_ExamProject",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Dict_ExamCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检查编码"),
                    CatalogName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "检查名称"),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    ExamPartCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位编码"),
                    IndexNo = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "五笔"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    PositionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置名称"),
                    RoomCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行机房编码"),
                    RoomName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行机房"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ExamCatalog", x => x.Id);
                },
                comment: "检查目录");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamCatalog_CatalogCode",
                table: "Dict_ExamCatalog",
                column: "CatalogCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ExamCatalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ExamProject",
                table: "Dict_ExamProject");

            migrationBuilder.RenameTable(
                name: "Dict_ExamProject",
                newName: "Dict_ExamProjects");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ExamProject_ProjectCode",
                table: "Dict_ExamProjects",
                newName: "IX_Dict_ExamProjects_ProjectCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ExamProjects",
                table: "Dict_ExamProjects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Dict_ExamCatalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单"),
                    ExamPart = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IndexNo = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    Room = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行机房"),
                    RoomCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行机房编码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "五笔")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ExamCatalogs", x => x.Id);
                },
                comment: "检查目录");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamCatalogs_Code",
                table: "Dict_ExamCatalogs",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCatalog_Code",
                table: "Dict_ExamCatalogs",
                columns: new[] { "Code", "IsDeleted" });
        }
    }
}
