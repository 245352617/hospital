using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class addrecipehistorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NursingRecipeHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱ID"),
                    Operation = table.Column<int>(type: "int", nullable: false, comment: "操作类型"),
                    OperatorCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "操作人编码"),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "操作人名称"),
                    OperaTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "操作时间"),
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
                    table.PrimaryKey("PK_NursingRecipeHistory", x => x.Id);
                },
                comment: "医嘱操作历史");

            migrationBuilder.CreateIndex(
                name: "IX_NursingRecipeHistory_RecipeId",
                table: "NursingRecipeHistory",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NursingRecipeHistory");
        }
    }
}
