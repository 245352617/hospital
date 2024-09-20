using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Change_Doctor_Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DoctorName",
                table: "CallCallingRecord",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "急诊医生姓名",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "接诊医生姓名");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "CallCallingRecord",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "急诊医生id",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "接诊医生id");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorName",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "急诊医生姓名",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "接诊医生姓名");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "急诊医生id",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "接诊医生id");

            migrationBuilder.Sql("Update CallRowConfig Set DefaultText = N'急诊医生', Text = N'急诊医生' Where [Key] = N'doctorName'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DoctorName",
                table: "CallCallingRecord",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "接诊医生姓名",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "急诊医生姓名");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "CallCallingRecord",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "接诊医生id",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "急诊医生id");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorName",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "接诊医生姓名",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "急诊医生姓名");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "CallCallInfo",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                comment: "接诊医生id",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true,
                oldComment: "急诊医生id");

            migrationBuilder.Sql("Update CallRowConfig Set DefaultText = N'接诊医生', Text = N'接诊医生' Where [Key] = N'doctorName'");
        }
    }
}
