using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Medicine_20220408 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "Dict_Medicine");

            migrationBuilder.DropColumn(
                name: "ExpirDate",
                table: "Dict_Medicine");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Dict_Medicine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "批次号");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirDate",
                table: "Dict_Medicine",
                type: "datetime2",
                maxLength: 20,
                nullable: true,
                comment: "失效期");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Dict_Medicine",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "库存");
        }
    }
}
