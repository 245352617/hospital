using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Treat_1216 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Dict_Treats");

            migrationBuilder.DropColumn(
                name: "IsFirstAid",
                table: "Dict_Treats");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Dict_Treats",
                newName: "TreatUnit");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dict_Treats",
                newName: "TreatName");

            migrationBuilder.RenameColumn(
                name: "FeeTypeSub",
                table: "Dict_Treats",
                newName: "FeeTypeSubCode");

            migrationBuilder.RenameColumn(
                name: "FeeTypeMain",
                table: "Dict_Treats",
                newName: "FeeTypeMainCode");

            migrationBuilder.RenameColumn(
                name: "ExecDept",
                table: "Dict_Treats",
                newName: "ExecDeptName");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Dict_Treats",
                newName: "TreatCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_Treats_Code",
                table: "Dict_Treats",
                newName: "IX_Dict_Treats_TreatCode");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "诊疗处置类别名称");

            migrationBuilder.AddColumn<int>(
                name: "PlatformType",
                table: "Dict_Treats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "平台标识");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Dict_Treats");

            migrationBuilder.DropColumn(
                name: "PlatformType",
                table: "Dict_Treats");

            migrationBuilder.RenameColumn(
                name: "TreatUnit",
                table: "Dict_Treats",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "TreatName",
                table: "Dict_Treats",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TreatCode",
                table: "Dict_Treats",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "FeeTypeSubCode",
                table: "Dict_Treats",
                newName: "FeeTypeSub");

            migrationBuilder.RenameColumn(
                name: "FeeTypeMainCode",
                table: "Dict_Treats",
                newName: "FeeTypeMain");

            migrationBuilder.RenameColumn(
                name: "ExecDeptName",
                table: "Dict_Treats",
                newName: "ExecDept");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_Treats_TreatCode",
                table: "Dict_Treats",
                newName: "IX_Dict_Treats_Code");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Dict_Treats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "诊疗处置类别");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAid",
                table: "Dict_Treats",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否急救标识");
        }
    }
}
