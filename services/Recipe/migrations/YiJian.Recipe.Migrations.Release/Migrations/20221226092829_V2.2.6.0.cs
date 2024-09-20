using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Recipe.Migrations.Migrations
{
    public partial class V2260 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DailyFrequency",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "每日次(HIS的配置频次信息)",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "HIS频次编码");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "RC_QuickStartCatalogue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "序号");

            migrationBuilder.AlterColumn<string>(
                name: "DailyFrequency",
                table: "RC_Prescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "HIS频次编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "HIS频次编码");

            migrationBuilder.AddColumn<string>(
                name: "MedFee",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                comment: "费别 用于申请单费别显示");

            migrationBuilder.AddColumn<string>(
                name: "MedNature",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                comment: "性质 用于申请单性质显示");

            migrationBuilder.AlterColumn<string>(
                name: "ObjectiveContent",
                table: "RC_GroupConsultations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "会诊目的内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "会诊目的内容");

            migrationBuilder.CreateTable(
                name: "RC_ImmunizationRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcupunctureManipulation = table.Column<int>(type: "int", nullable: false, comment: "针法，0=四针法，1=五针法"),
                    Times = table.Column<int>(type: "int", nullable: false, comment: "接种次数（第一次，第二次，第三次...）"),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "接种的记录时间"),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false, comment: "是否确认的，回传给HIS之后就是确认的true，否则就是false"),
                    DoctorAdviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "医嘱Id"),
                    MedicineId = table.Column<int>(type: "int", nullable: false, comment: "药品ID,HIS给过来的"),
                    PatientId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, comment: "接种患者Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_ImmunizationRecord", x => x.Id);
                },
                comment: "疫苗记录信息");

            migrationBuilder.CreateIndex(
                name: "IX_RC_ImmunizationRecord_DoctorAdviceId",
                table: "RC_ImmunizationRecord",
                column: "DoctorAdviceId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_ImmunizationRecord_MedicineId",
                table: "RC_ImmunizationRecord",
                column: "MedicineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_ImmunizationRecord");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "RC_QuickStartCatalogue");

            migrationBuilder.DropColumn(
                name: "MedFee",
                table: "RC_MedDetailResult");

            migrationBuilder.DropColumn(
                name: "MedNature",
                table: "RC_MedDetailResult");

            migrationBuilder.AlterColumn<string>(
                name: "DailyFrequency",
                table: "RC_QuickStartMedicine",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "HIS频次编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "每日次(HIS的配置频次信息)");

            migrationBuilder.AlterColumn<string>(
                name: "DailyFrequency",
                table: "RC_Prescribe",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "HIS频次编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "HIS频次编码");

            migrationBuilder.AlterColumn<string>(
                name: "ObjectiveContent",
                table: "RC_GroupConsultations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "会诊目的内容",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "会诊目的内容");
        }
    }
}
