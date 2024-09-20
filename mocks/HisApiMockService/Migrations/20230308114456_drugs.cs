using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HisApiMockService.Migrations
{
    /// <inheritdoc />
    public partial class drugs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HISMedicines",
                columns: table => new
                {
                    InvId = table.Column<decimal>(type: "TEXT", nullable: false),
                    MedicineCode = table.Column<decimal>(type: "TEXT", nullable: false),
                    MedicineName = table.Column<string>(type: "TEXT", nullable: false),
                    ScientificName = table.Column<string>(type: "TEXT", nullable: false),
                    Alias = table.Column<string>(type: "TEXT", nullable: false),
                    AliasPyCode = table.Column<string>(type: "TEXT", nullable: false),
                    AliasWbCode = table.Column<string>(type: "TEXT", nullable: false),
                    PyCode = table.Column<string>(type: "TEXT", nullable: false),
                    WbCode = table.Column<string>(type: "TEXT", nullable: false),
                    MedicineProperty = table.Column<decimal>(type: "TEXT", nullable: false),
                    DefaultDosage = table.Column<decimal>(type: "TEXT", nullable: false),
                    DosageQty = table.Column<decimal>(type: "TEXT", nullable: false),
                    DosageUnit = table.Column<string>(type: "TEXT", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    BigPackPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    BigPackFactor = table.Column<decimal>(type: "TEXT", nullable: false),
                    BigPackUnit = table.Column<string>(type: "TEXT", nullable: false),
                    SmallPackPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    SmallPackUnit = table.Column<string>(type: "TEXT", nullable: false),
                    SmallPackFactor = table.Column<decimal>(type: "TEXT", nullable: false),
                    ChildrenPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    FixPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    RetPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    InsuranceCode = table.Column<decimal>(type: "TEXT", nullable: false),
                    InsuranceName = table.Column<string>(type: "TEXT", nullable: false),
                    InsurancePayRate = table.Column<string>(type: "TEXT", nullable: false),
                    FactoryName = table.Column<string>(type: "TEXT", nullable: false),
                    FactoryCode = table.Column<decimal>(type: "TEXT", nullable: false),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: true),
                    WeightUnit = table.Column<string>(type: "TEXT", nullable: false),
                    Volume = table.Column<string>(type: "TEXT", nullable: false),
                    VolumeUnit = table.Column<string>(type: "TEXT", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: false),
                    IsSkinTest = table.Column<decimal>(type: "TEXT", nullable: true),
                    IsCompound = table.Column<string>(type: "TEXT", nullable: false),
                    IsDrunk = table.Column<int>(type: "INTEGER", nullable: true),
                    ToxicLevel = table.Column<decimal>(type: "TEXT", nullable: true),
                    IsHighRisk = table.Column<string>(type: "TEXT", nullable: false),
                    IsRefrigerated = table.Column<string>(type: "TEXT", nullable: false),
                    IsTumour = table.Column<string>(type: "TEXT", nullable: false),
                    AntibioticLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    IsPrecious = table.Column<int>(type: "INTEGER", nullable: true),
                    IsInsulin = table.Column<int>(type: "INTEGER", nullable: true),
                    IsAnaleptic = table.Column<int>(type: "INTEGER", nullable: true),
                    IsAllergyTest = table.Column<int>(type: "INTEGER", nullable: true),
                    IsLimited = table.Column<string>(type: "TEXT", nullable: false),
                    LimitedNote = table.Column<string>(type: "TEXT", nullable: false),
                    Specification = table.Column<string>(type: "TEXT", nullable: false),
                    DosageForm = table.Column<string>(type: "TEXT", nullable: false),
                    ExecDeptCode = table.Column<decimal>(type: "TEXT", nullable: false),
                    ExecDeptName = table.Column<string>(type: "TEXT", nullable: false),
                    UsageCode = table.Column<decimal>(type: "TEXT", nullable: true),
                    UsageName = table.Column<string>(type: "TEXT", nullable: false),
                    FrequencyCode = table.Column<string>(type: "TEXT", nullable: false),
                    FrequencyName = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<int>(type: "INTEGER", nullable: false),
                    IsFirstAid = table.Column<int>(type: "INTEGER", nullable: false),
                    MedicalInsuranceCode = table.Column<string>(type: "TEXT", nullable: false),
                    PharmacyCode = table.Column<string>(type: "TEXT", nullable: false),
                    PharmacyName = table.Column<string>(type: "TEXT", nullable: false),
                    AntibioticPermission = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrescriptionPermission = table.Column<decimal>(type: "TEXT", nullable: false),
                    BaseFlag = table.Column<string>(type: "TEXT", nullable: false),
                    PlatformType = table.Column<int>(type: "INTEGER", nullable: false),
                    Unpack = table.Column<int>(type: "INTEGER", nullable: false),
                    Qty = table.Column<decimal>(type: "TEXT", nullable: true),
                    EmergencySign = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HISMedicines", x => x.InvId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HISMedicines");
        }
    }
}
