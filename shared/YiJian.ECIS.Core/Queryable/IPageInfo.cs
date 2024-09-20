namespace YiJian.ECIS.Core
{
    public interface IPageInfo
    {
        long PageIndex { get; set; }

        uint PageSize { get; set; }
    }
}
