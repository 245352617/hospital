using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations
{
    public partial class v210addtablepatientcase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RC_PatientCase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Piid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者唯一标识"),
                    PatientId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "患者Id"),
                    PatientName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "患者名称"),
                    Pastmedicalhistory = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "既往史"),
                    Presentmedicalhistory = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "现病史"),
                    Physicalexamination = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true, comment: "体格检查"),
                    Narrationname = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "主诉"),
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
                    table.PrimaryKey("PK_RC_PatientCase", x => x.Id);
                },
                comment: "患者的病例信息");

            migrationBuilder.CreateIndex(
                name: "IX_RC_PatientCase_Piid",
                table: "RC_PatientCase",
                column: "Piid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_PatientCase");
        }
    }
}
