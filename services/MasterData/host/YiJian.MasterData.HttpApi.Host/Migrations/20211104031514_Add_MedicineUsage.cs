using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_MedicineUsage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_MedicineUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    Catalog = table.Column<int>(type: "int", nullable: false, comment: "分类  1：输液  2：注射  3：治疗  4：服药  10其他"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "五笔码"),
                    IndexNo = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "排序号"),
                    TreatCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "诊疗项目 描述：一个或多个项目，多个以,隔开"),
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
                    table.PrimaryKey("PK_Dict_MedicineUsages", x => x.Id);
                },
                comment: "药品用法字典");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_MedicineUsages_Code",
                table: "Dict_MedicineUsages",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_MedicineUsages");
        }
    }
}
