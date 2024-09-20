using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Basic.RecipeProjects.Dto
{
    /// <summary>
    /// 分页查询条件
    /// Directory: input
    /// </summary>
    public class RecipeProjectPagedInput : PageBase
    {
        /// <summary>
        /// 类别编码
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 过滤条件（编码、名称）
        /// </summary>
        public string Filter { get; set; }
    }
}
