using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace YiJian.ECIS.Core.Extensions;

/// <summary>
/// 对象拓展类
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// 将 DateTimeOffset 转换成本地 DateTime
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime ConvertToDateTime(this DateTimeOffset dateTime)
    {
        if (dateTime.Offset.Equals(TimeSpan.Zero))
            return dateTime.UtcDateTime;
        if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
            return dateTime.ToLocalTime().DateTime;
        else
            return dateTime.DateTime;
    }

    /// <summary>
    /// 年龄
    /// 年龄显示规则： 
    /// 1、不足24小时显示XX小时或XX小时XX分
    /// 2、不足1月显示XX天
    /// 3、不足一年显示XX月或XX月XX天
    /// 4、超过一年显示XX岁或XX岁XX月
    /// 5、超过6岁，直接显示XX岁
    /// </summary>
    /// <param name="datetime"></param>
    /// <param name="datetimeToDiff"></param>
    /// <returns></returns>
    public static string GetDiffDateTimeString(this DateTime datetime, DateTime datetimeToDiff)
    {
        TimeSpan diffTimeSpan = datetime - datetimeToDiff;
        int totalDiffMonths = (datetime.Year - datetimeToDiff.Year) * 12 +
            (datetime.Month - datetimeToDiff.Month) +
            (datetime.Day >= datetimeToDiff.Day ? 0 : -1) /*是否不足1月*/;
        if (diffTimeSpan.TotalHours < 24)
        {// 不足24小时
            var hoursString = $"{diffTimeSpan.Hours}小时";
            var minutesString = diffTimeSpan.Minutes != 0 ? $"{diffTimeSpan.Minutes}分" : "";
            return hoursString + minutesString;
        }
        // 显示岁
        string yearsString = totalDiffMonths < 12u ? "" : $"{totalDiffMonths / 12u}岁";
        // 超过6岁，不显示月份；不足1月，不显示月份
        string monthsString = totalDiffMonths / 12u >= 6 || totalDiffMonths <= 0 ? "" : $"{totalDiffMonths % 12u}月";

        string daysString = "";
        // 不足1月显示XX天
        if (totalDiffMonths < 1u)
        {
            daysString = $"{diffTimeSpan.Days}天";
        }

        return yearsString + monthsString + daysString;
    }

    /// <summary>
    /// 将 DateTime 转换成 DateTimeOffset
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTimeOffset ConvertToDateTimeOffset(this DateTime dateTime)
    {
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
    }

    /// <summary>
    /// 获取等待时长（xx小时xx分）
    /// </summary>
    /// <param name="beginTime"></param>
    /// <returns></returns>
    public static string GetWaitingTimeString(this DateTime beginTime)
    {
        return GetWaitingTimeString(beginTime, DateTime.Now);
    }

    /// <summary>
    /// 获取等待时长（xx小时xx分）
    /// </summary>
    /// <param name="endTime"></param>
    /// <param name="beginTime"></param>
    /// <returns></returns>
    private static string GetWaitingTimeString(this DateTime beginTime, DateTime endTime)
    {
        var timespan = endTime.Subtract(beginTime);
        if (timespan.TotalMinutes >= 60)
        {
            return $"{((int)timespan.TotalMinutes) / 60}小时{((int)timespan.TotalMinutes) % 60}分";
        }
        return $"{((int)timespan.TotalMinutes) % 60}分";
    }

    /// <summary>
    /// 判断是否是富基元类型
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns></returns>
    internal static bool IsRichPrimitive(this Type type)
    {
        // 处理元组类型
        if (type.IsValueTuple()) return false;

        // 处理数组类型，基元数组类型也可以是基元类型
        if (type.IsArray) return type.GetElementType().IsRichPrimitive();

        // 基元类型或值类型或字符串类型
        if (type.IsPrimitive || type.IsValueType || type == typeof(string)) return true;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) return type.GenericTypeArguments[0].IsRichPrimitive();

        return false;
    }

    /// <summary>
    /// 合并两个字典
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dic">字典</param>
    /// <param name="newDic">新字典</param>
    /// <returns></returns>
    internal static Dictionary<string, T> AddOrUpdate<T>(this Dictionary<string, T> dic, Dictionary<string, T> newDic)
    {
        foreach (var key in newDic.Keys)
        {
            if (dic.ContainsKey(key))
                dic[key] = newDic[key];
            else
                dic.Add(key, newDic[key]);
        }

        return dic;
    }

    /// <summary>
    /// 合并两个字典
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dic">字典</param>
    /// <param name="newDic">新字典</param>
    internal static void AddOrUpdate<T>(this ConcurrentDictionary<string, T> dic, Dictionary<string, T> newDic)
    {
        foreach (var (key, value) in newDic)
        {
            dic.AddOrUpdate(key, value, (key, old) => value);
        }
    }

    /// <summary>
    /// 判断是否是元组类型
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns></returns>
    internal static bool IsValueTuple(this Type type)
    {
        return type.ToString().StartsWith(typeof(ValueTuple).FullName);
    }


    /// <summary>
    /// 判断方法是否是异步
    /// </summary>
    /// <param name="method">方法</param>
    /// <returns></returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "VSTHRD200:对异步方法使用“Async”后缀", Justification = "<挂起>")]
    internal static bool IsAsync(this MethodInfo method)
    {
        return method.GetCustomAttribute<AsyncMethodBuilderAttribute>() != null
            || method.ReturnType.ToString().StartsWith(typeof(Task).FullName);
    }

    /// <summary>
    /// 判断类型是否实现某个泛型
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="generic">泛型类型</param>
    /// <returns>bool</returns>
    public static bool HasImplementedRawGeneric(this Type type, Type generic)
    {
        // 检查接口类型
        var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
        if (isTheRawGenericType) return true;

        // 检查类型
        while (type != null && type != typeof(object))
        {
            isTheRawGenericType = IsTheRawGenericType(type);
            if (isTheRawGenericType) return true;
            type = type.BaseType;
        }

        return false;

        // 判断逻辑
        bool IsTheRawGenericType(Type type) => generic == (type.IsGenericType ? type.GetGenericTypeDefinition() : type);
    }

    /// <summary>
    /// 判断是否是匿名类型
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns></returns>
    internal static bool IsAnonymous(this object obj)
    {
        var type = obj.GetType();

        return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
               && type.IsGenericType && type.Name.Contains("AnonymousType")
               && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
               && type.Attributes.HasFlag(TypeAttributes.NotPublic);
    }

    /// <summary>
    /// 获取所有祖先类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    internal static IEnumerable<Type> GetAncestorTypes(this Type type)
    {
        var ancestorTypes = new List<Type>();
        while (type != null && type != typeof(object))
        {
            if (IsNoObjectBaseType(type))
            {
                var baseType = type.BaseType;
                ancestorTypes.Add(baseType);
                type = baseType;
            }
            else break;
        }

        return ancestorTypes;

        static bool IsNoObjectBaseType(Type type) => type.BaseType != typeof(object);
    }

    /// <summary>
    /// 获取方法真实返回类型
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public static Type GetRealReturnType(this MethodInfo method)
    {
        // 判断是否是异步方法
        var isAsyncMethod = method.IsAsync();

        // 获取类型返回值并处理 Task 和 Task<T> 类型返回值
        var returnType = method.ReturnType;
        return isAsyncMethod ? (returnType.GenericTypeArguments.FirstOrDefault() ?? typeof(void)) : returnType;
    }

    /// <summary>
    /// 将一个对象转换为指定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T ChangeType<T>(this object obj)
    {
        return (T)ChangeType(obj, typeof(T));
    }

    /// <summary>
    /// 将一个对象转换为指定类型
    /// </summary>
    /// <param name="obj">待转换的对象</param>
    /// <param name="type">目标类型</param>
    /// <returns>转换后的对象</returns>
    public static object ChangeType(this object obj, Type type)
    {
        if (type == null) return obj;
        if (type == typeof(string)) return obj?.ToString();
        if (type == typeof(Guid) && obj != null) return Guid.Parse(obj.ToString());
        if (obj == null) return type.IsValueType ? Activator.CreateInstance(type) : null;

        var underlyingType = Nullable.GetUnderlyingType(type);
        if (type.IsAssignableFrom(obj.GetType())) return obj;
        else if ((underlyingType ?? type).IsEnum)
        {
            if (underlyingType != null && string.IsNullOrWhiteSpace(obj.ToString())) return null;
            else return Enum.Parse(underlyingType ?? type, obj.ToString());
        }
        // 处理DateTime -> DateTimeOffset 类型
        else if (obj.GetType().Equals(typeof(DateTime)) && (underlyingType ?? type).Equals(typeof(DateTimeOffset)))
        {
            return ((DateTime)obj).ConvertToDateTimeOffset();
        }
        // 处理 DateTimeOffset -> DateTime 类型
        else if (obj.GetType().Equals(typeof(DateTimeOffset)) && (underlyingType ?? type).Equals(typeof(DateTime)))
        {
            return ((DateTimeOffset)obj).ConvertToDateTime();
        }
        else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type))
        {
            try
            {
                return Convert.ChangeType(obj, underlyingType ?? type, null);
            }
            catch
            {
                return underlyingType == null ? Activator.CreateInstance(type) : null;
            }
        }
        else
        {
            var converter = TypeDescriptor.GetConverter(type);
            if (converter.CanConvertFrom(obj.GetType())) return converter.ConvertFrom(obj);

            var constructor = type.GetConstructor(Type.EmptyTypes);
            if (constructor != null)
            {
                var o = constructor.Invoke(null);
                var propertys = type.GetProperties();
                var oldType = obj.GetType();

                foreach (var property in propertys)
                {
                    var p = oldType.GetProperty(property.Name);
                    if (property.CanWrite && p != null && p.CanRead)
                    {
                        property.SetValue(o, ChangeType(p.GetValue(obj, null), property.PropertyType), null);
                    }
                }
                return o;
            }
        }
        return obj;
    }

    /// <summary>
    /// 查找方法指定特性，如果没找到则继续查找声明类
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="method"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static TAttribute GetFoundAttribute<TAttribute>(this MethodInfo method, bool inherit)
        where TAttribute : Attribute
    {
        // 获取方法所在类型
        var declaringType = method.DeclaringType;

        var attributeType = typeof(TAttribute);

        // 判断方法是否定义指定特性，如果没有再查找声明类
        var foundAttribute = method.IsDefined(attributeType, inherit)
            ? method.GetCustomAttribute<TAttribute>(inherit)
            : (
                declaringType.IsDefined(attributeType, inherit)
                ? declaringType.GetCustomAttribute<TAttribute>(inherit)
                : default
            );

        return foundAttribute;
    }

    /// <summary>
    /// 格式化字符串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    internal static string Format(this string str, params object[] args)
    {
        return args == null || args.Length == 0 ? str : string.Format(str, args);
    }

    /// <summary>
    /// 切割骆驼命名式字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    internal static string[] SplitCamelCase(this string str)
    {
        if (str == null) return Array.Empty<string>();

        if (string.IsNullOrWhiteSpace(str)) return new string[] { str };
        if (str.Length == 1) return new string[] { str };

        return Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
            .Where(u => u.Length > 0)
            .ToArray();
    }

    /// <summary>
    /// JsonElement 转 Object
    /// </summary>
    /// <param name="jsonElement"></param>
    /// <returns></returns>
    internal static object ToObject(this JsonElement jsonElement)
    {
        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.String:
                return jsonElement.GetString();
            case JsonValueKind.Undefined:
            case JsonValueKind.Null:
                return default;
            case JsonValueKind.Number:
                return jsonElement.GetDecimal();
            case JsonValueKind.True:
            case JsonValueKind.False:
                return jsonElement.GetBoolean();
            case JsonValueKind.Object:
                var enumerateObject = jsonElement.EnumerateObject();
                var dic = new Dictionary<string, object>();
                foreach (var item in enumerateObject)
                {
                    dic.Add(item.Name, item.Value.ToObject());
                }
                return dic;
            case JsonValueKind.Array:
                var enumerateArray = jsonElement.EnumerateArray();
                var list = new List<object>();
                foreach (var item in enumerateArray)
                {
                    list.Add(item.ToObject());
                }
                return list;
            default:
                return default;
        }
    }


    /// <summary>
    /// 清除字符串前后缀
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="pos">0：前后缀，1：后缀，-1：前缀</param>
    /// <param name="affixes">前后缀集合</param>
    /// <returns></returns>
    internal static string ClearStringAffixes(this string str, int pos = 0, params string[] affixes)
    {
        // 空字符串直接返回
        if (string.IsNullOrWhiteSpace(str)) return str;

        // 空前后缀集合直接返回
        if (affixes == null || affixes.Length == 0) return str;

        var startCleared = false;
        var endCleared = false;

        string tempStr = null;
        foreach (var affix in affixes)
        {
            if (string.IsNullOrWhiteSpace(affix)) continue;

            if (pos != 1 && !startCleared && str.StartsWith(affix, StringComparison.OrdinalIgnoreCase))
            {
                tempStr = str[affix.Length..];//消除前缀
                startCleared = true;
            }
            if (pos != -1 && !endCleared && str.EndsWith(affix, StringComparison.OrdinalIgnoreCase))
            {
                var _tempStr = !string.IsNullOrWhiteSpace(tempStr) ? tempStr : str;
                tempStr = _tempStr[..^affix.Length];//消除后缀
                endCleared = true;
            }
            if (startCleared && endCleared) break;
        }

        return !string.IsNullOrWhiteSpace(tempStr) ? tempStr : str;
    }

    ///// <summary>
    ///// 深度拷贝对象
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="t"></param>
    ///// <returns></returns>
    //public static T DeepClone<T>(this T t) where T : ISerializeBase
    //{
    //    var ms = new MemoryStream();
    //    var formater = new BinaryFormatter();
    //    formater.Serialize(ms, t);
    //    ms.Position = 0;
    //    return (T)formater.Deserialize(ms);
    //}

    /// <summary>
    /// 对象复制，会复制同名属性的值，不支持多层对象的深度复制，不支持属性名小写为"id"的属性的复制。
    /// </summary>
    /// <param name="objectdest">目标对象</param>
    /// <param name="objectsrc">源对象</param>
    /// <param name="ignoreGuid">默认不支持属性名小写为"id"的属性的复制</param>
    public static void Create<T>(this T objectdest, T objectsrc, bool ignoreGuid = true)
    {
        if (objectsrc == null)
        {
            return;
        }
        var sourceType = objectsrc.GetType();
        var destType = objectdest.GetType();
        foreach (var source in sourceType.GetProperties())
        {
            foreach (var dest in destType.GetProperties())
            {
                if (dest.Name == source.Name)
                {
                    if (ignoreGuid && dest.Name.ToLower() == "id")//Guid不复制
                    {
                        continue;
                    }
                    dest.SetValue(objectdest, source.GetValue(objectsrc));
                    continue;
                }
            }
        }
    }

    /// <summary>
    /// 对象复制，会复制同名属性的值，不支持多层对象的深度复制，不支持属性名小写为"id"的属性的复制。
    /// </summary>
    /// <typeparam name="T">目标对象</typeparam>
    /// <param name="objectsrc">源对象</param>
    /// <param name="ignoreGuid">默认不支持属性名小写为"id"的属性的复制</param>
    /// <returns></returns>
    public static T Create<T>(this T objectsrc, bool ignoreGuid = true) where T : new()
    {
        if (objectsrc == null)
        {
            return default;
        }
        var objectdest = new T();
        objectdest.Create(objectsrc, ignoreGuid);
        return objectdest;
    }

    /// <summary>
    /// 类型字段名对比
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static string CompareType(this Type t1, Type t2)
    {
        var sb = new StringBuilder();
        var tps1 = t1.GetProperties().Select(p => p.Name);
        var tps2 = t2.GetProperties().Select(p => p.Name);
        var same = tps1.Intersect(tps2);
        var left = tps1.Where(w => !tps2.Contains(w));
        var right = tps2.Where(w => !tps1.Contains(w));
        sb.AppendLine($"T1:{t1.Name},T2:{t2.Name}");
        sb.AppendLine($"============================================");
        sb.AppendLine($"相同项{same.Count()}个-------------");
        sb.AppendLine(String.Join(Environment.NewLine, same));
        sb.AppendLine($"仅T1项{left.Count()}个-------------");
        sb.AppendLine(String.Join(Environment.NewLine, left));
        sb.AppendLine($"仅T2项{right.Count()}个-------------");
        sb.AppendLine(String.Join(Environment.NewLine, right));
        sb.AppendLine($"============================================");
        return sb.ToString();
    }

    /// <summary>
    /// 类型字段名对比
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2_0"></param>
    /// <param name="t2_1"></param>
    /// <returns></returns>
    public static string CompareType(this Type t1, Type t2_0, Type t2_1)
    {
        var sb = new StringBuilder();
        var tps1 = t1.GetProperties().Select(p => p.Name);
        var tps2_0 = t2_0.GetProperties().Select(p => p.Name);
        var tps2_1 = t2_1.GetProperties().Select(p => p.Name);
        var tps2 = tps2_0.Concat(tps2_1).Distinct();
        var same = tps1.Intersect(tps2);
        var left = tps1.Where(w => !tps2.Contains(w));
        var right = tps2.Where(w => !tps1.Contains(w));
        sb.AppendLine($"T1:{t1.Name},T2:{t2_0.Name},T2:{t2_1.Name}");
        sb.AppendLine($"============================================");
        sb.AppendLine($"相同项{same.Count()}个-------------");
        sb.AppendLine(String.Join(Environment.NewLine, same));
        sb.AppendLine($"仅T1项{left.Count()}个-------------");
        sb.AppendLine(String.Join(Environment.NewLine, left));
        sb.AppendLine($"仅T2项{right.Count()}个-------------");
        sb.AppendLine(String.Join(Environment.NewLine, right));
        sb.AppendLine($"============================================");
        return sb.ToString();
    }

    /// <summary>
    /// 构造视图字段
    /// </summary>
    /// <param name="t"></param>
    /// <param name="viewName"></param>
    /// <param name="viewComment"></param>
    /// <returns></returns>
    public static string ViewProperties(this Type t, string viewName, string viewComment)
    {
        var sql = "insert into Dict_ViewSettings ([View],Comment,Prop,DefaultLabel,Label,DefaultWidth,Width,DefaultMinWidth,MinWidth,DefaultVisible,Visible,DefaultHeaderAlign,HeaderAlign,DefaultAlign,Align,DefaultShowTooltip,ShowTooltip,DefaultIndex,[Index],ParentID,IsActive) values ";
        var tps = t.GetProperties();
        var list = new List<string>();
        var index = 1;
        foreach (var tp in tps)
        {
            var da = tp.GetCustomAttribute<DescriptionAttribute>(false);
            if (da != null)
            {
                var ba = tp.GetCustomAttribute<BrowsableAttribute>(false);
                bool visible = true;
                if (ba != null && !ba.Browsable)
                {
                    visible = false;
                }
                var prop = tp.Name;
                var label = da.Description;
                list.Add($"('{viewName}',N'{viewComment}','{prop.ToCamelCase()}',N'{label}',N'{label}',100,100,100,100,{(visible ? 1 : 0)},{(visible ? 1 : 0)},'center','center','left','left',1,1,{index},{index},0,1)");
                index++;
            }
        }
        if (list.Count > 0)
        {
            sql += string.Join(",", list);
            return sql;
        }
        return "";
    }

    ///// <summary>
    ///// 输出对象的具体字段和字段赋值
    ///// </summary>
    ///// <param name="obj"></param>
    ///// <returns></returns>
    //public static string ToSimpleView(this object obj)
    //{
    //    if (obj == null)
    //    {
    //        return "null";
    //    }
    //    var sb = new StringBuilder();
    //    var sourceType = obj.GetType();
    //    foreach (var source in sourceType.GetProperties())
    //    {
    //        //var tp = source.PropertyType;
    //        //if (tp == typeof(string) || tp.IsPrimitive || tp.IsValueType)
    //        //{
    //        sb.AppendLine($"'{source.Name}':'{source.GetValue(obj)}'");
    //        //}
    //        //else
    //        //{
    //        //    sb.AppendLine($"'{source.Name}':");
    //        //    sb.AppendLine($"{source.GetValue(obj).ToSimpleView()}");
    //        //}
    //    }

    //    return sb.ToString();
    //}

    /// <summary>
    /// 检查数据库实体中对字符串长度的约束
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static bool CheckStringLength<T>(this T t, out string name, out string value, out int maxLength)
    {
        var tp = typeof(T);
        foreach (var p in tp.GetProperties())
        {
            var sla = tp.GetCustomAttribute<StringLengthAttribute>(false);
            if (sla != null)
            {
                var v = p.GetValue(t)?.ToString();
                if (v != null)
                {
                    if (v.Length > sla.MaximumLength)
                    {
                        name = p.Name;
                        value = v;
                        maxLength = sla.MaximumLength;
                        return false;
                    }
                }
            }
        }
        name = "";
        value = "";
        maxLength = 0;
        return true;
    }
}