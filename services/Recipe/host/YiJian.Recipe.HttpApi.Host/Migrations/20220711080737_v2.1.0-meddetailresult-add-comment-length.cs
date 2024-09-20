using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Recipe.Migrations
{
    public partial class v210meddetailresultaddcommentlength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MedicalNo",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "病历号 患者主索引id、用于条形码展示",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedType",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医嘱类型 处方：CF   非处方:YJ",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LgzxyyPayurl",
                table: "RC_MedDetailResult",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "支付二维码  深圳市龙岗中心医院微信公众",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LgjkzxPayurl",
                table: "RC_MedDetailResult",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "支付二维码 深圳市龙岗健康在线支付二维码",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HisNumber",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "His识别号 对应his处方识别（C）、医技序号（Y）  可用于二维码展示等",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelNumber",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "渠道识别号  4.5.3医嘱信息回传（his提供、需对接集成平台） chargeDetailNo projectItemNo",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelNo",
                table: "RC_MedDetailResult",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "HIS申请单号 处方：处方号码  医技：申请单id（检验、检查返回）",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MedicalNo",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "病历号 患者主索引id、用于条形码展示");

            migrationBuilder.AlterColumn<string>(
                name: "MedType",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "医嘱类型 处方：CF   非处方:YJ");

            migrationBuilder.AlterColumn<string>(
                name: "LgzxyyPayurl",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "支付二维码  深圳市龙岗中心医院微信公众");

            migrationBuilder.AlterColumn<string>(
                name: "LgjkzxPayurl",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "支付二维码 深圳市龙岗健康在线支付二维码");

            migrationBuilder.AlterColumn<string>(
                name: "HisNumber",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "His识别号 对应his处方识别（C）、医技序号（Y）  可用于二维码展示等");

            migrationBuilder.AlterColumn<string>(
                name: "ChannelNumber",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "渠道识别号  4.5.3医嘱信息回传（his提供、需对接集成平台） chargeDetailNo projectItemNo");

            migrationBuilder.AlterColumn<string>(
                name: "ChannelNo",
                table: "RC_MedDetailResult",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "HIS申请单号 处方：处方号码  医技：申请单id（检验、检查返回）");
        }
    }
}
