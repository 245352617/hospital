using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_MedicalInsuranceCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicalInsuranceCode",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保编码");

            migrationBuilder.AlterColumn<string>(
                name: "GuideCode",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "指引ID 关联 ExamNote表code",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "指引ID");

            migrationBuilder.AlterColumn<string>(
                name: "GuideCode",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "指引ID 关联 ExamNote表code",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "指引ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalInsuranceCode",
                table: "Dict_Medicine");

            migrationBuilder.AlterColumn<string>(
                name: "GuideCode",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "指引ID",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "指引ID 关联 ExamNote表code");

            migrationBuilder.AlterColumn<string>(
                name: "GuideCode",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "指引ID",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "指引ID 关联 ExamNote表code");
        }
    }
}
