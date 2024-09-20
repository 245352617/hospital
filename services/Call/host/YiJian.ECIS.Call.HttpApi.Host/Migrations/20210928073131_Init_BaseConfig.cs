using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Init_BaseConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallBaseConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CallMode = table.Column<int>(type: "int", nullable: false, comment: "当前叫号模式"),
                    RegularEffectTime = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "模式生效时间"),
                    UpdateNoHour = table.Column<int>(type: "int", nullable: false, comment: "每日更新号码时间（小时）"),
                    UpdateNoMinute = table.Column<int>(type: "int", nullable: false, comment: "每日更新号码时间（分钟）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallBaseConfig", x => x.Id);
                },
                comment: "叫号设置-基础设置");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallBaseConfig");
        }
    }
}
