using System.IO;

namespace YiJian.Health.Report.Statisticses.Dto;

/// <summary>
/// 下载Excel文件返回结果
/// </summary>
public class DownloadExcelResponse
{
    /// <summary>
    /// 下载Excel文件返回结果
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="stream"></param>
    public DownloadExcelResponse(string fileName, Stream stream)
    {
        FileName = fileName;
        Stream = stream;
    }


    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 流
    /// </summary>
    public Stream Stream { get; set; }

}

