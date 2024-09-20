using System.Threading.Tasks;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 描述：执行单打印数据接口
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:19:06
    /// </summary>
    public interface IExecutePrintAppService
    {
        /// <summary>
        /// 获取执行单打印数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<object> GetExecutePrintDataAsync(ExecuteQueryInput input);
    }
}
