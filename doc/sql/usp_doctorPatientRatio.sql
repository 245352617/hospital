USE [ECISReport]
GO
/****** Object:  StoredProcedure [dbo].[usp_doctorPatientRatio]    Script Date: 2023/2/1 11:33:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 医患比率存储过程 
CREATE PROC [dbo].[usp_doctorPatientRatio]
	@begin DATETIME, -- 开始时间
	@end DATETIME   -- 结束时间
AS 
  
SELECT s.FirstDoctorCode AS DoctorCode
      ,MAX(s.FirstDoctorName) AS DoctorName
      ,COUNT(s.PatientID)  AS ReceptionTota
FROM   (
           SELECT DISTINCT a.FirstDoctorCode
                 ,a.FirstDoctorName
                 ,a.PatientID
                 ,a.PatientName
                 ,a.VisitDate
           FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
           WHERE  a.VisitDate BETWEEN @begin AND @end
                  AND a.FirstDoctorCode>''
       )                   AS s
GROUP BY
       s.FirstDoctorCode
ORDER BY
       COUNT(s.PatientID) DESC