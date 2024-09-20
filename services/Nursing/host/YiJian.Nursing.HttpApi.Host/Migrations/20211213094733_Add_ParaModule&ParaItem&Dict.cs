using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YiJian.Nursing.Migrations
{
    public partial class Add_ParaModuleParaItemDict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Duct_Dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParaCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "参数代码"),
                    ParaName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "参数名称"),
                    DictCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "字典代码"),
                    DictValue = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "字典值"),
                    DictDesc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "字典值说明"),
                    ParentId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "上级代码"),
                    DictStandard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "字典标准（国标、自定义）"),
                    HisCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "HIS对照代码"),
                    HisName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "HIS对照"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室代码"),
                    ModuleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "模块代码"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, comment: "是否默认"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ValidState = table.Column<int>(type: "int", nullable: false, comment: "有效状态（1-有效，0-无效）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_Dict", x => x.Id);
                },
                comment: "表:导管字典-通用业务");

            migrationBuilder.CreateTable(
                name: "Duct_ParaItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "科室编号"),
                    ModuleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "参数所属模块"),
                    ParaCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "项目代码"),
                    ParaName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "项目名称"),
                    DisplayName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true, comment: "显示名称"),
                    ScoreCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "评分代码"),
                    GroupName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "导管分类"),
                    UnitName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "单位名称"),
                    ValueType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "数据类型"),
                    Style = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "文本类型"),
                    DecimalDigits = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "小数位数"),
                    MaxValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "最大值"),
                    MinValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "最小值"),
                    HighValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "高值"),
                    LowValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "低值"),
                    Threshold = table.Column<bool>(type: "bit", nullable: false, comment: "是否预警"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    DataSource = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "默认值"),
                    DictFlag = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "导管项目是否静态显示"),
                    GuidFlag = table.Column<bool>(type: "bit", nullable: true, comment: "是否评分"),
                    GuidId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "评分指引编号"),
                    StatisticalType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "特护单统计参数分类"),
                    DrawChartFlag = table.Column<int>(type: "int", nullable: false, comment: "绘制趋势图形"),
                    ColorParaCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "关联颜色"),
                    ColorParaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "关联颜色名称"),
                    PropertyParaCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "关联性状"),
                    PropertyParaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "关联性状名称"),
                    DeviceParaCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "设备参数代码"),
                    DeviceParaType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "设备参数类型（1-测量值，2-设定值）"),
                    HealthSign = table.Column<int>(type: "int", nullable: true, comment: "是否生命体征项目"),
                    KBCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "知识库代码"),
                    NuringViewCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "护理概览参数"),
                    AbnormalSign = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "是否异常体征参数"),
                    IsUsage = table.Column<bool>(type: "bit", nullable: true, comment: "是否用药所属项目"),
                    AddSource = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "添加来源"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ValidState = table.Column<int>(type: "int", nullable: false, comment: "有效状态"),
                    ParaItemType = table.Column<int>(type: "int", nullable: false, comment: "项目参数类型，用于区分监护仪或者呼吸机等")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_ParaItem", x => x.Id);
                },
                comment: "表:导管护理项目表");

            migrationBuilder.CreateTable(
                name: "Duct_ParaModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "模块代码"),
                    ModuleName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "模块名称"),
                    DisplayName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true, comment: "模块显示名称"),
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "科室代码"),
                    ModuleType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）"),
                    IsBloodFlow = table.Column<bool>(type: "bit", nullable: false, comment: "是否血流内导管"),
                    Py = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "模块拼音"),
                    Sort = table.Column<int>(type: "int", maxLength: 10, nullable: false, comment: "排序"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    ValidState = table.Column<int>(type: "int", nullable: false, comment: "是否有效(1-有效，0-无效)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duct_ParaModule", x => x.Id);
                },
                comment: "表:导管模块参数");

            migrationBuilder.CreateIndex(
                name: "IX_Duct_Dict_ParaCode",
                table: "Duct_Dict",
                column: "ParaCode");

            migrationBuilder.CreateIndex(
                name: "IX_Duct_ParaItem_DeptCode",
                table: "Duct_ParaItem",
                column: "DeptCode");

            migrationBuilder.CreateIndex(
                name: "IX_Duct_ParaModule_ModuleCode",
                table: "Duct_ParaModule",
                column: "ModuleCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Duct_Dict");

            migrationBuilder.DropTable(
                name: "Duct_ParaItem");

            migrationBuilder.DropTable(
                name: "Duct_ParaModule");
        }
    }
}
