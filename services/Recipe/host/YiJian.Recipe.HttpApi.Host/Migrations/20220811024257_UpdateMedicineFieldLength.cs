using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class UpdateMedicineFieldLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LimitedNote",
                table: "RC_Toxic",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                comment: "限制性用药描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "限制性用药描述");

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyName",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "药房",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "药房");

            migrationBuilder.AlterColumn<string>(
                name: "LimitedNote",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                comment: "限制性用药描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "限制性用药描述");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行科室名称");

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyName",
                table: "RC_ProjectMedicineProp",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "药房",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "药房");

            migrationBuilder.AlterColumn<string>(
                name: "LimitedNote",
                table: "RC_ProjectMedicineProp",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                comment: "限制性用药描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "限制性用药描述");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LimitedNote",
                table: "RC_Toxic",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "限制性用药描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "限制性用药描述");

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyName",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "药房",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "药房");

            migrationBuilder.AlterColumn<string>(
                name: "LimitedNote",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "限制性用药描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "限制性用药描述");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行科室名称");

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyName",
                table: "RC_ProjectMedicineProp",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "药房",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "药房");

            migrationBuilder.AlterColumn<string>(
                name: "LimitedNote",
                table: "RC_ProjectMedicineProp",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "限制性用药描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "限制性用药描述");
        }
    }
}
