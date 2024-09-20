using System;

namespace YiJian.Nursing.Settings
{
    /// <summary>
    /// 表:模块参数 读取输出
    /// </summary>
    [Serializable]
    public class ParaModuleData
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
        /// </summary>
        public string ModuleType { get; set; }

        /// <summary>
        /// 是否血流内导管
        /// </summary>
        public bool IsBloodFlow { get; set; }

        /// <summary>
        /// 模块拼音
        /// </summary>
        public string Py { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}