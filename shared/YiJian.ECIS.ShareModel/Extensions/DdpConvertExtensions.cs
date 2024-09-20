namespace System;

/// <summary>
/// Ddp类型转换
/// </summary>
public static class DdpConvertExtensions
{
    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static decimal DdpParseDecimal(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return 0;
        if (decimal.TryParse(value.Trim(), out decimal result)) return result;
        return 0;
    }

    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static decimal? DdpParseNullableDecimal(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return null;
        if (decimal.TryParse(value.Trim(), out decimal result)) return result;
        return null;
    }

    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static double DdpParseDouble(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return 0;
        if (double.TryParse(value.Trim(), out double result)) return result;
        return 0;
    }

    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static double? DdpParseNullableDouble(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return null;
        if (double.TryParse(value.Trim(), out double result)) return result;
        return null;
    }


    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float DdpParseFloat(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return 0;
        if (float.TryParse(value.Trim(), out float result)) return result;
        return 0;
    }


    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float? DdpParseNullableFloat(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return null;
        if (float.TryParse(value.Trim(), out float result)) return result;
        return null;
    }

    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int DdpParseInt(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return 0;
        if (int.TryParse(value.Trim(), out int result)) return result;
        return 0;
    }

    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int? DdpParseNullableInt(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return null;
        if (int.TryParse(value.Trim(), out int result)) return result;
        return null;
    }

    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool DdpParseBool(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return false;
        if (value == "1" || value.ToLower() == "true") return true;
        if (bool.TryParse(value.Trim(), out bool result)) return result;
        return false;
    }

    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool? DdpParseNullableBool(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return null;
        if (value == "1" || value.ToLower() == "true")
        {
            return true;
        }
        else if (value == "0" || value.ToLower() == "false")
        {
            return false;
        }
        if (bool.TryParse(value.Trim(), out bool result)) return result;
        return null;
    }

    /// <summary> 
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int DdpParseInsuranceCatalog(this string? value)
    {
        if (value is null || value.IsNullOrWhiteSpace()) return 0;

        switch (value)
        {
            case "自费":
                return 0;
            case "甲":
                return 1;
            case "乙":
                return 2;
            case "丙":
                return 4;
            case "少儿":
                return 5;
            case "其它":
                return 3;
            default:
                return 3;
        }
    }

    /// <summary> 
    /// 转换
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int DdpParseUnpack(this string? value)
    {
        //门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        if (value is null || value.IsNullOrWhiteSpace()) return 3; //北大默认按大包装计算
        if (int.TryParse(value, out int result)) return result;
        return 0;
    }




}
