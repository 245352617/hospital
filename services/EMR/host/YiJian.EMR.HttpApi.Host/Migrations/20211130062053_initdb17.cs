using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class initdb17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "EmrPatientEmr",
                comment: "患者电子病历/文书...",
                oldComment: "患者电子病历");

            migrationBuilder.CreateTable(
                name: "EmrDataBindContext",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PI_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者唯一Id"),
                    PatientId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, comment: "患者Id"),
                    PatientName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "患者名称"),
                    WriterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "录入人Id"),
                    WriterName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "录入人名称"),
                    Classify = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "电子文书分类（0=电子病历，1=文书）"),
                    PatientEmrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "患者电子病历Id"),
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
                    table.PrimaryKey("PK_EmrDataBindContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrDataBindContext_EmrPatientEmr_PatientEmrId",
                        column: x => x.PatientEmrId,
                        principalTable: "EmrPatientEmr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "数据上下文");

            migrationBuilder.CreateTable(
                name: "EmrDataBindMap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "数据分类"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "绑定的数据名称"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "绑定的数据"),
                    DataBindContextId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "数据上下文Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmrDataBindMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmrDataBindMap_EmrDataBindContext_DataBindContextId",
                        column: x => x.DataBindContextId,
                        principalTable: "EmrDataBindContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "数据绑定字典");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataBindContext_PatientEmrId",
                table: "EmrDataBindContext",
                column: "PatientEmrId");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataBindContext_PI_ID",
                table: "EmrDataBindContext",
                column: "PI_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EmrDataBindMap_DataBindContextId",
                table: "EmrDataBindMap",
                column: "DataBindContextId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmrDataBindMap");

            migrationBuilder.DropTable(
                name: "EmrDataBindContext");

            migrationBuilder.AlterTable(
                name: "EmrPatientEmr",
                comment: "患者电子病历",
                oldComment: "患者电子病历/文书...");
        }
    }
}
