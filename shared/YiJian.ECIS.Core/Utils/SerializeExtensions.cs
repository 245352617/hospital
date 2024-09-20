using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace YiJian.ECIS.Core.Utils;

/// <summary>
///     JSON序列化扩展
/// </summary>
public static class SerializeExtensions
{
    /// <summary>
    ///     序列化参数
    /// </summary>
    private static readonly JsonSerializerOptions JsonSerializeOptions = new()
    {
        WriteIndented = true,
        IgnoreNullValues = false
    };


    /// <summary>
    ///     对象序列化为JSON字符串
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToJsonString(this object source)
    {
        return JsonSerializer.Serialize(source, JsonSerializeOptions);
    }

    /// <summary>
    ///     对象序列化为XML字符串
    /// </summary>
    /// <param name="source"></param>
    /// <param name="encoding">字符编码，默认UTF8</param>
    /// <returns></returns>
    public static string ToXmlString(this object source, Encoding encoding = null)
    {
        var enc = encoding ?? Encoding.UTF8;
        var serializer = new XmlSerializer(source.GetType());
        using var ms = new MemoryStream();
        serializer.Serialize(ms, source);
        return enc.GetString(ms.ToArray());
    }
}