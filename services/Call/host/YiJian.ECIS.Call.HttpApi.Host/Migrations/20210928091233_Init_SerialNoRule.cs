using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Init_SerialNoRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallSerialNoRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "科室id"),
                    Prefix = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, comment: "开头字母"),
                    SerialLength = table.Column<int>(type: "int", nullable: false, defaultValue: 1, comment: "流水号位数"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallSerialNoRule", x => x.Id);
                },
                comment: "排队号规则");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallSerialNoRule");
        }
    }
}
