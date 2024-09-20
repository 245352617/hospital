using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HisApiMockService.Migrations
{
    /// <inheritdoc />
    public partial class addRegisterPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegisterPatient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IdCard = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    PatientNo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IdentifyNO = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    PatientName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Birthday = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Sex = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    HomeAddress = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    OfficeAddress = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Nationaddress = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    PhoneNumberHome = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    PhoneNumberBus = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    MaritalStatus = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    SsnNum = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    EthnicGroup = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Nationality = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    PatientClass = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    VisitNum = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    AlternateVisitId = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    AppointmentId = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Job = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Weight = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    ContactName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ContactPhone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CardType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CardNo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    SeeDate = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    RegisterId = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    RegisterSequence = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    RegisterDate = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Shift = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    DeptId = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Operator = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    VisitNo = table.Column<string>(type: "TEXT", nullable: true),
                    DoctorCode = table.Column<string>(type: "TEXT", nullable: true),
                    IsCancel = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterPatient", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisterPatient");
        }
    }
}
