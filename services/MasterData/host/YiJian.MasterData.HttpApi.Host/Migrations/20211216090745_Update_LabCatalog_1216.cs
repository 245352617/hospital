using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_LabCatalog_1216 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dict_LabTargets_Code",
                table: "Dict_LabTargets");

            migrationBuilder.DropIndex(
                name: "IX_Dict_LabSpecimens_Code",
                table: "Dict_LabSpecimens");

            migrationBuilder.DropIndex(
                name: "IX_Dict_LabProjects_CatalogCode",
                table: "Dict_LabProjects");

            migrationBuilder.DropIndex(
                name: "IX_Dict_LabProjects_Code",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Dict_LabTargets");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Dict_LabSpecimens");

            migrationBuilder.DropColumn(
                name: "IndexNo",
                table: "Dict_LabSpecimens");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Dict_LabSpecimens");

            migrationBuilder.DropColumn(
                name: "IndexNo",
                table: "Dict_LabSpecimenPositions");

            migrationBuilder.DropColumn(
                name: "Catalog",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "ExecDept",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "IndexNo",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "IsFirstAid",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "Specimen",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "Dept",
                table: "Dict_LabCatalogs");

            migrationBuilder.DropColumn(
                name: "DeptCode",
                table: "Dict_LabCatalogs");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Dict_LabTargets",
                newName: "TargetUnit");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dict_LabTargets",
                newName: "TargetName");

            migrationBuilder.RenameColumn(
                name: "IndexNo",
                table: "Dict_LabTargets",
                newName: "Sort");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Dict_LabTargets",
                newName: "Qty");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dict_LabProjects",
                newName: "ProjectName");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Dict_LabCatalogs",
                newName: "PositionName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dict_LabCatalogs",
                newName: "CatalogName");

            migrationBuilder.RenameColumn(
                name: "IndexNo",
                table: "Dict_LabCatalogs",
                newName: "Sort");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Dict_LabCatalogs",
                newName: "CatalogCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabCatalogs_Code",
                table: "Dict_LabCatalogs",
                newName: "IX_Dict_LabCatalogs_CatalogCode");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                table: "Dict_LabTargets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "项目编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "项目编码");

            migrationBuilder.AlterColumn<int>(
                name: "InsureType",
                table: "Dict_LabTargets",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "医保类型",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "医保类型");

            migrationBuilder.AddColumn<string>(
                name: "TargetCode",
                table: "Dict_LabTargets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "编码");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Dict_LabSpecimens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "排序号");

            migrationBuilder.AddColumn<string>(
                name: "SpecimenCode",
                table: "Dict_LabSpecimens",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "标本编码");

            migrationBuilder.AddColumn<string>(
                name: "SpecimenName",
                table: "Dict_LabSpecimens",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "标本名称");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Dict_LabSpecimenPositions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "排序号");

            migrationBuilder.AlterColumn<string>(
                name: "ContainerName",
                table: "Dict_LabProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "容器名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "容器名称");

            migrationBuilder.AlterColumn<string>(
                name: "ContainerCode",
                table: "Dict_LabProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "容器编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "容器编码");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogCode",
                table: "Dict_LabProjects",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "检验目录编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "检验分类编码");

            migrationBuilder.AddColumn<string>(
                name: "CatalogName",
                table: "Dict_LabProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "目录分类名称");

            migrationBuilder.AddColumn<string>(
                name: "ExecDeptName",
                table: "Dict_LabProjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "科室名称");

            migrationBuilder.AddColumn<int>(
                name: "PlatformType",
                table: "Dict_LabProjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "平台标识");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Dict_LabProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "位置名称");

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "Dict_LabProjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "编码");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Dict_LabProjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "排序号");

            migrationBuilder.AddColumn<string>(
                name: "SpecimenName",
                table: "Dict_LabProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "标本名称");

            migrationBuilder.AddColumn<string>(
                name: "ExecDeptCode",
                table: "Dict_LabCatalogs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室编码");

            migrationBuilder.AddColumn<string>(
                name: "ExecDeptName",
                table: "Dict_LabCatalogs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室名称");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabTargets_TargetCode",
                table: "Dict_LabTargets",
                column: "TargetCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabSpecimens_SpecimenCode",
                table: "Dict_LabSpecimens",
                column: "SpecimenCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabProjects_ProjectCode",
                table: "Dict_LabProjects",
                column: "ProjectCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dict_LabTargets_TargetCode",
                table: "Dict_LabTargets");

            migrationBuilder.DropIndex(
                name: "IX_Dict_LabSpecimens_SpecimenCode",
                table: "Dict_LabSpecimens");

            migrationBuilder.DropIndex(
                name: "IX_Dict_LabProjects_ProjectCode",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "TargetCode",
                table: "Dict_LabTargets");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Dict_LabSpecimens");

            migrationBuilder.DropColumn(
                name: "SpecimenCode",
                table: "Dict_LabSpecimens");

            migrationBuilder.DropColumn(
                name: "SpecimenName",
                table: "Dict_LabSpecimens");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Dict_LabSpecimenPositions");

            migrationBuilder.DropColumn(
                name: "CatalogName",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "ExecDeptName",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "PlatformType",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "SpecimenName",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "ExecDeptCode",
                table: "Dict_LabCatalogs");

            migrationBuilder.DropColumn(
                name: "ExecDeptName",
                table: "Dict_LabCatalogs");

            migrationBuilder.RenameColumn(
                name: "TargetUnit",
                table: "Dict_LabTargets",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "TargetName",
                table: "Dict_LabTargets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "Dict_LabTargets",
                newName: "IndexNo");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "Dict_LabTargets",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "ProjectName",
                table: "Dict_LabProjects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "Dict_LabCatalogs",
                newName: "IndexNo");

            migrationBuilder.RenameColumn(
                name: "PositionName",
                table: "Dict_LabCatalogs",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "CatalogName",
                table: "Dict_LabCatalogs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CatalogCode",
                table: "Dict_LabCatalogs",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_LabCatalogs_CatalogCode",
                table: "Dict_LabCatalogs",
                newName: "IX_Dict_LabCatalogs_Code");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                table: "Dict_LabTargets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "项目编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "项目编码");

            migrationBuilder.AlterColumn<string>(
                name: "InsureType",
                table: "Dict_LabTargets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医保类型",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldComment: "医保类型");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Dict_LabTargets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "编码");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Dict_LabSpecimens",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "标本编码");

            migrationBuilder.AddColumn<int>(
                name: "IndexNo",
                table: "Dict_LabSpecimens",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "排序号");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Dict_LabSpecimens",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "标本名称");

            migrationBuilder.AddColumn<int>(
                name: "IndexNo",
                table: "Dict_LabSpecimenPositions",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "排序号");

            migrationBuilder.AlterColumn<string>(
                name: "ContainerName",
                table: "Dict_LabProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "容器名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "容器名称");

            migrationBuilder.AlterColumn<string>(
                name: "ContainerCode",
                table: "Dict_LabProjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "容器编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "容器编码");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogCode",
                table: "Dict_LabProjects",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "检验分类编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "检验目录编码");

            migrationBuilder.AddColumn<string>(
                name: "Catalog",
                table: "Dict_LabProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "检验分类");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Dict_LabProjects",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "编码");

            migrationBuilder.AddColumn<string>(
                name: "ExecDept",
                table: "Dict_LabProjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "科室");

            migrationBuilder.AddColumn<int>(
                name: "IndexNo",
                table: "Dict_LabProjects",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "排序号");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAid",
                table: "Dict_LabProjects",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否急救");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Dict_LabProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "位置");

            migrationBuilder.AddColumn<string>(
                name: "Specimen",
                table: "Dict_LabProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "标本");

            migrationBuilder.AddColumn<string>(
                name: "Dept",
                table: "Dict_LabCatalogs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "科室");

            migrationBuilder.AddColumn<string>(
                name: "DeptCode",
                table: "Dict_LabCatalogs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "科室编码");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabTargets_Code",
                table: "Dict_LabTargets",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabSpecimens_Code",
                table: "Dict_LabSpecimens",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabProjects_CatalogCode",
                table: "Dict_LabProjects",
                column: "CatalogCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabProjects_Code",
                table: "Dict_LabProjects",
                column: "Code");
        }
    }
}
