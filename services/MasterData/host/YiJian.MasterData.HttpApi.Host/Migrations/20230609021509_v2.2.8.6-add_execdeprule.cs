using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class v2286add_execdeprule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_ExecuteDepRuleDic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleId = table.Column<int>(type: "int", nullable: false, comment: "规则id"),
                    RuleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "规则名称"),
                    ExeDepCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "执行科室代码"),
                    ExeDepName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "执行科室名称"),
                    SpellCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "拼英编号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ExecuteDepRuleDic", x => x.Id);
                },
                comment: "执行科室规则字典");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ExecuteDepRuleDic");
        }
    }
}
