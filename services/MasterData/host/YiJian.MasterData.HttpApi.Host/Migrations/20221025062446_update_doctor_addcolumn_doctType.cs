using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class update_doctor_addcolumn_doctType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Dict_Doctor",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "联系电话",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "联系电话");

            migrationBuilder.AlterColumn<string>(
                name: "Skill",
                table: "Dict_Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医生擅长",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "医生擅长");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Dict_Doctor",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医生性别",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "医生性别");

            migrationBuilder.AlterColumn<string>(
                name: "Introdution",
                table: "Dict_Doctor",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "医生简介",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "医生简介");

            migrationBuilder.AddColumn<int>(
                name: "DoctorType",
                table: "Dict_Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "人员类型	1.急诊医生  2.急诊护士 0.其他人员");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorType",
                table: "Dict_Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Dict_Doctor",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "联系电话",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "联系电话");

            migrationBuilder.AlterColumn<string>(
                name: "Skill",
                table: "Dict_Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "医生擅长",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "医生擅长");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Dict_Doctor",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "医生性别",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "医生性别");

            migrationBuilder.AlterColumn<string>(
                name: "Introdution",
                table: "Dict_Doctor",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                comment: "医生简介",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "医生简介");
        }
    }
}
