using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class addlispacstreat3table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAid",
                table: "NursingPrescribes",
                type: "bit",
                nullable: true,
                comment: "是否是急救药");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstAid",
                table: "NursingPrescribes");
        }
    }
}
