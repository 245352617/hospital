using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class add_doctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医生代码"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生姓名"),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "机构编码"),
                    BranchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "机构名称"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "科室代码"),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "科室名称"),
                    Sex = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医生性别"),
                    DoctorTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生职称"),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "联系电话"),
                    Skill = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "医生擅长"),
                    Introdution = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "医生简介"),
                    AnaesthesiaAuthority = table.Column<bool>(type: "bit", nullable: false, comment: "麻醉处方权限"),
                    DrugAuthority = table.Column<bool>(type: "bit", nullable: false, comment: "处方权限"),
                    SpiritAuthority = table.Column<bool>(type: "bit", nullable: false, comment: "精神处方权限"),
                    AntibioticAuthority = table.Column<bool>(type: "bit", nullable: false, comment: "抗生素处方权限"),
                    PracticeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "医师执业代码"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Doctor", x => x.Id);
                },
                comment: "医生表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_Doctor");
        }
    }
}
