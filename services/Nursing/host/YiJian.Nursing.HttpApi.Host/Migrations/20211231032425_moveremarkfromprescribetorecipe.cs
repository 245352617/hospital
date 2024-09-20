using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class moveremarkfromprescribetorecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "NursingPrescribe");

            migrationBuilder.AlterColumn<string>(
                name: "TraineeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "管培生编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "管培生代码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "开嘱医生名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "申请医生");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "开嘱医生编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "申请医生编码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "开嘱科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "申请科室");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "开嘱科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "申请科室编码");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "NursingRecipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "医嘱说明");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "NursingRecipe");

            migrationBuilder.AlterColumn<string>(
                name: "TraineeCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "管培生代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "管培生编码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "申请医生",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "开嘱医生名称");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDoctorCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "申请医生编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "开嘱医生编码");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "申请科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "开嘱科室名称");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyDeptCode",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "申请科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "开嘱科室编码");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "NursingPrescribe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "医嘱说明");
        }
    }
}
