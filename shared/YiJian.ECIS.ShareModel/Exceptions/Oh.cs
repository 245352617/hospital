namespace YiJian.ECIS.ShareModel.Exceptions
{
    /// <summary>
    /// 错误处理
    /// </summary>
    public static class Oh
    {
        /// <summary>
        /// 抛出错误
        /// </summary>
        /// <param name="message">错误信息</param>
        public static EcisBusinessException Error(string message)
        {
            throw new EcisBusinessException(message: message);
        }
    }
}
