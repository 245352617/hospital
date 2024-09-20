using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.EMR.Migrations
{
    public partial class v_visitSerial_emrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var vewVisitSerialEmrs = @"--根据就诊流水号获取患者病历信息
CREATE VIEW v_visitSerial_emrs
AS
SELECT p.Id,
       p.EmrTitle,
       p.PatientName,
       p.DoctorName, 
       a.VisSerialNo
FROM   EmrPatientEmr AS p
       JOIN Ecis_Patient.dbo.Pat_AdmissionRecord AS a
            ON  a.PI_ID = p.PI_ID
";
            migrationBuilder.Sql(vewVisitSerialEmrs);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
