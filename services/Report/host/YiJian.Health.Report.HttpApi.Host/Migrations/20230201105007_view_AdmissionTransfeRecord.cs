using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class view_AdmissionTransfeRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var viewAdmissionRecord = @"-- 入院患者记录视图
CREATE VIEW v_AdmissionRecord
AS
SELECT a.VisitNo
      ,a.VisSerialNo
      ,a.PatientID
      ,a.PatientName 
      ,a.SexName
      ,a.Age
      ,a.Birthday
      ,a.IDNo
      ,a.ContactsPhone
      ,a.FirstDoctorCode
      ,a.FirstDoctorName
      ,a.DutyNurseCode
      ,a.DutyNurseName
      ,a.VisitDate
      ,a.TriageLevel
      ,a.TriageLevelName 
      ,a.OutDeptTime  
FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a 
";

            var viewAdmissionTransfeRecord = @"-- 入院患者流转记录视图
CREATE VIEW v_AdmissionTransfeRecord
AS
SELECT a.VisitNo
      ,a.VisSerialNo
      ,a.PatientID
      ,a.PatientName 
      ,a.SexName
      ,a.Age
      ,a.Birthday
      ,a.IDNo
      ,a.ContactsPhone
      ,a.FirstDoctorCode
      ,a.FirstDoctorName
      ,a.DutyNurseCode
      ,a.DutyNurseName
      ,a.VisitDate
      ,a.TriageLevel
      ,a.TriageLevelName 
      ,a.OutDeptTime
      ,c.TransferType 
      ,c.TransferTime
      ,DATEDIFF(MINUTE, a.VisitDate, c.TransferTime) AS ResidenceTime -- 分钟
      ,c.FromAreaCode
      ,c.ToAreaCode
      ,c.ToArea
   	  ,c.TransferReason
FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
JOIN   Ecis_Patient.dbo.Pat_TransferRecord AS c
ON a.PI_ID = c.PI_ID 
";
            migrationBuilder.Sql(viewAdmissionRecord);
            migrationBuilder.Sql(viewAdmissionTransfeRecord);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
