using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_VitalSignExpression : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_VitalSignExpressions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "评分项"),
                    StLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅰ级评分表达式"),
                    NdLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅱ级评分表达式"),
                    RdLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅲ级评分表达式"),
                    ThALevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅳa级评分表达式"),
                    ThBLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Ⅳb级评分表达式"),
                    DefaultStLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "默认Ⅰ级评分表达式"),
                    DefaultNdLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "默认Ⅱ级评分表达式"),
                    DefaultRdLevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "默认Ⅲ级评分表达式"),
                    DefaultThALevelExpression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "默认Ⅳa级评分表达式"),
                    DefaultThBLevelExpression = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, comment: "默认Ⅳb级评分表达式")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_VitalSignExpressions", x => x.Id);
                },
                comment: "评分项");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_VitalSignExpressions_ItemName",
                table: "Dict_VitalSignExpressions",
                column: "ItemName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_VitalSignExpressions");
        }
    }
}
