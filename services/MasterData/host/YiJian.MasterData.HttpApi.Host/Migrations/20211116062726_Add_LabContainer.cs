using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_LabContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Container",
                table: "Dict_LabProjects");

            migrationBuilder.DropColumn(
                name: "Container",
                table: "Dict_ExamProjects");

            migrationBuilder.DropColumn(
                name: "ContainerCode",
                table: "Dict_ExamProjects");

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

            migrationBuilder.AddColumn<string>(
                name: "ContainerName",
                table: "Dict_LabProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "容器名称");

            migrationBuilder.CreateTable(
                name: "Dict_LabContainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContainerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "容器编码"),
                    ContainerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "容器名称"),
                    ContainerColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "容器颜色"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_LabContainers", x => x.Id);
                },
                comment: "容器编码");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_LabContainers_ContainerCode",
                table: "Dict_LabContainers",
                column: "ContainerCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_LabContainers");

            migrationBuilder.DropColumn(
                name: "ContainerName",
                table: "Dict_LabProjects");

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

            migrationBuilder.AddColumn<string>(
                name: "Container",
                table: "Dict_LabProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "容器名称");

            migrationBuilder.AddColumn<string>(
                name: "Container",
                table: "Dict_ExamProjects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "容器名称");

            migrationBuilder.AddColumn<string>(
                name: "ContainerCode",
                table: "Dict_ExamProjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "容器编码");
        }
    }
}
