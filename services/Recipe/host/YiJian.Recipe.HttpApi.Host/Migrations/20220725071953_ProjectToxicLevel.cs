using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class ProjectToxicLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLimited",
                table: "RC_ProjectMedicineProp",
                type: "bit",
                nullable: true,
                comment: "限制性用药标识");

            migrationBuilder.AddColumn<string>(
                name: "LimitedNote",
                table: "RC_ProjectMedicineProp",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "限制性用药描述");

            migrationBuilder.AddColumn<int>(
                name: "ToxicLevel",
                table: "RC_ProjectMedicineProp",
                type: "int",
                nullable: true,
                comment: "精神药  0非精神药,1一类精神药,2二类精神药");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLimited",
                table: "RC_ProjectMedicineProp");

            migrationBuilder.DropColumn(
                name: "LimitedNote",
                table: "RC_ProjectMedicineProp");

            migrationBuilder.DropColumn(
                name: "ToxicLevel",
                table: "RC_ProjectMedicineProp");
        }
    }
}
