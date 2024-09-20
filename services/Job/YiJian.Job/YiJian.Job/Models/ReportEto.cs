namespace YiJian.Job.Models;

/// <summary>
/// 希望采集的月份参数
/// </summary>
public class MonthReportEto
{
    public MonthReportEto(int year, int month)
    {
        Year = year;
        Month = month;
    }

    /// <summary>
    /// 年份
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// 月份
    /// </summary>
    public int Month { get; set; }
}

/// <summary>
/// 希望采集的季度参数
/// </summary>
public class QuarterReportEto
{
    public QuarterReportEto(int year, int quarter)
    {
        Year = year;
        Quarter = quarter;
    }

    /// <summary>
    /// 年份
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// 季度
    /// </summary>
    public int Quarter { get; set; }
}

