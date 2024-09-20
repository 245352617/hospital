using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Medicine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_Operation",
                table: "Dict_Operation");

            migrationBuilder.RenameTable(
                name: "Dict_Operation",
                newName: "Dict_Operations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_Operations",
                table: "Dict_Operations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Dict_Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "药品编码"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "药品名称"),
                    ScientificName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "学名"),
                    Alias = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "别名"),
                    AliasPyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名拼音"),
                    AliasWbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "别名五笔码"),
                    WbCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "五笔"),
                    PyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音码"),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "中药分类"),
                    DefaultDosage = table.Column<double>(type: "float", maxLength: 20, nullable: false, comment: "默认剂量"),
                    DosageQty = table.Column<double>(type: "float", nullable: true, comment: "剂量"),
                    DosageUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂量单位"),
                    BasicUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "基本单位"),
                    BasicUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "基本单位价格"),
                    BigPackPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "包装价格"),
                    BigPackAmount = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "大包装量"),
                    BigPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "包装单位"),
                    SmallPackSinglePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "小包装单价"),
                    SmallPackUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "小包装单位"),
                    SmallPackAmount = table.Column<int>(type: "int", nullable: false, comment: "小包装量"),
                    ChildrenPrice = table.Column<decimal>(type: "decimal(18,2)", maxLength: 20, nullable: true, comment: "儿童价格"),
                    FixPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "批发价格"),
                    RetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "零售价格"),
                    InsureType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "医保类型：0自费,1甲类,2乙类，3丙类"),
                    InsureCode = table.Column<int>(type: "int", nullable: false, comment: "医保类型代码"),
                    PayRate = table.Column<int>(type: "int", nullable: true, comment: "医保支付比例"),
                    Factory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "厂家"),
                    FactoryCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "厂家代码"),
                    BatchNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "批次号"),
                    ExpirDate = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: true, comment: "失效期"),
                    Weight = table.Column<double>(type: "float", maxLength: 20, nullable: true, comment: "重量"),
                    WeightUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "重量单位"),
                    Volum = table.Column<double>(type: "float", maxLength: 20, nullable: true, comment: "体积"),
                    VolumUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "体积单位"),
                    Remark = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "备注"),
                    IsSkinTest = table.Column<bool>(type: "bit", nullable: true, comment: "皮试药"),
                    IsCompound = table.Column<bool>(type: "bit", nullable: true, comment: "复方药"),
                    IsDrunk = table.Column<bool>(type: "bit", nullable: true, comment: "麻醉药"),
                    ToxicLevel = table.Column<int>(type: "int", maxLength: 20, nullable: true, comment: "精神药  0非精神药,1一类精神药,2二类精神药"),
                    IsHighRisk = table.Column<bool>(type: "bit", nullable: true, comment: "高危药"),
                    IsRefrigerated = table.Column<bool>(type: "bit", nullable: true, comment: "冷藏药"),
                    IsTumour = table.Column<bool>(type: "bit", nullable: true, comment: "肿瘤药"),
                    AntibioticLevel = table.Column<int>(type: "int", nullable: true, comment: "抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级"),
                    IsPrecious = table.Column<bool>(type: "bit", nullable: true, comment: "贵重药"),
                    IsInsulin = table.Column<bool>(type: "bit", nullable: true, comment: "胰岛素"),
                    IsAnaleptic = table.Column<bool>(type: "bit", nullable: true, comment: "兴奋剂"),
                    IsAllergyTest = table.Column<bool>(type: "bit", nullable: true, comment: "试敏药"),
                    IsLimited = table.Column<bool>(type: "bit", nullable: true, comment: "限制性用药标识"),
                    LimitedNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "限制性用药描述"),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "规格"),
                    DosageForm = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "剂型"),
                    ExecDept = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "执行科室"),
                    DefaultUsage = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "默认用法"),
                    DefaultFrequency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "默认频次"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: true, comment: "急救药"),
                    PharmacyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "药房代码"),
                    Pharmacy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "药房"),
                    AntibioticPermission = table.Column<int>(type: "int", maxLength: 20, nullable: false, comment: "抗生素权限"),
                    PrescriptionPermission = table.Column<int>(type: "int", nullable: false, comment: "处方权"),
                    Stock = table.Column<int>(type: "int", nullable: false, comment: "库存"),
                    BaseFlag = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "基药标准  N普通,Y国基,P省基,C市基"),
                    Usage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "药物用途"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProHospitalUse = table.Column<bool>(type: "bit", nullable: true),
                    HospitalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HospitalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dict_Medicines", x => x.Id);
                },
                comment: "药品表");

            migrationBuilder.CreateIndex(
                name: "IX_Code",
                table: "Dict_Medicines",
                columns: new[] { "Code", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Dict_Medicines_Code",
                table: "Dict_Medicines",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "Dict_Medicines",
                columns: new[] { "Name", "IsDeleted" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dict_Medicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_Operations",
                table: "Dict_Operations");

            migrationBuilder.RenameTable(
                name: "Dict_Operations",
                newName: "Dict_Operation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_Operation",
                table: "Dict_Operation",
                column: "Id");

        }
    }
}
