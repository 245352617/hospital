using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Nursing.Migrations
{
    public partial class updateexeccolumnlength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsageName",
                table: "NursingRecipeExec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "用法名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "用法名称");

            migrationBuilder.AlterColumn<string>(
                name: "FinishNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "完成护士",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "完成护士");

            migrationBuilder.AlterColumn<string>(
                name: "FinishNurse",
                table: "NursingRecipeExec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "完成护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "完成护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "ExcuteNurseName",
                table: "NursingRecipeExec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "ExcuteNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行护士",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "执行护士");

            migrationBuilder.AlterColumn<string>(
                name: "CheckNurseName",
                table: "NursingRecipeExec",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "核对护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "核对护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "CheckNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "核对护士",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "核对护士");

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "NursingRecipe",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "患者名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "患者名称");

            migrationBuilder.AlterColumn<string>(
                name: "HisOrderNo",
                table: "NursingRecipe",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                comment: "HIS医嘱号",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "HIS医嘱号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsageName",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "用法名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "用法名称");

            migrationBuilder.AlterColumn<string>(
                name: "FinishNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "完成护士",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "完成护士");

            migrationBuilder.AlterColumn<string>(
                name: "FinishNurse",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "完成护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "完成护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "ExcuteNurseName",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "ExcuteNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "执行护士",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行护士");

            migrationBuilder.AlterColumn<string>(
                name: "CheckNurseName",
                table: "NursingRecipeExec",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "核对护士名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "核对护士名称");

            migrationBuilder.AlterColumn<string>(
                name: "CheckNurseCode",
                table: "NursingRecipeExec",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "核对护士",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "核对护士");

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "NursingRecipe",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "患者名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "患者名称");

            migrationBuilder.AlterColumn<string>(
                name: "HisOrderNo",
                table: "NursingRecipe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "HIS医嘱号",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldComment: "HIS医嘱号");
        }
    }
}
