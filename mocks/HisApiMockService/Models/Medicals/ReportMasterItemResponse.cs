namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 检验项目
/// </summary>
public class ReportMasterItemResponse
{
    /// <summary>
    /// 检验内部ID
    /// </summary>
    public string MasterItemId { get; set; }

    /// <summary>
    /// 检验项目序号
    /// </summary>
    public string MasterItemNo { get; set; }

    /// <summary>
    /// 检验项目代码
    /// </summary>
    public string MasterItemCode { get; set; }

    /// <summary>
    /// 检验项目名称
    /// </summary>
    public string MasterItemName { get; set; }

}