namespace YiJian.ECIS.ShareModel.Responses;

/// <summary>
/// 分页信息
/// </summary>
public class PageResulModel
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public PageResulModel()
    {

    }

    /// <summary>
    /// 分页信息
    /// </summary>
    public PageResulModel(int currentPage, int pageSize, int rowCount)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        RowCount = rowCount;
    }

    /// <summary>
    /// 当前页码
    /// </summary>
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; } = 20;

    /// <summary>
    /// 总记录数
    /// </summary>
    public int RowCount { get; set; }

    /// <summary>
    /// 总页码
    /// </summary>
    public int PageCount
    {
        get
        {
            if (PageSize == 0) return 0;
            int pageCount = RowCount / PageSize; // 计算总页数
            if (RowCount % PageSize != 0) // 如果总记录数不能被每页大小整除
            {
                pageCount++; // 则总页数加1
            }
            return pageCount;
        }
    }

}
