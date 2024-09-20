using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Init_RowConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallRowConfig",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Field = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Order = table.Column<short>(type: "smallint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Width = table.Column<short>(type: "smallint", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    Wrap = table.Column<bool>(type: "bit", nullable: false),
                    DefaultOrder = table.Column<short>(type: "smallint", nullable: false),
                    DefaultText = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DefaultWidth = table.Column<short>(type: "smallint", nullable: false),
                    DefaultVisible = table.Column<bool>(type: "bit", nullable: false),
                    DefaultWrap = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallRowConfig", x => x.Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallRowConfig");
        }
    }
}
