using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.BodyParts.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class v2320_skin_propertyLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PressSmell",
                table: "IcuSkin",
                maxLength: 50,
                nullable: true,
                comment: "伤口气味",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "伤口气味");

            migrationBuilder.AlterColumn<string>(
                name: "PressColor",
                table: "IcuSkin",
                maxLength: 50,
                nullable: true,
                comment: "伤口颜色",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "伤口颜色");

            migrationBuilder.AlterColumn<string>(
                name: "PressArea",
                table: "IcuSkin",
                maxLength: 50,
                nullable: true,
                comment: "压疮面积",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "压疮面积");

            migrationBuilder.AlterColumn<string>(
                name: "NurseName",
                table: "IcuSkin",
                maxLength: 50,
                nullable: true,
                comment: "护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "NurseId",
                table: "IcuSkin",
                maxLength: 50,
                nullable: true,
                comment: "护士Id",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "护士Id");

            migrationBuilder.AlterColumn<string>(
                name: "ExudateColor",
                table: "IcuSkin",
                maxLength: 50,
                nullable: true,
                comment: "渗出液颜色",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "渗出液颜色");

            migrationBuilder.AlterColumn<string>(
                name: "ExudateAmount",
                table: "IcuSkin",
                maxLength: 50,
                nullable: true,
                comment: "渗出液量",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "渗出液量");

            migrationBuilder.AlterColumn<string>(
                name: "PressType",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "压疮类型",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "压疮类型");

            migrationBuilder.AlterColumn<string>(
                name: "PressSource",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "压疮来源",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "压疮来源");

            migrationBuilder.AlterColumn<string>(
                name: "PressSmell",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "伤口气味",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "伤口气味");

            migrationBuilder.AlterColumn<string>(
                name: "PressPart",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "压疮部位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "压疮部位");

            migrationBuilder.AlterColumn<string>(
                name: "PressColor",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "伤口颜色",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "伤口颜色");

            migrationBuilder.AlterColumn<string>(
                name: "PressArea",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "压疮面积",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "压疮面积");

            migrationBuilder.AlterColumn<string>(
                name: "NurseName",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "NurseId",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "护士Id",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "护士Id");

            migrationBuilder.AlterColumn<string>(
                name: "ExudateColor",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "渗出液颜色",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "渗出液颜色");

            migrationBuilder.AlterColumn<string>(
                name: "ExudateAmount",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: true,
                comment: "渗出液量",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "渗出液量");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PressSmell",
                table: "IcuSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "伤口气味",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "伤口气味");

            migrationBuilder.AlterColumn<string>(
                name: "PressColor",
                table: "IcuSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "伤口颜色",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "伤口颜色");

            migrationBuilder.AlterColumn<string>(
                name: "PressArea",
                table: "IcuSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "压疮面积",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "压疮面积");

            migrationBuilder.AlterColumn<string>(
                name: "NurseName",
                table: "IcuSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "护士名称",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "NurseId",
                table: "IcuSkin",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "护士Id",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "护士Id");

            migrationBuilder.AlterColumn<string>(
                name: "ExudateColor",
                table: "IcuSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "渗出液颜色",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "渗出液颜色");

            migrationBuilder.AlterColumn<string>(
                name: "ExudateAmount",
                table: "IcuSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "渗出液量",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "渗出液量");

            migrationBuilder.AlterColumn<string>(
                name: "PressType",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "压疮类型",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "压疮类型");

            migrationBuilder.AlterColumn<string>(
                name: "PressSource",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "压疮来源",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "压疮来源");

            migrationBuilder.AlterColumn<string>(
                name: "PressSmell",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "伤口气味",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "伤口气味");

            migrationBuilder.AlterColumn<string>(
                name: "PressPart",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "压疮部位",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "压疮部位");

            migrationBuilder.AlterColumn<string>(
                name: "PressColor",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "伤口颜色",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "伤口颜色");

            migrationBuilder.AlterColumn<string>(
                name: "PressArea",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "压疮面积",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "压疮面积");

            migrationBuilder.AlterColumn<string>(
                name: "NurseName",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "护士名称",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "NurseId",
                table: "IcuNursingSkin",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                comment: "护士Id",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "护士Id");

            migrationBuilder.AlterColumn<string>(
                name: "ExudateColor",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "渗出液颜色",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "渗出液颜色");

            migrationBuilder.AlterColumn<string>(
                name: "ExudateAmount",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "渗出液量",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "渗出液量");
        }
    }
}
