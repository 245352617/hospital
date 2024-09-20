using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_Exam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_ExamCatalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    ExamPart = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位"),
                    IndexNo = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "五笔"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    RoomCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行机房编码"),
                    Room = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行机房"),
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
                    table.PrimaryKey("PK_Dict_ExamCatalogs", x => x.Id);
                },
                comment: "检查目录");

            migrationBuilder.CreateTable(
                name: "Dict_ExamNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "注意事项编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "注意事项名称"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    Dept = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室"),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单"),
                    DescTemplate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "检查申请描述模板"),
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
                    table.PrimaryKey("PK_Dict_ExamNotes", x => x.Id);
                },
                comment: "检查申请注意事项");

            migrationBuilder.CreateTable(
                name: "Dict_ExamParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检查部位编码"),
                    PartName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "检查部位名称"),
                    IndexNo = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
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
                    table.PrimaryKey("PK_Dict_ExamParts", x => x.Id);
                },
                comment: "检查部位");

            migrationBuilder.CreateTable(
                name: "Dict_ExamProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "分类编码"),
                    Catalog = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "分类名称"),
                    IndexNo = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "五笔"),
                    ExamPart = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "检查部位"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "单位"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "价格"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    Dept = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室名称"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    RoomCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行机房编码"),
                    Room = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行机房描述"),
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
                    table.PrimaryKey("PK_Dict_ExamProjects", x => x.Id);
                },
                comment: "检查申请项目");

            migrationBuilder.CreateTable(
                name: "Dict_ExamTargets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "项目编码"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "单位"),
                    Amount = table.Column<int>(type: "int", nullable: false, comment: "数量"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "价格"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "其它价格"),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "规格"),
                    IndexNo = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "五笔"),
                    InsureType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保类型"),
                    SpecialFlag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "特殊标识"),
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
                    table.PrimaryKey("PK_Dict_ExamTargets", x => x.Id);
                },
                comment: "检查明细项");

            migrationBuilder.CreateTable(
                name: "Dict_LabSpecimens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "标本编码"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "标本名称"),
                    IndexNo = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "五笔"),
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
                    table.PrimaryKey("PK_Dict_LabSpecimens", x => x.Id);
                },
                comment: "检验标本");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamCatalogs_Code",
                table: "Dict_ExamCatalogs",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCatalog_Code",
                table: "Dict_ExamCatalogs",
                columns: new[] { "Code", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamNotes_Code",
                table: "Dict_ExamNotes",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamParts_PartCode",
                table: "Dict_ExamParts",
                column: "PartCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamProjects_Code",
                table: "Dict_ExamProjects",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamTargets_Code",
                table: "Dict_ExamTargets",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabSpecimens_Code",
                table: "Dict_LabSpecimens",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ExamCatalogs");

            migrationBuilder.DropTable(
                name: "Dict_ExamNotes");

            migrationBuilder.DropTable(
                name: "Dict_ExamParts");

            migrationBuilder.DropTable(
                name: "Dict_ExamProjects");

            migrationBuilder.DropTable(
                name: "Dict_ExamTargets");

            migrationBuilder.DropTable(
                name: "Dict_LabSpecimens");
        }
    }
}
