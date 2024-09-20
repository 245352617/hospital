using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Handover.Migrations
{
    public partial class Add_ShiftHandoverSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Handover_ShiftHandoverSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "类别编码"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "类别名称"),
                    ShiftName = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "班次名称"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "开始时间"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "结束时间"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    MatchingColor = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "匹配颜色"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "类型，医生1，护士0"),
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
                    table.PrimaryKey("PK_Handover_ShiftHandoverSetting", x => x.Id);
                },
                comment: "交接班配置表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Handover_ShiftHandoverSetting");
        }
    }
}
