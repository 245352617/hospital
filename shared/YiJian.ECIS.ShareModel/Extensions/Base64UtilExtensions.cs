using System.Text;

namespace YiJian.ECIS.ShareModel.Extensions;

/// <summary>
/// base64编码解码工具扩展
/// </summary>
public static class Base64UtilExtensions
{
    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="code"></param>
    /// <param name="code_type"></param>
    /// <returns></returns>
    public static string EncodeBase64(this string code, string code_type = "utf-8")
    {
        byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
        try
        {
            return Convert.ToBase64String(bytes);
        }
        catch
        {
            return code;
        }
    }

    /// <summary>
    /// 解码
    /// </summary>
    /// <param name="code"></param>
    /// <param name="code_type"></param>
    /// <returns></returns>
    public static string DecodeBase64(this string code, string code_type = "utf-8")
    {
        byte[] bytes = Convert.FromBase64String(code);
        try
        {
            return Encoding.GetEncoding(code_type).GetString(bytes);
        }
        catch
        {
            return code;
        }
    }
}
