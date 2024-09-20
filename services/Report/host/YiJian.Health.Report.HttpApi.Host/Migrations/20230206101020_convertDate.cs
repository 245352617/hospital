using Microsoft.EntityFrameworkCore.Migrations;

namespace YiJian.Health.Report.Migrations
{
    public partial class convertDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var func = @"--创建
-- 维度 0=月度,1=季度,2=年度
-- @formatDate Veidoo = 0 (eg:2023M2); Veidoo = 1 (eg:2023Q1); Veidoo = 2 (eg:2023)
-- @flag flag = 0(希望获取开始时间) flag=1(希望获取结束时间)
CREATE FUNCTION convertDate
(
	@veidoo         INT
   ,@formatDate     VARCHAR(10)
   ,@flag           INT
)
RETURNS DATETIME
AS
BEGIN
    DECLARE @ret DATETIME
    DECLARE @year INT
    DECLARE @month INT
    DECLARE @quarter INT
    DECLARE @index INT
    
    IF @veidoo=0 --月度
    BEGIN
        SELECT @index = CHARINDEX('M' ,@formatDate ,0)
        SELECT @year = SUBSTRING(@formatDate ,0 ,@index) 
        SELECT @month = SUBSTRING(@formatDate ,@index+1 ,LEN(@formatDate))
        
        SELECT @ret = DATEFROMPARTS(@year ,@month ,1)
        
        IF @flag=0 --返回开始时间
        BEGIN
            RETURN @ret
        END 
        --返回结束时间
        RETURN DATEADD(second ,-1 ,DATEADD(MONTH ,1 ,@ret))
    END
    ELSE 
    IF @veidoo=1 --季度
    BEGIN
        SELECT @index = CHARINDEX('Q' ,@formatDate ,0)
        SELECT @year = SUBSTRING(@formatDate ,0 ,@index) 
        SELECT @quarter = SUBSTRING(@formatDate ,@index+1 ,LEN(@formatDate))
        
        IF @quarter=1
            SET @month = 1
        ELSE 
        IF @quarter=2
            SET @month = 4
        ELSE 
        IF @quarter=3
            SET @month = 7
        ELSE 
        IF @quarter=4
            SET @month = 10
        
        SELECT @ret = DATEFROMPARTS(@year ,@month ,1)
        
        IF @flag=0 --返回开始时间
        BEGIN
            RETURN @ret
        END
        --返回结束时间
        RETURN DATEADD(second ,-1 ,DATEADD(MONTH ,3 ,@ret))
    END 
    --年度 
    SELECT @ret = DATEFROMPARTS(@formatDate ,1 ,1)
    IF @flag=0 --返回开始时间
    BEGIN
        RETURN @ret
    END
    
    --返回结束时间
    RETURN DATEADD(second ,-1 ,DATEADD(YEAR ,1 ,@ret))
END
";

            migrationBuilder.Sql(func);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
