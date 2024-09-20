using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "EmrDataBindContext",
                comment: "电子病历、文书绑定的数据上下文",
                oldComment: "数据上下文");

            migrationBuilder.AddColumn<int>(
                name: "Classify",
                table: "EmrTemplateCatalogue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "电子文书分类(0=电子病历,1=文书)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmrDataBindMap",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "绑定的数据名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "绑定的数据名称");

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "EmrDataBindMap",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "绑定的数据",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "绑定的数据");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "EmrDataBindMap",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "数据分类",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "数据分类");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classify",
                table: "EmrTemplateCatalogue");

            migrationBuilder.AlterTable(
                name: "EmrDataBindContext",
                comment: "数据上下文",
                oldComment: "电子病历、文书绑定的数据上下文");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmrDataBindMap",
                type: "nvarchar(max)",
                nullable: true,
                comment: "绑定的数据名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "绑定的数据名称");

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "EmrDataBindMap",
                type: "nvarchar(max)",
                nullable: true,
                comment: "绑定的数据",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "绑定的数据");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "EmrDataBindMap",
                type: "nvarchar(max)",
                nullable: true,
                comment: "数据分类",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "数据分类");
        }
    }
}
