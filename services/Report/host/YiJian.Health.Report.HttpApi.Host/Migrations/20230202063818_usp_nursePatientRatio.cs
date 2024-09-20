using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class usp_nursePatientRatio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"-- 护患比率存储过程 
CREATE PROC [dbo].[usp_nursePatientRatio]
	@begin DATETIME, -- 开始时间
	@end DATETIME   -- 结束时间
AS 
BEGIN
    SELECT s.DutyNurseCode       AS NurseCode
          ,MAX(s.DutyNurseName)  AS NurseName
          ,COUNT(s.PatientID)    AS ReceptionTota
    FROM   (
               SELECT DISTINCT a.DutyNurseCode
                     ,a.DutyNurseName
                     ,a.PatientID
                     ,a.PatientName
                     ,a.VisitDate
               FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
               WHERE  a.VisitDate BETWEEN @begin AND @end
                      AND a.DutyNurseCode>''
           )                     AS s
    GROUP BY
           s.DutyNurseCode
    ORDER BY
           COUNT(s.PatientID) DESC
END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
