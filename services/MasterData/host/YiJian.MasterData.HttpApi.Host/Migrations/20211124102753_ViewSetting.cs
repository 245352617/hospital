using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class ViewSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_ViewSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prop = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "属性"),
                    DefaultLabel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "默认标头"),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "标头"),
                    DefaultHeaderAlign = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认标头对齐"),
                    HeaderAlign = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "标头对齐"),
                    DefaultAlign = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "默认对齐"),
                    Align = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "对齐"),
                    DefaultWidth = table.Column<int>(type: "int", nullable: false, comment: "默认宽度"),
                    Width = table.Column<int>(type: "int", nullable: false, comment: "宽度"),
                    DefaultMinWidth = table.Column<int>(type: "int", nullable: false, comment: "默认最小宽度"),
                    MinWidth = table.Column<int>(type: "int", nullable: false, comment: "最小宽度"),
                    DefaultVisible = table.Column<bool>(type: "bit", nullable: false, comment: "默认显示"),
                    Visible = table.Column<bool>(type: "bit", nullable: false, comment: "是否显示"),
                    DefaultShowTooltip = table.Column<bool>(type: "bit", nullable: false, comment: "默认是否提示"),
                    ShowTooltip = table.Column<bool>(type: "bit", nullable: false, comment: "是否提示"),
                    DefaultIndex = table.Column<int>(type: "int", nullable: false, comment: "默认序号"),
                    Index = table.Column<int>(type: "int", nullable: false, comment: "序号"),
                    View = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "视图"),
                    Comment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "注释"),
                    ParentID = table.Column<int>(type: "int", nullable: false, comment: "父级ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ViewSettings", x => x.Id);
                },
                comment: "视图配置");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ViewSettings_Prop",
                table: "Dict_ViewSettings",
                column: "Prop");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ViewSettings");
        }
    }
}
