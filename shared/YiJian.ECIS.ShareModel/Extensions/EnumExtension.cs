using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Volo.Abp;

namespace YiJian.ECIS.ShareModel.Extensions;

/// <summary>
/// 枚举扩展类
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// 获取枚举的描述信息
    /// </summary>
    public static string GetDescription(this Enum enumValue, [CallerMemberName] string memberName = "")
    {
        string value = enumValue.ToString();

        var type = enumValue.GetType();

        FieldInfo field = type.GetField(value);

        if (field != null)
        {
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (objs.Length == 0) return value;

            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }

        throw new UserFriendlyException($"枚举值无效: {memberName}, value: {enumValue}");
    }

    /// <summary>
    /// 根据描述返回枚举
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="description"></param>
    /// <returns></returns>
    public static T ToEnum<T>(this string description) where T : Enum
    {
        FieldInfo[] fields = typeof(T).GetFields();
        foreach (FieldInfo field in fields)
        {
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (objs.Length > 0 && (objs[0] as DescriptionAttribute).Description == description)
            {
                return (T)field.GetValue(null);
            }
        }

        return default(T);
    }
}
