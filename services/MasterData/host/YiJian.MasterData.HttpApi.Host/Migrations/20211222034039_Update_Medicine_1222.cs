using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_Medicine_1222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dict_MedicineUsages_Code",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropIndex(
                name: "IX_Dict_Medicines_Code",
                table: "Dict_Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_Code",
                table: "Dict_Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_Name",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropColumn(
                name: "BigPackAmount",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "CategoryCode",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "ExecDept",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "Factory",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "InsureType",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "IsFirstAid",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "SmallPackAmount",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "IndexNo",
                table: "Dict_MedicineFrequencies");

            migrationBuilder.RenameColumn(
                name: "IndexNo",
                table: "Dict_MedicineUsages",
                newName: "Sort");

            migrationBuilder.RenameColumn(
                name: "VolumUnit",
                table: "Dict_Medicines",
                newName: "VolumeUnit");

            migrationBuilder.RenameColumn(
                name: "Volum",
                table: "Dict_Medicines",
                newName: "Volume");

            migrationBuilder.RenameColumn(
                name: "SmallPackSinglePrice",
                table: "Dict_Medicines",
                newName: "SmallPackPrice");

            migrationBuilder.RenameColumn(
                name: "Pharmacy",
                table: "Dict_Medicines",
                newName: "PharmacyName");

            migrationBuilder.RenameColumn(
                name: "PayRate",
                table: "Dict_Medicines",
                newName: "InsurancePayRate");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dict_Medicines",
                newName: "MedicineName");

            migrationBuilder.RenameColumn(
                name: "InsureCode",
                table: "Dict_Medicines",
                newName: "InsuranceCode");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dict_MedicineFrequencies",
                newName: "FrequencyName");

            migrationBuilder.RenameColumn(
                name: "ExecTimes",
                table: "Dict_MedicineFrequencies",
                newName: "ExecDayTimes");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Dict_MedicineFrequencies",
                newName: "FrequencyCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_MedicineFrequencies_Code",
                table: "Dict_MedicineFrequencies",
                newName: "IX_Dict_MedicineFrequencies_FrequencyCode");

            migrationBuilder.AlterTable(
                name: "Dict_Medicines",
                comment: "药品字典",
                oldComment: "药品表");

            migrationBuilder.AddColumn<string>(
                name: "UsageCode",
                table: "Dict_MedicineUsages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "编码");

            migrationBuilder.AddColumn<string>(
                name: "UsageName",
                table: "Dict_MedicineUsages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "FactoryCode",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "厂家代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "厂家代码");

            migrationBuilder.AlterColumn<decimal>(
                name: "DosageQty",
                table: "Dict_Medicines",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "剂量",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComment: "剂量");

            migrationBuilder.AddColumn<int>(
                name: "BigPackFactor",
                table: "Dict_Medicines",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "大包装量(大包装系数)");

            migrationBuilder.AddColumn<string>(
                name: "ExecDeptName",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "执行科室");

            migrationBuilder.AddColumn<string>(
                name: "FactoryName",
                table: "Dict_Medicines",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "厂家名称");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceType",
                table: "Dict_Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "医保类型：0自费,1甲类,2乙类，3丙类");

            migrationBuilder.AddColumn<string>(
                name: "MedicineCode",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "药品编码");

            migrationBuilder.AddColumn<string>(
                name: "MedicineProperty",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "药物属性：西药、中药、西药制剂、中药制剂");

            migrationBuilder.AddColumn<int>(
                name: "PlatformType",
                table: "Dict_Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "平台标识");

            migrationBuilder.AddColumn<int>(
                name: "SmallPackFactor",
                table: "Dict_Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "小包装量(小包装系数)");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Dict_MedicineFrequencies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "排序号");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_MedicineUsages_UsageCode",
                table: "Dict_MedicineUsages",
                column: "UsageCode");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_Medicines_MedicineCode",
                table: "Dict_Medicines",
                column: "MedicineCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dict_MedicineUsages_UsageCode",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropIndex(
                name: "IX_Dict_Medicines_MedicineCode",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "UsageCode",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropColumn(
                name: "UsageName",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropColumn(
                name: "BigPackFactor",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "ExecDeptName",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "FactoryName",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "InsuranceType",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "MedicineCode",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "MedicineProperty",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "PlatformType",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "SmallPackFactor",
                table: "Dict_Medicines");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Dict_MedicineFrequencies");

            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "Dict_MedicineUsages",
                newName: "IndexNo");

            migrationBuilder.RenameColumn(
                name: "VolumeUnit",
                table: "Dict_Medicines",
                newName: "VolumUnit");

            migrationBuilder.RenameColumn(
                name: "Volume",
                table: "Dict_Medicines",
                newName: "Volum");

            migrationBuilder.RenameColumn(
                name: "SmallPackPrice",
                table: "Dict_Medicines",
                newName: "SmallPackSinglePrice");

            migrationBuilder.RenameColumn(
                name: "PharmacyName",
                table: "Dict_Medicines",
                newName: "Pharmacy");

            migrationBuilder.RenameColumn(
                name: "MedicineName",
                table: "Dict_Medicines",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "InsurancePayRate",
                table: "Dict_Medicines",
                newName: "PayRate");

            migrationBuilder.RenameColumn(
                name: "InsuranceCode",
                table: "Dict_Medicines",
                newName: "InsureCode");

            migrationBuilder.RenameColumn(
                name: "FrequencyName",
                table: "Dict_MedicineFrequencies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FrequencyCode",
                table: "Dict_MedicineFrequencies",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "ExecDayTimes",
                table: "Dict_MedicineFrequencies",
                newName: "ExecTimes");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_MedicineFrequencies_FrequencyCode",
                table: "Dict_MedicineFrequencies",
                newName: "IX_Dict_MedicineFrequencies_Code");

            migrationBuilder.AlterTable(
                name: "Dict_Medicines",
                comment: "药品表",
                oldComment: "药品字典");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Dict_MedicineUsages",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "编码");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Dict_MedicineUsages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "名称");

            migrationBuilder.AlterColumn<string>(
                name: "FactoryCode",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "厂家代码",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "厂家代码");

            migrationBuilder.AlterColumn<double>(
                name: "DosageQty",
                table: "Dict_Medicines",
                type: "float",
                nullable: true,
                comment: "剂量",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "剂量");

            migrationBuilder.AddColumn<int>(
                name: "BigPackAmount",
                table: "Dict_Medicines",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "大包装量");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "中药分类");

            migrationBuilder.AddColumn<string>(
                name: "CategoryCode",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "中药分类编码");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "药品编码");

            migrationBuilder.AddColumn<string>(
                name: "ExecDept",
                table: "Dict_Medicines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "执行科室");

            migrationBuilder.AddColumn<string>(
                name: "Factory",
                table: "Dict_Medicines",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "厂家");

            migrationBuilder.AddColumn<string>(
                name: "InsureType",
                table: "Dict_Medicines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "医保类型：0自费,1甲类,2乙类，3丙类");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstAid",
                table: "Dict_Medicines",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "是否急救");

            migrationBuilder.AddColumn<int>(
                name: "SmallPackAmount",
                table: "Dict_Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "小包装量");

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "Dict_Medicines",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "药物用途");

            migrationBuilder.AddColumn<int>(
                name: "IndexNo",
                table: "Dict_MedicineFrequencies",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                comment: "排序号");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_MedicineUsages_Code",
                table: "Dict_MedicineUsages",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Dict_Medicines_Code",
                table: "Dict_Medicines",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_Code",
                table: "Dict_Medicines",
                columns: new[] { "Code", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_Name",
                table: "Dict_Medicines",
                columns: new[] { "Name", "IsDeleted" });
        }
    }
}
