using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_Field_AddCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddCard",
                table: "Dict_MedicineUsage",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加卡片类型10.注射单,皮试单  08.雾化申请单  09.输液卡");

            migrationBuilder.AddColumn<string>(
                name: "AddCard",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加卡片类型10.注射单,皮试单  08.雾化申请单  09.输液卡");

            migrationBuilder.AddColumn<string>(
                name: "AddCard",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加卡片类型10.注射单,皮试单  08.雾化申请单  09.输液卡");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddCard",
                table: "Dict_MedicineUsage");

            migrationBuilder.DropColumn(
                name: "AddCard",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "AddCard",
                table: "Dict_ExamProject");
        }
    }
}
