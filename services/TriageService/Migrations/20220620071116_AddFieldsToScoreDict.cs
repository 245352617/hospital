using Microsoft.EntityFrameworkCore.Migrations;

namespace SamJan.MicroService.PreHospital.TriageService.Migrations
{
    public partial class AddFieldsToScoreDict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Dict_ScoreDict",
                nullable: true,
                comment: "备注文本",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AddColumn<string>(
                name: "AssociatedOptions",
                table: "Dict_ScoreDict",
                nullable: true,
                comment: "关联选项");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Dict_ScoreDict",
                maxLength: 200,
                nullable: true,
                comment: "Icon Url");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Dict_ScoreDict",
                nullable: false,
                defaultValue: false,
                comment: "是否启用，0：否，1：是");

            migrationBuilder.AddColumn<int>(
                name: "OptionStyle",
                table: "Dict_ScoreDict",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RemarkStyle",
                table: "Dict_ScoreDict",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssociatedOptions",
                table: "Dict_ScoreDict");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Dict_ScoreDict");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Dict_ScoreDict");

            migrationBuilder.DropColumn(
                name: "OptionStyle",
                table: "Dict_ScoreDict");

            migrationBuilder.DropColumn(
                name: "RemarkStyle",
                table: "Dict_ScoreDict");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Dict_ScoreDict",
                type: "nvarchar(max)",
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "备注文本");
        }
    }
}
