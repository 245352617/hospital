using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.Reflection;

namespace YiJian.ECIS.ShareModel.Extensions;

/// <summary>
/// 实体扩展类
/// </summary>
public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// 从实体配置表（包括表备注、字段备注、字段默认值）
    /// 由于字段类型、是否可空之类的信息已由 efcore 实现，故而无需多余的扩展
    /// </summary>
    /// <param name="b"></param>
    public static void ConfigureByEntity(this EntityTypeBuilder b)
    {
        // 表备注
        SetTableComment(b);
        foreach (PropertyInfo info in b.Metadata.ClrType.GetProperties())
        {
            // 设置备注
            SetFieldComment(b, info);
            // 设置默认值
            SetFieldDefaultValue(b, info);
        }
    }

    /// <summary>
    /// 设置表备注
    /// </summary>
    /// <param name="b"></param>
    private static void SetTableComment(EntityTypeBuilder b)
    {
        string tableDescription = b.Metadata.ClrType.GetCustomAttribute<DescriptionAttribute>()?.Description;
        if (string.IsNullOrEmpty(tableDescription)) return;

        // 设置表备注
        b.HasComment(tableDescription);
    }

    /// <summary>
    /// 设置字段默认值
    /// </summary>
    /// <param name="b"></param>
    /// <param name="info"></param>
    private static void SetFieldDefaultValue(EntityTypeBuilder b, PropertyInfo info)
    {
        DefaultValueAttribute defaultValueAttribute = info.GetCustomAttribute<DefaultValueAttribute>();
        if (defaultValueAttribute == null) return;

        // 设置默认值
        b.Property(info.Name).HasDefaultValue(defaultValueAttribute.Value);
    }

    /// <summary>
    /// 设置字段备注
    /// </summary>
    /// <param name="b"></param>
    /// <param name="info"></param>
    private static void SetFieldComment(EntityTypeBuilder b, PropertyInfo info)
    {
        IsNeedCommentAttribute customAttribute = info.GetCustomAttribute<IsNeedCommentAttribute>();
        DescriptionAttribute descAttribute = info.GetCustomAttribute<DescriptionAttribute>();
        if (customAttribute != null && !customAttribute.IsNeed) return;
        if (descAttribute == null) return;

        // 设置备注
        b.Property(info.Name).HasComment(descAttribute.Description);
    }
}
