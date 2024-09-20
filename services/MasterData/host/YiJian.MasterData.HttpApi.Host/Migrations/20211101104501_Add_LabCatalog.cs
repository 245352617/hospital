using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_LabCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Name",
                table: "Dict_Medicines",
                newName: "IX_Medicine_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Code",
                table: "Dict_Medicines",
                newName: "IX_Medicine_Code");

            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "拼音码");

            migrationBuilder.CreateTable(
                name: "Dict_LabCatalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "分类编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "分类编码"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    Dept = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室"),
                    IndexNo = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "五笔"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置描述"),
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
                    table.PrimaryKey("PK_Dict_LabCatalogs", x => x.Id);
                },
                comment: "检验目录");

            migrationBuilder.CreateTable(
                name: "Dict_LabProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    CatalogCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "检验分类编码"),
                    Catalog = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "检验分类"),
                    SpecimenCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "标本编码"),
                    Specimen = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "标本"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编码"),
                    Dept = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "科室"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "位置编码"),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "位置"),
                    IndexNo = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "排序号"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "五笔"),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "单位"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", maxLength: 50, nullable: false, comment: "价格"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "价格"),
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
                    table.PrimaryKey("PK_Dict_LabProjects", x => x.Id);
                },
                comment: "检验项目");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabCatalogs_Code",
                table: "Dict_LabCatalogs",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabProjects_Code",
                table: "Dict_LabProjects",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_LabCatalogs");

            migrationBuilder.DropTable(
                name: "Dict_LabProjects");

            migrationBuilder.RenameIndex(
                name: "IX_Medicine_Name",
                table: "Dict_Medicines",
                newName: "IX_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Medicine_Code",
                table: "Dict_Medicines",
                newName: "IX_Code");

            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "拼音码");
        }
    }
}
