using System.Collections.Generic;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描述：一键执行Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/27 20:01:44
    /// </summary>
    public class BatchExecDto
    {
        /// <summary>
        /// 一键执行列表Dto
        /// </summary>
        public List<CheckExecDto> ExecArr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
    }
}
