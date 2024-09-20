using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v210emrcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "EmrDataBindContext",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "患者名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "患者名称");

            migrationBuilder.AddColumn<string>(
                name: "OrgCode",
                table: "EmrDataBindContext",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "机构ID");

            migrationBuilder.AddColumn<string>(
                name: "RegisterSerialNo",
                table: "EmrDataBindContext",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                comment: "流水号");

            migrationBuilder.AddColumn<string>(
                name: "VisitNo",
                table: "EmrDataBindContext",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                comment: "就诊号");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrgCode",
                table: "EmrDataBindContext");

            migrationBuilder.DropColumn(
                name: "RegisterSerialNo",
                table: "EmrDataBindContext");

            migrationBuilder.DropColumn(
                name: "VisitNo",
                table: "EmrDataBindContext");

            migrationBuilder.AlterColumn<string>(
                name: "PatientName",
                table: "EmrDataBindContext",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "患者名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "患者名称");
        }
    }
}
