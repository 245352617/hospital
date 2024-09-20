using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class add_TreatCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "Dict_LabTree",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "父级id",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "父级id");

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "Dict_LabTree",
                type: "nvarchar(max)",
                nullable: true,
                comment: "当前节点全路径",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "当前节点全路径");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeCode",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加药品编码(多个用','分隔)");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeName",
                table: "Dict_LabProject",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "附加药品名称(多个用','分隔)");

            migrationBuilder.AddColumn<string>(
                name: "TreatCode",
                table: "Dict_LabProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加处置编码(多个用','分隔)");

            migrationBuilder.AddColumn<string>(
                name: "TreatName",
                table: "Dict_LabProject",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "附加处置名称(多个用','分隔)");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "Dict_ExamTree",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "父级id",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "父级id");

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "Dict_ExamTree",
                type: "nvarchar(max)",
                nullable: true,
                comment: "当前节点全路径",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "当前节点全路径");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeCode",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加药品编码(多个用','分隔)");

            migrationBuilder.AddColumn<string>(
                name: "PrescribeName",
                table: "Dict_ExamProject",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "附加药品名称(多个用','分隔)");

            migrationBuilder.AddColumn<string>(
                name: "TreatCode",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "附加处置编码(多个用','分隔)");

            migrationBuilder.AddColumn<string>(
                name: "TreatName",
                table: "Dict_ExamProject",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "附加处置名称(多个用','分隔)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescribeCode",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "PrescribeName",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "TreatCode",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "TreatName",
                table: "Dict_LabProject");

            migrationBuilder.DropColumn(
                name: "PrescribeCode",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "PrescribeName",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "TreatCode",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "TreatName",
                table: "Dict_ExamProject");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "Dict_LabTree",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "父级id",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "父级id");

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "Dict_LabTree",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "当前节点全路径",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "当前节点全路径");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "Dict_ExamTree",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "父级id",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "父级id");

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "Dict_ExamTree",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "当前节点全路径",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "当前节点全路径");
        }
    }
}
