using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.ECIS.Call.Migrations
{
    public partial class Init_ConsultingRoomRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallConsultingRoomRegular",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsultingRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "诊室id"),
                    IsActived = table.Column<bool>(type: "bit", nullable: false, comment: "是否使用"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallConsultingRoomRegular", x => x.Id);
                },
                comment: "诊室固定表");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallConsultingRoomRegular");
        }
    }
}
