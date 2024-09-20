using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_Treat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_Treats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "名称"),
                    PyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "拼音码"),
                    WbCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "五笔"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "单价"),
                    OtherPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "其它价格"),
                    CategoryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "诊疗处置类别代码"),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "诊疗处置类别"),
                    Specification = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "规格"),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "单位"),
                    FrequencyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "默认频次代码"),
                    ExecDeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行科室代码"),
                    ExecDept = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "执行科室"),
                    FeeTypeMain = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "收费大类代码"),
                    FeeTypeSub = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "收费小类代码"),
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
                    table.PrimaryKey("PK_Dict_Treats", x => x.Id);
                },
                comment: "诊疗项目字典");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_Treats_Code",
                table: "Dict_Treats",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_Treats");
        }
    }
}
