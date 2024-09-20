using hyjiacan.py4n;
using System.Linq;

namespace YiJian.MasterData;

/// <summary>
/// 拼音工具
/// </summary>
public static class PYUtil
{
    /// <summary>
    /// 获取中文拼音首字母
    /// </summary>
    /// <param name="hz"></param>
    /// <returns></returns>
    public static string ParsePY(this string hz)
    { 
        return string.Join("", Pinyin4Net.GetPinyinArray(hz, PinyinFormat.UPPERCASE).Select(s => s.FirstOrDefault()?.FirstOrDefault()));
    } 
}
