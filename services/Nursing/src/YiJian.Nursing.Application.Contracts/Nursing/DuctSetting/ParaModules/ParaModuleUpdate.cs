using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.Nursing.Settings
{
    /// <summary>
    /// 表:模块参数 修改输入
    /// </summary>
    [Serializable]
    public class ParaModuleUpdate
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [DynamicStringLength(typeof(ParaModuleConsts), nameof(ParaModuleConsts.MaxModuleNameLength), ErrorMessage = "模块名称最大长度不能超过{1}!")]
        [Required(ErrorMessage = "模块名称不能为空！")]
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块显示名称
        /// </summary>
        [DynamicStringLength(typeof(ParaModuleConsts), nameof(ParaModuleConsts.MaxDisplayNameLength), ErrorMessage = "模块显示名称最大长度不能超过{1}!")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
        /// </summary>
        [DynamicStringLength(typeof(ParaModuleConsts), nameof(ParaModuleConsts.MaxModuleTypeLength), ErrorMessage = "模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）最大长度不能超过{1}!")]
        public string ModuleType { get; set; }

        /// <summary>
        /// 是否血流内导管
        /// </summary>
        public bool IsBloodFlow { get; set; }

        /// <summary>
        /// 模块拼音
        /// </summary>
        [DynamicStringLength(typeof(ParaModuleConsts), nameof(ParaModuleConsts.MaxPyLength), ErrorMessage = "模块拼音最大长度不能超过{1}!")]
        public string Py { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required(ErrorMessage = "是否启用不能为空！")]
        public bool IsEnable { get; set; }
    }
}