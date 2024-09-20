using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class Add_IllnessObserveHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NursingIllnessObserveHeader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "项目编码"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "项目名称"),
                    Unit = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "单位"),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "最大值"),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "最小值"),
                    MaxEarlyWarning = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "最大值预警"),
                    MinEarlyWarning = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "最小值预警"),
                    ParentCode = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "父级编码"),
                    Grade = table.Column<int>(type: "int", nullable: false, comment: "等级，1:一级，2：二级"),
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
                    table.PrimaryKey("PK_NursingIllnessObserveHeader", x => x.Id);
                },
                comment: "病情观察头部");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingIllnessObserveHeader");
        }
    }
}
