namespace YiJian.ECIS.Core
{
    /// <summary>
    /// 选项 1允许字段 2.不自动调用saveChange
    /// </summary>
    public interface IRepoOptions
    {
        /// <summary>
        /// 允许字段
        /// </summary>
        string AllowedFields { get; }
        /// <summary>
        /// 不自动调用saveChange
        /// </summary>
        bool SaveChangeBySelf { get; }
    }

    public class RepoOptions : IRepoOptions
    {
        public virtual string AllowedFields { get; set; }
        //不自动调用saveChange
        public bool SaveChangeBySelf { get; set; }
    }
}
