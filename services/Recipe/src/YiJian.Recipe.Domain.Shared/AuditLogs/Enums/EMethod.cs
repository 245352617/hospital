namespace YiJian.AuditLogs.Enums
{
    /// <summary>
    /// 方法类型  GET = 0, POST = 1, PUT = 2,DELETE = 3,HEAD = 4,OPTIONS = 5, PATCH = 6
    /// </summary>
    public enum EMethod : int
    {
        /// <summary>
        /// get
        /// </summary>
        GET = 0,

        /// <summary>
        /// post
        /// </summary>
        POST = 1,

        /// <summary>
        /// put
        /// </summary>
        PUT = 2,

        /// <summary>
        /// delete
        /// </summary>
        DELETE = 3,

        /// <summary>
        /// head
        /// </summary>
        HEAD = 4,

        /// <summary>
        /// options
        /// </summary>
        OPTIONS = 5,

        /// <summary>
        /// patch
        /// </summary>
        PATCH = 6,
    }

}
