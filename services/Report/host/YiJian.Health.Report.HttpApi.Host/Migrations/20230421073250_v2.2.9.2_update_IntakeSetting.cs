using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.Health.Report.Migrations
{
    public partial class v2292_update_IntakeSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Way",
                table: "RpIntakeSetting");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RpIntakeSetting",
                newName: "Content");

            migrationBuilder.AlterColumn<string>(
                name: "InputMode",
                table: "RpIntakeSetting",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "方式",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50,
                oldComment: "输入类型");

            migrationBuilder.AddColumn<int>(
                name: "InputType",
                table: "RpIntakeSetting",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0,
                comment: "输入类型");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "RpIntake",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "颜色");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputType",
                table: "RpIntakeSetting");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "RpIntake");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "RpIntakeSetting",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "InputMode",
                table: "RpIntakeSetting",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0,
                comment: "输入类型",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "方式");

            migrationBuilder.AddColumn<string>(
                name: "Way",
                table: "RpIntakeSetting",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "方式");
        }
    }
}
