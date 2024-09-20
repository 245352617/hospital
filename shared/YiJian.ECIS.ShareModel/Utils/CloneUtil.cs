using Newtonsoft.Json;
using System.Reflection;
using System.Xml.Serialization;

namespace YiJian.ECIS.ShareModel.Utils;

/// <summary>
/// 克隆工具
/// </summary>
public static class CloneUtil
{
    ///// <summary>
    ///// 使用二进制流克隆
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentException"></exception>
    //public static T Clone<T>(T source)
    //{
    //    if (!typeof(T).IsSerializable)
    //    {
    //        throw new ArgumentException("The type must be serializable.", "source");
    //    }

    //    if (Object.ReferenceEquals(source, null))
    //    {
    //        return default(T);
    //    }

    //    IFormatter formatter = new BinaryFormatter();
    //    Stream stream = new MemoryStream();
    //    using (stream)
    //    {
    //        formatter.Serialize(stream, source);
    //        stream.Seek(0, SeekOrigin.Begin);
    //        return (T)formatter.Deserialize(stream);
    //    }
    //}

    /// <summary>
    /// 使用系列化反系列化方式克隆
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T CloneJson<T>(this T source)
    {
        if (Object.ReferenceEquals(source, null))
        {
            return default(T);
        }

        var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
    }

    /// <summary>
    /// 使用反射克隆
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepCopyByReflect<T>(T obj)
    {
        //如果是字符串或值类型则直接返回
        if (obj is string || obj.GetType().IsValueType) return obj;
        object retval = Activator.CreateInstance(obj.GetType());
        FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            try { field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj))); }
            catch { }
        }
        return (T)retval;
    }

    /// <summary>
    /// 使用XML序列化与反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="xmlData"></param>
    /// <returns></returns>
    public static T DeserializeXML<T>(string xmlData) where T : new()
    {
        if (string.IsNullOrEmpty(xmlData))
            return default(T);

        TextReader tr = new StringReader(xmlData);
        T DocItms = new T();
        XmlSerializer xms = new XmlSerializer(DocItms.GetType());
        DocItms = (T)xms.Deserialize(tr);

        return DocItms == null ? default(T) : DocItms;
    }
}
