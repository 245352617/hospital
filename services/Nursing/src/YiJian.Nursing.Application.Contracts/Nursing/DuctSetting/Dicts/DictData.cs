using System;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:导管字典-通用业务 读取输出
    /// </summary>
    [Serializable]
    public class DictData
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 字典代码
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 字典值说明
        /// </summary>
        public string DictDesc { get; set; }

        /// <summary>
        /// 上级代码
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 字典标准（国标、自定义）
        /// </summary>
        public string DictStandard { get; set; }

        /// <summary>
        /// HIS对照代码
        /// </summary>
        public string HisCode { get; set; }

        /// <summary>
        /// HIS对照
        /// </summary>
        public string HisName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}