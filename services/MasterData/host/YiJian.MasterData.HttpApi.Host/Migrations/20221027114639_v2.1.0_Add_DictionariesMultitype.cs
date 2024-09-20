using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210_Add_DictionariesMultitype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_DictionariesMultitype",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(50)", nullable: true, comment: "字典分组编码"),
                    GroupName = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "字典分组名称"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "字典编码"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "字典名称"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "配置类型"),
                    Value = table.Column<string>(type: "nvarchar(500)", nullable: false, comment: "配置值"),
                    DataFrom = table.Column<int>(type: "int", nullable: false, comment: "数据来源，0：急诊添加，1：预检分诊同步"),
                    Status = table.Column<bool>(type: "bit", nullable: false, comment: "状态"),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "备注"),
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
                    table.PrimaryKey("PK_Dict_DictionariesMultitype", x => x.Id);
                },
                comment: "字典多类型表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_DictionariesMultitype");
        }
    }
}
