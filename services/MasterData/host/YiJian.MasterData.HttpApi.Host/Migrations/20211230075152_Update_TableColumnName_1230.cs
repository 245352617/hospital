using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_TableColumnName_1230 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionCode",
                table: "Dict_LabCatalog");

            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Dict_LabCatalog");

            migrationBuilder.DropColumn(
                name: "PositionCode",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Dict_ExamProject");

            migrationBuilder.DropColumn(
                name: "ExamPartCode",
                table: "Dict_ExamCatalog");

            migrationBuilder.DropColumn(
                name: "PositionCode",
                table: "Dict_ExamCatalog");

            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Dict_ExamCatalog");

            migrationBuilder.RenameColumn(
                name: "IndexNo",
                table: "Dict_ExamCatalog",
                newName: "Sort");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "执行科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "执行科室");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Dict_Treats",
                type: "int",
                nullable: false,
                comment: "Id",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行科室");

            migrationBuilder.AlterColumn<string>(
                name: "BigPackUnit",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "大包装单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "包装单位");

            migrationBuilder.AlterColumn<decimal>(
                name: "BigPackPrice",
                table: "Dict_Medicines",
                type: "decimal(18,2)",
                nullable: false,
                comment: "大包装价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "包装价格");

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceType",
                table: "Dict_LabTarget",
                type: "int",
                maxLength: 20,
                nullable: false,
                comment: "医保目录:0=自费,1=甲类,2=乙类,3=其它",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldComment: "医保类型");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Dict_LabSpecimenPosition",
                type: "bit",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "SpecimenPartName",
                table: "Dict_LabProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "检验位置名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "检验部位名称");

            migrationBuilder.AlterColumn<string>(
                name: "SpecimenPartCode",
                table: "Dict_LabProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "检验位置编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "检验部位编码");

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherPrice",
                table: "Dict_LabProject",
                type: "decimal(18,2)",
                nullable: false,
                comment: "其他价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "价格");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogName",
                table: "Dict_LabProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "检验目录名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "目录分类名称");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Dict_LabContainer",
                type: "bit",
                nullable: false,
                comment: "是否启用",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogName",
                table: "Dict_LabCatalog",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "分类名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "分类编码");

            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
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

            migrationBuilder.AlterColumn<int>(
                name: "Qty",
                table: "Dict_ExamTarget",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
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
                oldMaxLength: 20,
                oldComment: "医保类型");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "Dict_ExamProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "检查名称");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                table: "Dict_ExamProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "检查编码");

            migrationBuilder.AlterColumn<string>(
                name: "PartName",
                table: "Dict_ExamProject",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "检查部位名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "检查部位名称");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogCode",
                table: "Dict_ExamProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "目录编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "分类编码");

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Dict_ExamCatalog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行机房名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行机房");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogName",
                table: "Dict_ExamCatalog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "检查名称");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogCode",
                table: "Dict_ExamCatalog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "检查编码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "Dict_ExamCatalog",
                newName: "IndexNo");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "Dict_Treats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "执行科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "执行科室名称");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Dict_Treats",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ExecDeptName",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "执行科室名称");

            migrationBuilder.AlterColumn<string>(
                name: "BigPackUnit",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "包装单位",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "大包装单位");

            migrationBuilder.AlterColumn<decimal>(
                name: "BigPackPrice",
                table: "Dict_Medicines",
                type: "decimal(18,2)",
                nullable: false,
                comment: "包装价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "大包装价格");

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceType",
                table: "Dict_LabTarget",
                type: "int",
                maxLength: 20,
                nullable: false,
                comment: "医保类型",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldComment: "医保目录:0=自费,1=甲类,2=乙类,3=其它");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Dict_LabSpecimenPosition",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否启用");

            migrationBuilder.AlterColumn<string>(
                name: "SpecimenPartName",
                table: "Dict_LabProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "检验部位名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "检验位置名称");

            migrationBuilder.AlterColumn<string>(
                name: "SpecimenPartCode",
                table: "Dict_LabProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "检验部位编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "检验位置编码");

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherPrice",
                table: "Dict_LabProject",
                type: "decimal(18,2)",
                nullable: false,
                comment: "价格",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "其他价格");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogName",
                table: "Dict_LabProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "目录分类名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "检验目录名称");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Dict_LabContainer",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "是否启用");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogName",
                table: "Dict_LabCatalog",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "分类编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "分类名称");

            migrationBuilder.AddColumn<string>(
                name: "PositionCode",
                table: "Dict_LabCatalog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "位置编码");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Dict_LabCatalog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "位置描述");

            migrationBuilder.AlterColumn<string>(
                name: "WbCode",
                table: "Dict_ExamTarget",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "五笔",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

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

            migrationBuilder.AlterColumn<int>(
                name: "Qty",
                table: "Dict_ExamTarget",
                type: "int",
                nullable: false,
                comment: "数量",
                oldClrType: typeof(int),
                oldType: "int");

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
                maxLength: 20,
                nullable: false,
                comment: "医保类型",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "Dict_ExamProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "检查名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldComment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                table: "Dict_ExamProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "检查编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "编码");

            migrationBuilder.AlterColumn<string>(
                name: "PartName",
                table: "Dict_ExamProject",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "检查部位名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "检查部位名称");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogCode",
                table: "Dict_ExamProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "分类编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "目录编码");

            migrationBuilder.AddColumn<string>(
                name: "PositionCode",
                table: "Dict_ExamProject",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "位置编码");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Dict_ExamProject",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "位置");

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Dict_ExamCatalog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行机房",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "执行机房名称");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogName",
                table: "Dict_ExamCatalog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "检查名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "CatalogCode",
                table: "Dict_ExamCatalog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "检查编码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "编码");

            migrationBuilder.AddColumn<string>(
                name: "ExamPartCode",
                table: "Dict_ExamCatalog",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "检查部位编码");

            migrationBuilder.AddColumn<string>(
                name: "PositionCode",
                table: "Dict_ExamCatalog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "位置编码");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Dict_ExamCatalog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "位置名称");
        }
    }
}
