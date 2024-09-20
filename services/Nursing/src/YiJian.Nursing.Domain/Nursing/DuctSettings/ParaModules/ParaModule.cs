using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;

namespace YiJian.Nursing.Settings
{
    /// <summary>
    /// 表:模块参数（基础配置表）
    /// </summary>
    [Comment("表:模块参数（基础配置表）")]
    public class ParaModule : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 模块代码
        /// </summary>
        [StringLength(50)]
        [Required]
        [Comment("模块代码")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [StringLength(80)]
        [Required]
        [Comment("模块名称")]
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块显示名称
        /// </summary>
        [StringLength(80)]
        [Comment("模块显示名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [StringLength(20)]
        [Comment("科室代码")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("模块类型")]
        public string ModuleType { get; set; }

        /// <summary>
        /// 是否血流内导管
        /// </summary>
        [Comment("是否血流内导管")]
        public bool IsBloodFlow { get; set; }

        /// <summary>
        /// 模块拼音
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        [Comment("模块拼音")]
        public string Py { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [StringLength(10)]
        [Required]
        [Comment("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [Comment("是否启用")]
        public bool IsEnable { get; set; }

        #region constructor
        /// <summary>
        /// 表:模块参数构造器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="displayName">模块显示名称</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleType">模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）</param>
        /// <param name="isBloodFlow">是否血流内导管</param>
        /// <param name="py">模块拼音</param>
        /// <param name="sort">排序</param>
        /// <param name="isEnable">是否启用</param>
        public ParaModule(Guid id,
            [NotNull] string moduleCode,  // 模块代码
            [NotNull] string moduleName,  // 模块名称
            string displayName,           // 模块显示名称
            [NotNull] string deptCode,    // 科室代码
            string moduleType,            // 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
            bool isBloodFlow,             // 是否血流内导管
            string py,                    // 模块拼音
            [NotNull] int sort,        // 排序
            [NotNull] bool isEnable      // 是否启用
            ) : base(id)
        {
            //模块代码
            ModuleCode = Check.NotNull(moduleCode, "模块代码", ParaModuleConsts.MaxModuleCodeLength);
            //科室代码
            DeptCode = Check.Length(deptCode, "科室代码", ParaModuleConsts.MaxDeptCodeLength);

            //模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
            ModuleType = Check.Length(moduleType, "模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）", ParaModuleConsts.MaxModuleTypeLength);

            Modify(moduleName,// 模块名称
                displayName,    // 模块显示名称
                isBloodFlow,    // 是否血流内导管
                py,             // 模块拼音
                sort,        // 排序
                isEnable       // 是否启用
                );
        }
        #endregion

        #region Modify
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="displayName">模块显示名称</param>
        /// <param name="isBloodFlow">是否血流内导管</param>
        /// <param name="py">模块拼音</param>
        /// <param name="sort">排序</param>
        /// <param name="isEnable">是否启用</param>
        public void Modify([NotNull] string moduleName,// 模块名称
            string displayName,           // 模块显示名称

            bool isBloodFlow,             // 是否血流内导管
            string py,                    // 模块拼音
            [NotNull] int sort,        // 排序
            [NotNull] bool isEnable     // 是否启用
            )
        {

            //模块名称
            ModuleName = Check.NotNull(moduleName, "模块名称", ParaModuleConsts.MaxModuleNameLength);

            //模块显示名称
            DisplayName = Check.Length(displayName, "模块显示名称", ParaModuleConsts.MaxDisplayNameLength);


            //是否血流内导管
            IsBloodFlow = isBloodFlow;

            //模块拼音
            Py = Check.Length(moduleName.FirstLetterPY(), "模块拼音", ParaModuleConsts.MaxPyLength);

            //排序
            Sort = Check.NotNull(sort, "排序");

            //是否启用
            IsEnable = Check.NotNull(isEnable, "是否启用");


        }
        #endregion

        #region constructor
        private ParaModule()
        {
            // for EFCore
        }
        #endregion
    }
}
