using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.MasterData.Migrations
{
    public partial class Update_TableName_1230 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sys_Sequences",
                table: "Sys_Sequences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_VitalSignExpressions",
                table: "Dict_VitalSignExpressions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ViewSettings",
                table: "Dict_ViewSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_Treats",
                table: "Dict_Treats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_Operations",
                table: "Dict_Operations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_MedicineUsages",
                table: "Dict_MedicineUsages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_Medicines",
                table: "Dict_Medicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_MedicineFrequencies",
                table: "Dict_MedicineFrequencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_DictionariesTypes",
                table: "Dict_DictionariesTypes");

            migrationBuilder.RenameTable(
                name: "Sys_Sequences",
                newName: "Sys_Sequence");

            migrationBuilder.RenameTable(
                name: "Dict_VitalSignExpressions",
                newName: "Dict_VitalSignExpression");

            migrationBuilder.RenameTable(
                name: "Dict_ViewSettings",
                newName: "Dict_ViewSetting");

            migrationBuilder.RenameTable(
                name: "Dict_Treats",
                newName: "Dict_Treat");

            migrationBuilder.RenameTable(
                name: "Dict_Operations",
                newName: "Dict_Operation");

            migrationBuilder.RenameTable(
                name: "Dict_MedicineUsages",
                newName: "Dict_MedicineUsage");

            migrationBuilder.RenameTable(
                name: "Dict_Medicines",
                newName: "Dict_Medicine");

            migrationBuilder.RenameTable(
                name: "Dict_MedicineFrequencies",
                newName: "Dict_MedicineFrequencie");

            migrationBuilder.RenameTable(
                name: "Dict_DictionariesTypes",
                newName: "Dict_DictionariesType");

            migrationBuilder.RenameIndex(
                name: "IX_Sys_Sequences_Code",
                table: "Sys_Sequence",
                newName: "IX_Sys_Sequence_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_VitalSignExpressions_ItemName",
                table: "Dict_VitalSignExpression",
                newName: "IX_Dict_VitalSignExpression_ItemName");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ViewSettings_Prop",
                table: "Dict_ViewSetting",
                newName: "IX_Dict_ViewSetting_Prop");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_Treats_TreatCode",
                table: "Dict_Treat",
                newName: "IX_Dict_Treat_TreatCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_MedicineUsages_UsageCode",
                table: "Dict_MedicineUsage",
                newName: "IX_Dict_MedicineUsage_UsageCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_Medicines_MedicineCode",
                table: "Dict_Medicine",
                newName: "IX_Dict_Medicine_MedicineCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_MedicineFrequencies_FrequencyCode",
                table: "Dict_MedicineFrequencie",
                newName: "IX_Dict_MedicineFrequencie_FrequencyCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_DictionariesTypes_DictionariesTypeCode",
                table: "Dict_DictionariesType",
                newName: "IX_Dict_DictionariesType_DictionariesTypeCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sys_Sequence",
                table: "Sys_Sequence",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_VitalSignExpression",
                table: "Dict_VitalSignExpression",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ViewSetting",
                table: "Dict_ViewSetting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_Treat",
                table: "Dict_Treat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_Operation",
                table: "Dict_Operation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_MedicineUsage",
                table: "Dict_MedicineUsage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_Medicine",
                table: "Dict_Medicine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_MedicineFrequencie",
                table: "Dict_MedicineFrequencie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_DictionariesType",
                table: "Dict_DictionariesType",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sys_Sequence",
                table: "Sys_Sequence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_VitalSignExpression",
                table: "Dict_VitalSignExpression");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_ViewSetting",
                table: "Dict_ViewSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_Treat",
                table: "Dict_Treat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_Operation",
                table: "Dict_Operation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_MedicineUsage",
                table: "Dict_MedicineUsage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_MedicineFrequencie",
                table: "Dict_MedicineFrequencie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_Medicine",
                table: "Dict_Medicine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dict_DictionariesType",
                table: "Dict_DictionariesType");

            migrationBuilder.RenameTable(
                name: "Sys_Sequence",
                newName: "Sys_Sequences");

            migrationBuilder.RenameTable(
                name: "Dict_VitalSignExpression",
                newName: "Dict_VitalSignExpressions");

            migrationBuilder.RenameTable(
                name: "Dict_ViewSetting",
                newName: "Dict_ViewSettings");

            migrationBuilder.RenameTable(
                name: "Dict_Treat",
                newName: "Dict_Treats");

            migrationBuilder.RenameTable(
                name: "Dict_Operation",
                newName: "Dict_Operations");

            migrationBuilder.RenameTable(
                name: "Dict_MedicineUsage",
                newName: "Dict_MedicineUsages");

            migrationBuilder.RenameTable(
                name: "Dict_MedicineFrequencie",
                newName: "Dict_MedicineFrequencies");

            migrationBuilder.RenameTable(
                name: "Dict_Medicine",
                newName: "Dict_Medicines");

            migrationBuilder.RenameTable(
                name: "Dict_DictionariesType",
                newName: "Dict_DictionariesTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Sys_Sequence_Code",
                table: "Sys_Sequences",
                newName: "IX_Sys_Sequences_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_VitalSignExpression_ItemName",
                table: "Dict_VitalSignExpressions",
                newName: "IX_Dict_VitalSignExpressions_ItemName");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_ViewSetting_Prop",
                table: "Dict_ViewSettings",
                newName: "IX_Dict_ViewSettings_Prop");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_Treat_TreatCode",
                table: "Dict_Treats",
                newName: "IX_Dict_Treats_TreatCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_MedicineUsage_UsageCode",
                table: "Dict_MedicineUsages",
                newName: "IX_Dict_MedicineUsages_UsageCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_MedicineFrequencie_FrequencyCode",
                table: "Dict_MedicineFrequencies",
                newName: "IX_Dict_MedicineFrequencies_FrequencyCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_Medicine_MedicineCode",
                table: "Dict_Medicines",
                newName: "IX_Dict_Medicines_MedicineCode");

            migrationBuilder.RenameIndex(
                name: "IX_Dict_DictionariesType_DictionariesTypeCode",
                table: "Dict_DictionariesTypes",
                newName: "IX_Dict_DictionariesTypes_DictionariesTypeCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sys_Sequences",
                table: "Sys_Sequences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_VitalSignExpressions",
                table: "Dict_VitalSignExpressions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_ViewSettings",
                table: "Dict_ViewSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_Treats",
                table: "Dict_Treats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_Operations",
                table: "Dict_Operations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_MedicineUsages",
                table: "Dict_MedicineUsages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_MedicineFrequencies",
                table: "Dict_MedicineFrequencies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_Medicines",
                table: "Dict_Medicines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dict_DictionariesTypes",
                table: "Dict_DictionariesTypes",
                column: "Id");
        }
    }
}
