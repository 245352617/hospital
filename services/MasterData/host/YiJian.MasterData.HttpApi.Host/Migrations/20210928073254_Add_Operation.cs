using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Add_Operation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dict_Operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationCode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    OperationName = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    PyCode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AddUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleteUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField3 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField4 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtensionField5 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Operation", x => x.Id);
                },
                comment: "手术字典表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_Operation");
        }
    }
}
