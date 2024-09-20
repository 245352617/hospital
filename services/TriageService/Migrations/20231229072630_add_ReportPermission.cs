using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class add_ReportPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_ReportPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 20, nullable: true, comment: "用户名"),
                    ReportName = table.Column<string>(maxLength: 20, nullable: true, comment: "报表名")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_ReportPermission", x => x.Id);
                },
                comment: "分诊报表权限设置");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_ReportPermission");
        }
    }
}
