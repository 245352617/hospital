namespace YiJian.ECIS.ShareModel.Utils;

/// <summary>
/// 日期工具
/// </summary>
public static class DateTimeUtil
{
    /// <summary>
    /// 跟进生日获取年龄
    /// <![CDATA[
    ///  eg: 小于1岁算零岁，小于6岁算5岁（未到生日之前都算实岁）
    /// ]]>
    /// </summary>
    /// <param name="birthdate"></param>
    /// <returns></returns>
    public static int GetAgeByBirthdate(this DateTime birthdate)
    {
        DateTime now = DateTime.Now;
        int age = now.Year - birthdate.Year;
        if (now.Month < birthdate.Month || (now.Month == birthdate.Month && now.Day < birthdate.Day))
        {
            age = age - 1;
        }
        return age < 0 ? 0 : age;
    }

    /// <summary>
    /// 小于六岁算儿童，不包括6周岁
    /// </summary>
    /// <returns></returns>
    public static bool IsChildren(this DateTime birthdate)
    {
        var age = birthdate.GetAgeByBirthdate();
        if (age < 6) return true;
        return false;
    }

}
