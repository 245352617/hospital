using System.Text;

namespace YiJian.ECIS.ShareModel.Utils;

/// <summary>
/// url 地址编码和解码
/// </summary>
public static class UrlUtil
{
    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string UrlEncode(this string url)
    {
        return System.Web.HttpUtility.UrlEncode(url, Encoding.UTF8);
    }

    /// <summary>
    /// 解码
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string UrlDecode(this string url)
    {
        return System.Web.HttpUtility.UrlDecode(url, Encoding.UTF8);
    }

}
