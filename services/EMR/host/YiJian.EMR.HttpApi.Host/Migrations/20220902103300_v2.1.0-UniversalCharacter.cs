using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v210UniversalCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmrUniversalCharacter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "分类"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrUniversalCharacter", x => x.Id);
                },
                comment: "通用字符");

            migrationBuilder.CreateTable(
                name: "EmrUniversalCharacterNode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Character = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "字符"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    UniversalCharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrUniversalCharacterNode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrUniversalCharacterNode_EmrUniversalCharacter_UniversalCharacterId",
                        column: x => x.UniversalCharacterId,
                        principalTable: "EmrUniversalCharacter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmrUniversalCharacterNode_UniversalCharacterId",
                table: "EmrUniversalCharacterNode",
                column: "UniversalCharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrUniversalCharacterNode");

            migrationBuilder.DropTable(
                name: "EmrUniversalCharacter");
        }
    }
}
