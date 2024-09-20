using System;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:人体图-编号字典 读取输出
    /// </summary>
    [Serializable]
    public class CanulaPartData
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 部位名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 部位编号
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}