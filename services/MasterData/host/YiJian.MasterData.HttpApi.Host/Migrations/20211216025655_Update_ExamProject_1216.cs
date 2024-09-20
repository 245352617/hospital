using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_ExamProject_1216 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dict_ExamProjects_CatalogCode",
                table: "Dict_ExamProjects");

            migrationBuilder.DropIndex(
                name: "IX_Dict_ExamProjects_Code",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "Catalog",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "IsFirstAid",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Dict_ExamProjects");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Dict_ExamProjects",
                newName: "PositionName");

            migrationBuilder.RenameColumn(
                name: "IndexNo",
                table: "Dict_ExamProjects",
                newName: "Sort");

            migrationBuilder.RenameColumn(
                name: "ExecDept",
                table: "Dict_ExamProjects",
                newName: "ExecDeptName");

            migrationBuilder.RenameColumn(
                name: "ExamPart",
                table: "Dict_ExamProjects",
                newName: "ExamPartCode");

            migrationBuilder.AddColumn<string>(
                name: "CatalogName",
                table: "Dict_ExamProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "目录名称");

            migrationBuilder.AddColumn<int>(
                name: "PlatformType",
                table: "Dict_ExamProjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "平台标识");

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "Dict_ExamProjects",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "检查编码");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Dict_ExamProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "检查名称");

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "Dict_ExamProjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行机房名称");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamProjects_ProjectCode",
                table: "Dict_ExamProjects",
                column: "ProjectCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dict_ExamProjects_ProjectCode",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "CatalogName",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "PlatformType",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "Dict_ExamProjects");

            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "Dict_ExamProjects",
                newName: "IndexNo");

            migrationBuilder.RenameColumn(
                name: "PositionName",
                table: "Dict_ExamProjects",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "ExecDeptName",
                table: "Dict_ExamProjects",
                newName: "ExecDept");

            migrationBuilder.RenameColumn(
                name: "ExamPartCode",
                table: "Dict_ExamProjects",
                newName: "ExamPart");

            migrationBuilder.AddColumn<string>(
                name: "Catalog",
                table: "Dict_ExamProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "分类名称");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Dict_ExamProjects",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "编码");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAid",
                table: "Dict_ExamProjects",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否急救");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Dict_ExamProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "名称");

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Dict_ExamProjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行机房描述");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamProjects_CatalogCode",
                table: "Dict_ExamProjects",
                column: "CatalogCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_ExamProjects_Code",
                table: "Dict_ExamProjects",
                column: "Code");
        }
    }
}
