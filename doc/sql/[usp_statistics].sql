/************************************************************
 * Code formatted by SoftTree SQL Assistant © v12.0.191
 * Time: 2023/2/11 18:30:46
 ************************************************************/

--创建统计报表采集存储过程    
--@reportType 报表类型  0=急诊科医患比, 1=急诊科护患比, 2=急诊科各级患者比例, 3=抢救室滞留时间中位数, 4=急诊抢救室患者死亡率    
--@veidoo  维度 0=月度,1=季度,2=年度    
--@formatDate Veidoo = 0 (eg:2023M2); Veidoo = 1 (eg:2023Q1); Veidoo = 2 (eg:2023)    
ALTER PROC usp_statistics @reportType INT , @veidoo INT, @formatDate VARCHAR(10)     
AS     
    
DECLARE @begin DATETIME    
DECLARE @end DATETIME    
DECLARE @patientCount INT    
DECLARE @doctorCount INT    
DECLARE @nurseCount INT    
DECLARE @year INT    
DECLARE @month INT    
DECLARE @quarter INT    
    
BEGIN
    SELECT @begin = dbo.convertDate(@veidoo ,@formatDate ,0)     
    SELECT @end = dbo.convertDate(@veidoo ,@formatDate ,1)    
    SELECT @year = DATEPART(YEAR ,@begin)    
    SELECT @month = DATEPART(MONTH ,@begin)    
    SELECT @quarter = DATEPART(QUARTER ,@begin) 
    
    PRINT(CONCAT('入参：@reportType=',@reportType,'  @veidoo=',@veidoo,'  @formatDate=',@formatDate))
    PRINT(CONCAT( '@begin=',@begin,@end,'  @year=',@year,' @month=',@month ,'  @quarter=',@quarter)) 
    
    --获取医患关系的患者总数    
    SELECT @patientCount = COUNT(a.PatientID)
    FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
    WHERE  a.VisitDate BETWEEN @begin AND @end 
    
    IF @reportType=0 --医生患者
    BEGIN
        --获取医患关系的医生总数    
        SELECT @doctorCount = COUNT(DISTINCT a.FirstDoctorCode)
        FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
        WHERE  a.VisitDate BETWEEN @begin AND @end
               AND a.FirstDoctorCode!='' 
        
        --没有患者记录，直接跳出    
        IF @patientCount=0
        BEGIN
            PRINT(N'没有查到患者信息') 
            RETURN 0
        END
        ELSE
        BEGIN
            PRINT(CONCAT(N'就诊患者数量=' ,@patientCount)) 
            PRINT(CONCAT(N'在岗医生数量=' ,@doctorCount))    
            IF @veidoo=0 --月度
            BEGIN
                --判断是否已经存在，如果已经存在记录，只要更新即可
                IF EXISTS (
                       SELECT TOP 1*
                       FROM   RpStatisticsMonthDoctorAndPatient AS t
                       WHERE  t.YearMonth = @begin
                   )
                BEGIN
                    DECLARE @id INT
                    SELECT TOP 1 @id = t.Id
                    FROM   RpStatisticsMonthDoctorAndPatient AS t
                    WHERE  t.YearMonth = @begin
                    
                    PRINT '该数据已经存在，只做更新'
                    UPDATE RpStatisticsMonthDoctorAndPatient
                    SET    YearMonth = @begin
                          ,[Year] = @year
                          ,[Month] = @month
                          ,DoctorTotal = @doctorCount
                          ,ReceptionTotal = @patientCount
                    WHERE  Id = @id
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    --新记录，添加
                    INSERT INTO RpStatisticsMonthDoctorAndPatient(YearMonth ,[Year] ,[Month] ,DoctorTotal ,ReceptionTotal) 
                           VALUES (@begin ,@year ,@month ,@doctorCount ,@patientCount)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=1 --季度
            BEGIN
                IF EXISTS(
                       SELECT TOP 1*
                       FROM   RpStatisticsQuarterDoctorAndPatient AS t
                       WHERE  t.YearQuarter = @begin
                   )
                BEGIN
                    DECLARE @id2 INT
                    SELECT TOP 1 @id2 = t.Id
                    FROM   RpStatisticsQuarterDoctorAndPatient AS t
                    WHERE  t.YearQuarter = @begin
                    
                    PRINT '该数据已经存在，只做更新'	
                    UPDATE RpStatisticsQuarterDoctorAndPatient
                    SET    YearQuarter = @begin
                          ,[Year] = @year
                          ,Quarter = @quarter
                          ,DoctorTotal = @doctorCount
                          ,ReceptionTotal = @patientCount
                    WHERE  Id = @id2
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsQuarterDoctorAndPatient(YearQuarter ,[Year] ,Quarter ,DoctorTotal ,ReceptionTotal) 
                           VALUES (@begin ,@year ,@quarter ,@doctorCount ,@patientCount)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=2 --年度
            BEGIN
                IF EXISTS (
                       SELECT TOP 1*
                       FROM   RpStatisticsYearDoctorAndPatient AS t
                       WHERE  t.[Year] = @year
                   )
                BEGIN
                    DECLARE @id3 INT 
                    SELECT TOP 1 @id3 = t.Id
                    FROM   RpStatisticsYearDoctorAndPatient AS t
                    WHERE  t.[Year] = @year
                    
                    PRINT '该数据已经存在，只做更新'	
                    UPDATE RpStatisticsYearDoctorAndPatient
                    SET    [Year] = @year
                          ,DoctorTotal = @doctorCount
                          ,ReceptionTotal = @patientCount
                    WHERE  Id = @id3
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsYearDoctorAndPatient([Year] ,DoctorTotal ,ReceptionTotal) 
                           VALUES (@year ,@doctorCount ,@patientCount)
                    
                    RETURN 1
                END
            END
        END
    END
    ELSE      
    IF @reportType=1 --护士患者
    BEGIN
        SELECT @nurseCount = COUNT(DISTINCT a.DutyNurseCode)
        FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
        WHERE  a.VisitDate BETWEEN @begin AND @end
               AND a.DutyNurseCode!='' 
        
        --没有患者记录，直接跳出    
        IF @patientCount=0
        BEGIN
            PRINT(N'没有查到患者信息') 
            RETURN 0
        END
        ELSE
        BEGIN
            PRINT(CONCAT(N'就诊患者数量=' ,@patientCount)) 
            PRINT(CONCAT(N'在岗护士数量=' ,@nurseCount))    
            IF @veidoo=0 --月度
            BEGIN
                IF EXISTS (
                       SELECT TOP 1*
                       FROM   RpStatisticsMonthNurseAndPatient AS t
                       WHERE  t.YearMonth = @begin
                   )
                BEGIN
                    DECLARE @id4 INT
                    SELECT TOP 1 @id4 = t.Id
                    FROM   RpStatisticsMonthNurseAndPatient AS t
                    WHERE  t.YearMonth = @begin
                    
                    PRINT '该数据已经存在，只做更新'
                    UPDATE RpStatisticsMonthNurseAndPatient
                    SET    YearMonth = @begin
                          ,[Year] = @year
                          ,[Month] = @month
                          ,NurseTotal = @nurseCount
                          ,ReceptionTotal = @patientCount
                    WHERE  Id = @id4
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsMonthNurseAndPatient(YearMonth ,[Year] ,[Month] ,NurseTotal ,ReceptionTotal) 
                           VALUES (@begin ,@year ,@month ,@nurseCount ,@patientCount)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=1 --季度
            BEGIN
                IF EXISTS (
                       SELECT TOP 1*
                       FROM   RpStatisticsQuarterNurseAndPatient AS t
                       WHERE  t.YearQuarter = @begin
                   )
                BEGIN
                    DECLARE @id5 INT
                    SELECT TOP 1 @id5 = t.Id
                    FROM   RpStatisticsQuarterNurseAndPatient AS t
                    WHERE  t.YearQuarter = @begin
                    
                    PRINT '该数据已经存在，只做更新'
                    UPDATE RpStatisticsQuarterNurseAndPatient
                    SET    YearQuarter = @begin
                          ,[Year] = @year
                          ,Quarter = @quarter
                          ,NurseTotal = @nurseCount
                          ,ReceptionTotal = @patientCount
                    WHERE  Id = @id5
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsQuarterNurseAndPatient(YearQuarter ,[Year] ,Quarter ,NurseTotal ,ReceptionTotal) 
                           VALUES (@begin ,@year ,@quarter ,@nurseCount ,@patientCount)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=2 --年度
            BEGIN
                IF EXISTS (
                       SELECT TOP 1*
                       FROM   RpStatisticsYearNurseAndPatient AS t
                       WHERE  t.[Year] = @year
                   )
                BEGIN
                    DECLARE @id6 INT
                    SELECT TOP 1 @id6 = t.Id
                    FROM   RpStatisticsYearNurseAndPatient AS t
                    WHERE  t.[Year] = @year
                    
                    PRINT '该数据已经存在，只做更新'
                    UPDATE RpStatisticsYearNurseAndPatient
                    SET    [Year] = @year
                          ,NurseTotal = @nurseCount
                          ,ReceptionTotal = @patientCount
                    WHERE  Id = @id6
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsYearNurseAndPatient([Year] ,NurseTotal ,ReceptionTotal) 
                           VALUES (@year ,@nurseCount ,@patientCount)
                    
                    RETURN 1
                END
            END
        END
    END
    ELSE      
    IF @reportType=2 -- 各科各级患者
    BEGIN
        SELECT a.TriageLevelName
              ,CASE a.TriageLevelName
                    WHEN N'Ⅰ 级' THEN COUNT(DISTINCT a.PatientID)
                    WHEN N'Ⅱ 级' THEN COUNT(DISTINCT a.PatientID)
                    WHEN N'Ⅲ 级' THEN COUNT(DISTINCT a.PatientID)
                    WHEN N'Ⅳa 级' THEN COUNT(DISTINCT a.PatientID)
                    WHEN N'Ⅳb 级' THEN COUNT(DISTINCT a.PatientID)
                    ELSE 0
               END AS lv
        INTO   #tmp
        FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
        WHERE  a.VisitDate BETWEEN @begin AND @end
        GROUP BY
               a.TriageLevelName 
        --定义变量    
        DECLARE @l1 INT=0    
        DECLARE @l2 INT=0    
        DECLARE @l3 INT=0    
        DECLARE @l4 INT=0    
        DECLARE @l5 INT=0 
        --变量赋值    
        SELECT TOP 1 @l1 = lv
        FROM   #tmp
        WHERE  TriageLevelName = N'Ⅰ 级'    
        
        SELECT TOP 1 @l2 = lv
        FROM   #tmp
        WHERE  TriageLevelName = N'Ⅱ 级'    
        
        SELECT TOP 1 @l3 = lv
        FROM   #tmp
        WHERE  TriageLevelName = N'Ⅲ 级'    
        
        SELECT TOP 1 @l4 = lv
        FROM   #tmp
        WHERE  TriageLevelName = N'Ⅳa 级'    
        
        SELECT TOP 1 @l5 = lv
        FROM   #tmp
        WHERE  TriageLevelName = N'Ⅳb 级' 
        
        DROP TABLE #tmp    
        
        BEGIN
            IF @veidoo=0 --月度
            BEGIN
                IF EXISTS(
                       SELECT TOP 1*
                       FROM   RpStatisticsMonthLevelAndPatient AS t
                       WHERE  t.YearMonth = @begin
                   )
                BEGIN
                    DECLARE @id7 INT
                    SELECT TOP 1 @id7 = t.Id
                    FROM   RpStatisticsMonthLevelAndPatient AS t
                    WHERE  t.YearMonth = @begin
                    
                    PRINT '该数据已经存在，只做更新'
                    UPDATE RpStatisticsMonthLevelAndPatient
                    SET    YearMonth = @begin
                          ,[Year] = @year
                          ,[Month] = @month
                          ,LI = @l1
                          ,LII = @l2
                          ,LIII = @l3
                          ,LIVa = @l4
                          ,LIVb = @l5
                    WHERE  Id = @id7
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsMonthLevelAndPatient(YearMonth ,[Year] ,[Month] ,LI ,LII ,LIII ,LIVa ,LIVb) 
                           VALUES (@begin ,@year ,@month ,@l1 ,@l2 ,@l3 ,@l4 ,@l5)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=1 --季度
            BEGIN
                IF EXISTS(
                       SELECT TOP 1*
                       FROM   RpStatisticsQuarterLevelAndPatient AS t
                       WHERE  t.YearQuarter = @begin
                   )
                BEGIN
                    DECLARE @id8 INT
                    SELECT TOP 1 @id8 = t.Id
                    FROM   RpStatisticsQuarterLevelAndPatient AS t
                    WHERE  t.YearQuarter = @begin
                    
                    PRINT '该数据已经存在，只做更新'
                    UPDATE RpStatisticsQuarterLevelAndPatient
                    SET    YearQuarter = @begin
                          ,[Year] = @year
                          ,Quarter = @quarter
                          ,LI = @l1
                          ,LII = @l2
                          ,LIII = @l3
                          ,LIVa = @l4
                          ,LIVb = @l5
                    WHERE  Id = @id8
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsQuarterLevelAndPatient(YearQuarter ,[Year] ,Quarter ,LI ,LII ,LIII ,LIVa ,LIVb) 
                           VALUES (@begin ,@year ,@quarter ,@l1 ,@l2 ,@l3 ,@l4 ,@l5)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=2 --年度
            BEGIN
                IF EXISTS (
                       SELECT TOP 1*
                       FROM   RpStatisticsYearLevelAndPatient AS t
                       WHERE  t.[Year] = @year
                   )
                BEGIN
                    DECLARE @id9 INT
                    SELECT TOP 1 @id9 = @id
                    FROM   RpStatisticsYearLevelAndPatient AS t
                    WHERE  t.[Year] = @year
                    
                    PRINT '该数据已经存在，只做更新' 
                    UPDATE RpStatisticsYearLevelAndPatient
                    SET    [Year] = @year
                          ,LI = @l1
                          ,LII = @l2
                          ,LIII = @l3
                          ,LIVa = @l4
                          ,LIVb = @l5
                    WHERE  Id = @id9
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsYearLevelAndPatient([Year] ,LI ,LII ,LIII ,LIVa ,LIVb) 
                           VALUES (@year ,@l1 ,@l2 ,@l3 ,@l4 ,@l5)
                    
                    RETURN 1
                END
            END
        END
    END
    ELSE     
    IF @reportType=3 --抢救室滞留时间
    BEGIN
        SELECT DATEDIFF(MINUTE ,a.VisitDate ,t.TransferTime) AS ResidenceTime --滞留时间
        INTO   #temp
        FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
               JOIN Ecis_Patient.dbo.Pat_TransferRecord AS t
                    ON  a.PI_ID = t.PI_ID
        WHERE  t.FromAreaCode = 'RescueArea'
               AND t.TransferType!='Death'
               AND a.VisitDate BETWEEN @begin AND @end    
        
        DECLARE @qjcount INT     
        DECLARE @avgnum INT    
        DECLARE @midnum INT 
        
        --抢救总数    
        SELECT @qjcount = COUNT(ResidenceTime)
        FROM   #temp 
        
        --平均数      
        SELECT @avgnum = AVG(ResidenceTime)
        FROM   #temp 
        
        --中位数     
        SELECT TOP 1 @midnum = b.ResidenceTime
        FROM   (
                   SELECT *
                         ,ROW_NUMBER() OVER(ORDER BY ResidenceTime) AS rowid
                   FROM   #temp
               ) AS b
        WHERE  rowid = @qjcount/2 
        
        DROP TABLE #temp     
        
        BEGIN
            IF @veidoo=0 --月度
            BEGIN
                IF EXISTS(
                       SELECT TOP 1*
                       FROM   RpStatisticsMonthEmergencyroomAndPatient AS t
                       WHERE  t.YearMonth = @begin
                   )
                BEGIN
                    DECLARE @id10 INT
                    SELECT TOP 1 @id10 = t.Id
                    FROM   RpStatisticsMonthEmergencyroomAndPatient AS t
                    WHERE  t.YearMonth = @begin
                    
                    PRINT '该数据已经存在，只做更新' 
                    UPDATE RpStatisticsMonthEmergencyroomAndPatient
                    SET    YearMonth = @begin
                          ,[Year] = @year
                          ,[Month] = @month
                          ,RescueTotal = @qjcount
                          ,AvgDetentionTime = @avgnum
                          ,MidDetentionTime = @midnum
                    WHERE  Id = @id10
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsMonthEmergencyroomAndPatient(
                               YearMonth
                              ,[Year]
                              ,[Month]
                              ,RescueTotal
                              ,AvgDetentionTime
                              ,MidDetentionTime
                           ) 
                           VALUES 
                           (@begin ,@year ,@month ,@qjcount ,@avgnum ,@midnum)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=1 --季度
            BEGIN
                IF EXISTS(
                       SELECT TOP 1*
                       FROM   RpStatisticsQuarterEmergencyroomAndPatient AS t
                       WHERE  t.YearQuarter = @begin
                   )
                BEGIN
                    DECLARE @id11 INT
                    SELECT TOP 1 @id11 = t.Id
                    FROM   RpStatisticsQuarterEmergencyroomAndPatient AS t
                    WHERE  t.YearQuarter = @begin
                    
                    PRINT '该数据已经存在，只做更新' 
                    UPDATE RpStatisticsQuarterEmergencyroomAndPatient
                    SET    YearQuarter = @begin
                          ,[Year] = @year
                          ,Quarter = @quarter
                          ,RescueTotal = @qjcount
                          ,AvgDetentionTime = @avgnum
                          ,MidDetentionTime = @midnum
                    WHERE  Id = @id11
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsQuarterEmergencyroomAndPatient(
                               YearQuarter
                              ,[Year]
                              ,Quarter
                              ,RescueTotal
                              ,AvgDetentionTime
                              ,MidDetentionTime
                           ) 
                           VALUES 
                           (@begin ,@year ,@quarter ,@qjcount ,@avgnum ,@midnum)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=2 --年度
            BEGIN
                IF EXISTS(
                       SELECT TOP 1*
                       FROM   RpStatisticsYearEmergencyroomAndPatient AS t
                       WHERE  t.[Year] = @year
                   )
                BEGIN
                    DECLARE @id12 INT
                    SELECT TOP 1 @id12 = t.Id
                    FROM   RpStatisticsYearEmergencyroomAndPatient AS t
                    WHERE  t.[Year] = @year
                    
                    PRINT '该数据已经存在，只做更新' 
                    UPDATE RpStatisticsYearEmergencyroomAndPatient
                    SET    [Year] = @year
                          ,RescueTotal = @qjcount
                          ,AvgDetentionTime = @avgnum
                          ,MidDetentionTime = @midnum
                    WHERE  Id = @id12
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsYearEmergencyroomAndPatient([Year] ,RescueTotal ,AvgDetentionTime ,MidDetentionTime) 
                           VALUES 
                           (@year ,@qjcount ,@avgnum ,@midnum)
                    
                    RETURN 1
                END
            END
        END
    END
    ELSE     
    IF @reportType=4 --抢救室患者死亡率
    BEGIN
        DECLARE @qjcount2 INT    
        DECLARE @deathtoll INT    
        
        SELECT @qjcount2 = COUNT(DISTINCT a.PatientID)
        FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
               JOIN Ecis_Patient.dbo.Pat_TransferRecord AS t
                    ON  a.PI_ID = t.PI_ID
        WHERE  t.FromAreaCode = 'RescueArea'
               AND a.VisitDate BETWEEN @begin AND @end    
        
        SELECT @deathtoll = COUNT(a.PatientID)
        FROM   Ecis_Patient.dbo.Pat_AdmissionRecord AS a
               JOIN Ecis_Patient.dbo.Pat_TransferRecord AS t
                    ON  a.PI_ID = t.PI_ID
        WHERE  t.FromAreaCode = 'RescueArea'
               AND t.TransferType = 'Death'
               AND a.VisitDate BETWEEN @begin AND @end    
        
        BEGIN
            IF @veidoo=0 --月度
            BEGIN
                IF EXISTS(
                       SELECT TOP 1*
                       FROM   RpStatisticsMonthEmergencyroomAndDeathPatient AS t
                       WHERE  t.YearMonth = @begin
                   )
                BEGIN
                    DECLARE @id13 INT
                    SELECT TOP 1 @id13 = t.Id
                    FROM   RpStatisticsMonthEmergencyroomAndDeathPatient AS t
                    WHERE  t.YearMonth = @begin
                    
                    PRINT '该数据已经存在，只做更新' 
                    UPDATE RpStatisticsMonthEmergencyroomAndDeathPatient
                    SET    YearMonth = @begin
                          ,[Year] = @year
                          ,[Month] = @month
                          ,RescueTotal = @qjcount2
                          ,DeathToll = @deathtoll
                    WHERE  Id = @id13
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsMonthEmergencyroomAndDeathPatient(YearMonth ,[Year] ,[Month] ,RescueTotal ,DeathToll) 
                           VALUES (@begin ,@year ,@month ,@qjcount2 ,@deathtoll)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=1 --季度
            BEGIN
                IF EXISTS(
                       SELECT TOP 1*
                       FROM   RpStatisticsQuarterEmergencyroomAndDeathPatient AS t
                       WHERE  t.YearQuarter = @begin
                   )
                BEGIN
                    DECLARE @id14 INT
                    SELECT TOP 1 @id14 = t.Id
                    FROM   RpStatisticsQuarterEmergencyroomAndDeathPatient AS t
                    WHERE  t.YearQuarter = @begin
                    
                    PRINT '该数据已经存在，只做更新' 
                    UPDATE RpStatisticsQuarterEmergencyroomAndDeathPatient
                    SET    YearQuarter = @begin
                          ,[Year] = @year
                          ,Quarter = @quarter
                          ,RescueTotal = @qjcount2
                          ,DeathToll = @deathtoll
                    WHERE  Id = @id14
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsQuarterEmergencyroomAndDeathPatient(YearQuarter ,[Year] ,Quarter ,RescueTotal ,DeathToll) 
                           VALUES 
                           (@begin ,@year ,@quarter ,@qjcount2 ,@deathtoll)
                    
                    RETURN 1
                END
            END
            ELSE     
            IF @veidoo=2 --年度
            BEGIN
                IF EXISTS(
                       SELECT TOP 1*
                       FROM   RpStatisticsYearEmergencyroomAndDeathPatient AS t
                       WHERE  t.[Year] = @year
                   )
                BEGIN
                    DECLARE @id15 INT
                    SELECT TOP 1 @id15 = t.Id
                    FROM   RpStatisticsYearEmergencyroomAndDeathPatient AS t
                    WHERE  t.[Year] = @year
                    
                    PRINT '该数据已经存在，只做更新' 
                    UPDATE RpStatisticsYearEmergencyroomAndDeathPatient
                    SET    [Year] = @year
                          ,RescueTotal = @qjcount2
                          ,DeathToll = @deathtoll
                    WHERE  Id = @id15
                    
                    RETURN 1
                END
                ELSE
                BEGIN
                    INSERT INTO RpStatisticsYearEmergencyroomAndDeathPatient([Year] ,RescueTotal ,DeathToll) 
                           VALUES 
                           (@year ,@qjcount2 ,@deathtoll)
                    
                    RETURN 1
                END
            END
        END
    END
    ELSE
    BEGIN
        PRINT('没有找到任何需要操作的报报表类型参数')
        RETURN-1
    END
END    