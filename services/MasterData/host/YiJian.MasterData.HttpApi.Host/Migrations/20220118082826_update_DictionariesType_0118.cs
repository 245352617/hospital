using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_DictionariesType_0118 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataFrom",
                table: "Dict_DictionariesType",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "数据来源，0：急诊添加，1：预检分诊同步");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFrom",
                table: "Dict_DictionariesType");
        }
    }
}
