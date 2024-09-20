using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Medicine_20220412 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PharmacyName",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "药房",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "药房");

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyCode",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "药房代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "药房代码");

            migrationBuilder.AlterColumn<string>(
                name: "DosageForm",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "剂型",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "剂型");

            migrationBuilder.AlterColumn<string>(
                name: "BaseFlag",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "基药标准 01国基，02省基，03市基，04基药，05中草药，06非基药",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "基药标准  N普通,Y国基,P省基,C市基");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PharmacyName",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "药房",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "药房");

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyCode",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "药房代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "药房代码");

            migrationBuilder.AlterColumn<string>(
                name: "DosageForm",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "剂型",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "剂型");

            migrationBuilder.AlterColumn<string>(
                name: "BaseFlag",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "基药标准  N普通,Y国基,P省基,C市基",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "基药标准 01国基，02省基，03市基，04基药，05中草药，06非基药");
        }
    }
}
