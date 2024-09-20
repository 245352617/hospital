using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class UpdateMedicineFieldLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PharmacyName",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "药房",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "药房");

            migrationBuilder.AlterColumn<string>(
                name: "LimitedNote",
                table: "Dict_Medicine",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                comment: "限制性用药描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "限制性用药描述");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "Dict_Medicine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行科室名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PharmacyName",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "药房",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "药房");

            migrationBuilder.AlterColumn<string>(
                name: "LimitedNote",
                table: "Dict_Medicine",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "限制性用药描述",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "限制性用药描述");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "Dict_Medicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行科室名称");
        }
    }
}
