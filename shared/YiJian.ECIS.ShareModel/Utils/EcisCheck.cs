namespace YiJian.ECIS.ShareModel.Utils;

/// <summary>
/// 实体检查
/// </summary>
public static class EcisCheck
{
    /// <summary>
    /// Guid 参数是否已初始化
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Guid NotNullOrEmpty(Guid value, string parameterName, string message = null)
    {
        message ??= $"{parameterName} can not be null not empty!";
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message);
        }

        return value;
    }

    /// <summary>
    /// Guid 参数是否已初始化
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Guid? NotNullOrEmpty(Guid? value, string parameterName, string message = null)
    {
        message ??= $"{parameterName} can not be null not empty!";
        if (!value.HasValue || value == Guid.Empty)
        {
            throw new ArgumentException(message);
        }

        return value;
    }
}
