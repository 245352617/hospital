using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class v210addnewtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_Separation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "分方单名称"),
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
                    table.PrimaryKey("PK_Dict_Separation", x => x.Id);
                },
                comment: "分方配置");

            migrationBuilder.CreateTable(
                name: "Dict_Usage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法编码"),
                    UsageName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "用法名称"),
                    SeparationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "分方配置Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Usage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dict_Usage_Dict_Separation_SeparationId",
                        column: x => x.SeparationId,
                        principalTable: "Dict_Separation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dict_Usage_SeparationId",
                table: "Dict_Usage",
                column: "SeparationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_Usage");

            migrationBuilder.DropTable(
                name: "Dict_Separation");
        }
    }
}
