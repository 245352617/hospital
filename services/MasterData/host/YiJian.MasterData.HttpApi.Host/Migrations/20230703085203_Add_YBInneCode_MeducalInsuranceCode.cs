using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YiJian.MasterData.Migrations
{
    public partial class Add_YBInneCode_MeducalInsuranceCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Sys_Region",
                comment: "地区字典");

            migrationBuilder.AlterTable(
                name: "Sys_ReceivedLog",
                comment: "接收数据日志");

            migrationBuilder.AlterTable(
                name: "Dict_Usage",
                comment: "用药途经");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Dict_ViewSetting",
                type: "bit",
                nullable: false,
                comment: "是否激活",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Dict_Treat",
                type: "int",
                nullable: false,
                comment: "主键",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "MeducalInsuranceCode",
                table: "Dict_Treat",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保编码");

            migrationBuilder.AddColumn<string>(
                name: "YBInneCode",
                table: "Dict_Treat",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "医保二级编码");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "Dict_Separation",
                type: "int",
                nullable: false,
                comment: "排序顺序",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Operation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "拼音编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Dict_Operation",
                type: "decimal(18,2)",
                nullable: false,
                comment: "价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "OperationName",
                table: "Dict_Operation",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "手术名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OperationCode",
                table: "Dict_Operation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "手术编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Dict_Operation",
                type: "int",
                nullable: false,
                comment: "时长",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "TargetUnit",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "TargetName",
                table: "Dict_ExamTarget",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "TargetCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "Dict_ExamTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "规格",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SpecialFlag",
                table: "Dict_ExamTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "特殊标识",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "Dict_ExamTarget",
                type: "int",
                nullable: false,
                comment: "排序号",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Qty",
                table: "Dict_ExamTarget",
                type: "decimal(18,2)",
                nullable: false,
                comment: "数量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "拼音码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "项目编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Dict_ExamTarget",
                type: "decimal(18,2)",
                nullable: false,
                comment: "价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherPrice",
                table: "Dict_ExamTarget",
                type: "decimal(18,2)",
                nullable: false,
                comment: "其它价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Dict_ExamTarget",
                type: "bit",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceType",
                table: "Dict_ExamTarget",
                type: "int",
                nullable: false,
                comment: "医保目录:0=自费,1=甲类,2=乙类,3=其它",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterCode",
                table: "Dict_Department",
                type: "nvarchar(max)",
                nullable: true,
                comment: "挂号科室编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CallScreenIp",
                table: "Dict_ConsultingRoom",
                type: "nvarchar(max)",
                nullable: true,
                comment: "叫号屏 IP",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeducalInsuranceCode",
                table: "Dict_Treat");

            migrationBuilder.DropColumn(
                name: "YBInneCode",
                table: "Dict_Treat");

            migrationBuilder.AlterTable(
                name: "Sys_Region",
                oldComment: "地区字典");

            migrationBuilder.AlterTable(
                name: "Sys_ReceivedLog",
                oldComment: "接收数据日志");

            migrationBuilder.AlterTable(
                name: "Dict_Usage",
                oldComment: "用药途经");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Dict_ViewSetting",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否激活");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Dict_Treat",
                type: "int",
                nullable: false,
                comment: "Id",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "主键")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "Dict_Separation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序顺序");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_Operation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "拼音编码");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Dict_Operation",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "价格");

            migrationBuilder.AlterColumn<string>(
                name: "OperationName",
                table: "Dict_Operation",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "手术名称");

            migrationBuilder.AlterColumn<string>(
                name: "OperationCode",
                table: "Dict_Operation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "手术编码");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Dict_Operation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "时长");

            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "五笔");

            migrationBuilder.AlterColumn<string>(
                name: "TargetUnit",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "单位");

            migrationBuilder.AlterColumn<string>(
                name: "TargetName",
                table: "Dict_ExamTarget",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "TargetCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "编码");

            migrationBuilder.AlterColumn<string>(
                name: "Specification",
                table: "Dict_ExamTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "规格");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialFlag",
                table: "Dict_ExamTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "特殊标识");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "Dict_ExamTarget",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序号");

            migrationBuilder.AlterColumn<decimal>(
                name: "Qty",
                table: "Dict_ExamTarget",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "数量");

            migrationBuilder.AlterColumn<string>(
                name: "PyCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "拼音码");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "项目编码");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Dict_ExamTarget",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "价格");

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherPrice",
                table: "Dict_ExamTarget",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "其它价格");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Dict_ExamTarget",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否启用");

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceType",
                table: "Dict_ExamTarget",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "医保目录:0=自费,1=甲类,2=乙类,3=其它");

            migrationBuilder.AlterColumn<string>(
                name: "RegisterCode",
                table: "Dict_Department",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "挂号科室编码");

            migrationBuilder.AlterColumn<string>(
                name: "CallScreenIp",
                table: "Dict_ConsultingRoom",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "叫号屏 IP");
        }
    }
}
