using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.BodyParts.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class update_pi_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PI_ID",
                table: "IcuSignature",
                maxLength: 50,
                nullable: true,
                comment: "患者id",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "患者id");

            migrationBuilder.AlterColumn<string>(
                name: "PI_ID",
                table: "IcuNursingSkin",
                maxLength: 50,
                nullable: false,
                comment: "患者id",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "患者id");

            migrationBuilder.AlterColumn<string>(
                name: "PI_ID",
                table: "IcuNursingEvent",
                maxLength: 50,
                nullable: false,
                comment: "患者id",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "患者id");

            migrationBuilder.AlterColumn<string>(
                name: "PI_ID",
                table: "FileRecord",
                maxLength: 50,
                nullable: true,
                comment: "患者ID",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "患者ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PI_ID",
                table: "IcuSignature",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "患者id",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "患者id");

            migrationBuilder.AlterColumn<string>(
                name: "PI_ID",
                table: "IcuNursingSkin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "患者id",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldComment: "患者id");

            migrationBuilder.AlterColumn<string>(
                name: "PI_ID",
                table: "IcuNursingEvent",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "患者id",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldComment: "患者id");

            migrationBuilder.AlterColumn<string>(
                name: "PI_ID",
                table: "FileRecord",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "患者ID",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "患者ID");
        }
    }
}
