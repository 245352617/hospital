using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v210udatecomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "EmrPhraseCatalogue",
                comment: "常用语目录");

            migrationBuilder.AlterTable(
                name: "EmrPhrase",
                comment: "病历常用语");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "EmrPhraseCatalogue",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "目录标题",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TemplateType",
                table: "EmrPhraseCatalogue",
                type: "int",
                nullable: false,
                comment: "模板类型，0=通用(全院)，1=科室，2=个人",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "EmrPhraseCatalogue",
                type: "int",
                nullable: false,
                comment: "排序号码",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Belonger",
                table: "EmrPhraseCatalogue",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "归属人 如果 TemplateType=2 归属者为医生Id doctorId, 如果 TemplateType=1 归属者为科室id deptid , 如果 TemplateType=0 归属者为hospital",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "EmrPhrase",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "标题",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "EmrPhrase",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "EmrPhrase",
                type: "int",
                nullable: false,
                comment: "排序号码",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CatalogueId",
                table: "EmrPhrase",
                type: "int",
                nullable: false,
                comment: "目录Id",
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "EmrPhraseCatalogue",
                oldComment: "常用语目录");

            migrationBuilder.AlterTable(
                name: "EmrPhrase",
                oldComment: "病历常用语");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "EmrPhraseCatalogue",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "目录标题");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateType",
                table: "EmrPhraseCatalogue",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "模板类型，0=通用(全院)，1=科室，2=个人");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "EmrPhraseCatalogue",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序号码");

            migrationBuilder.AlterColumn<string>(
                name: "Belonger",
                table: "EmrPhraseCatalogue",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "归属人 如果 TemplateType=2 归属者为医生Id doctorId, 如果 TemplateType=1 归属者为科室id deptid , 如果 TemplateType=0 归属者为hospital");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "EmrPhrase",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "标题");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "EmrPhrase",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "内容");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "EmrPhrase",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序号码");

            migrationBuilder.AlterColumn<int>(
                name: "CatalogueId",
                table: "EmrPhrase",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "目录Id");
        }
    }
}
