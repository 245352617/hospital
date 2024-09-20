using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_MedicineFrequency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_Diagnoses");

            migrationBuilder.CreateTable(
                name: "Dict_MedicineFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "频次编码"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "频次名称"),
                    Times = table.Column<int>(type: "int", maxLength: 50, nullable: false, comment: "频次系数"),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "频次单位"),
                    ExecTimes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开"),
                    Weeks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "频次周明细"),
                    Catalog = table.Column<int>(type: "int", nullable: false, comment: "频次分类 0：临时 1：长期 2：通用"),
                    IndexNo = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "排序号"),
                    Remark = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "备注"),
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
                    table.PrimaryKey("PK_Dict_MedicineFrequencies", x => x.Id);
                },
                comment: "药品频次字典");

            migrationBuilder.CreateTable(
                name: "Sys_Sequences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    Value = table.Column<int>(type: "int", nullable: false, comment: "序列值"),
                    Format = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "格式"),
                    Length = table.Column<int>(type: "int", nullable: false, comment: "序列值长度"),
                    Date = table.Column<DateTime>(type: "date", nullable: false, comment: "日期"),
                    Memo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "备注"),
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
                    table.PrimaryKey("PK_Sys_Sequences", x => x.Id);
                },
                comment: "序列");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_MedicineFrequencies_Code",
                table: "Dict_MedicineFrequencies",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Sequences_Code",
                table: "Sys_Sequences",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_MedicineFrequencies");

            migrationBuilder.DropTable(
                name: "Sys_Sequences");

            migrationBuilder.CreateTable(
                name: "Dict_Diagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "代码"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IndexNo = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    Note = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    SpecialFalg = table.Column<int>(type: "int", nullable: false, comment: "毒麻药诊断标识： 1 毒药 2 麻药 3 精神"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "五笔")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Diagnoses", x => x.Id);
                },
                comment: "诊断字典");

            migrationBuilder.CreateIndex(
                name: "IX_Code",
                table: "Dict_Diagnoses",
                columns: new[] { "Code", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Dict_Diagnoses_Code",
                table: "Dict_Diagnoses",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "Dict_Diagnoses",
                columns: new[] { "Name", "IsDeleted" });
        }
    }
}
